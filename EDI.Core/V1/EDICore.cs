using indice.Edi;
using EDI.Core.Handlers;
using Microsoft.Extensions.Logging;
using EDI.Entities.Entities;
using EDI.Contracts.Repository;
using EDI.Entities.Utils;
using System.Net;

namespace EDI.Core.V1
{
    public class EDICore
    {
        private readonly IEDIRepository _ediContext;
        private readonly ErrorHandler<X12_315> _errorHandler;
        private readonly ILogger<X12_315> _logger;

        public EDICore(IEDIRepository context, ILogger<X12_315> logger)
        {
            _ediContext = context;
            _logger = logger;
            _errorHandler = new ErrorHandler<X12_315>(logger);
        }

        public async Task<ResponseService<ItemContainer>> GetContainerById(string id)
        {
            try
            {
                var response = _ediContext.GetByIdAsync(id);
                return new ResponseService<ItemContainer>(false, response == null ? "No records found" : "Container Found", HttpStatusCode.OK, response.Result);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "Get container", new ItemContainer());
            }
        }

        public async Task<ResponseService<List<ItemContainer>>> GetAllContainers()
        {
            try
            {
                var response = await _ediContext.GetAllAsync();
                return new ResponseService<List<ItemContainer>>(false, response == null ? "No records found" : "All Containers", HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "Get all containers", new List<ItemContainer>());
            }
        }

        public async Task<ResponseService<Tuple<List<ItemContainer>, bool>>> PostContainers()
        {
            try
            {
                List<ItemContainer> ediProcessed = ProcessEDIToJson(@"C:\Users\DOLCEY MENDOZA\Documents\projects\advent-edi\EDI.Entities\x12.315.edi");
                foreach (var container in ediProcessed)
                {
                    var containerJson = Newtonsoft.Json.JsonConvert.SerializeObject(container);
                    await _ediContext.AddAsync(container);
                }
                return new ResponseService<Tuple<List<ItemContainer>, bool>>(false, ediProcessed.Count >= 1 ? "Container Added" : "Containers Added", HttpStatusCode.OK, Tuple.Create(ediProcessed, true));
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "PostContainers", Tuple.Create(new List<ItemContainer>(), false));
            }
            
        }

        public List<ItemContainer> ProcessEDIToJson(string inputEDIFilename) 
        {
            var ediGrammar = EdiGrammar.NewX12();
            ediGrammar.SetAdvice(
                segmentNameDelimiter: '*',
                dataElementSeparator: '*',
                componentDataElementSeparator: '>',
                segmentTerminator: '~',
                releaseCharacter: null,
                reserved: null,
                decimalMark: '.'
            );

            var po315 = default(X12_315);
            using (var stream = new StreamReader(inputEDIFilename))
            {
                po315 = new EdiSerializer().Deserialize<X12_315>(stream, ediGrammar);
                List<ItemContainer> containers = containersMapping(po315);
                return containers;
            }
        }

        public List<ItemContainer> containersMapping(X12_315 x12_315)
        {
            var containers = new List<ItemContainer>();
            Random rand=new();
            foreach (var group in x12_315.Groups)
            {
                foreach (var order in group.Orders)
                {
                    foreach (var info in order.ReferenceIds)
                    {
                        ItemContainer container = new ItemContainer()
                        {
                            ContainerId = info.ReferenceIdentification,
                            Dimensions = "20x10x5",
                            Book = true,
                            IssuedBy = group.ApplicationSenderCode,
                            Fee = rand.Next(100,10001)
                        };
                        foreach (var port in order.PortsOrTerminal)
                        {
                            if(port.PortOrTerminalFunctionCode == "L" || port.PortOrTerminalFunctionCode == "O")
                            {
                                container.Status = "IN YARD";
                            }

                            if (port.PortOrTerminalFunctionCode == "L")
                            {
                                container.Origin = port.LocationIdentifier;
                                container.Description = port.PortName;
                            }
                            
                            if (port.PortOrTerminalFunctionCode == "M" || port.PortOrTerminalFunctionCode == "1")
                            {
                                container.Destination = port.LocationIdentifier;
                                container.Status = "UNLOAD FROM VESSEL";
                            }
                            
                            if (port.PortOrTerminalFunctionCode == "R")
                                container.Status = "UNLOAD FROM VESSEL";
                            
                            if (port.PortOrTerminalFunctionCode == "T" || port.PortOrTerminalFunctionCode == "Y")
                                container.Status = "GATE OUT";
                        }
                        containers.Add(container);
                    }
                }
            }
            return containers;
        }
    }
}
