using EDI.Contracts.Repository;
using EDI.Core.V1;
using EDI.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDI.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EDIController : ControllerBase
    {
        private readonly EDICore _ediCore;

        public EDIController(IEDIRepository context, ILogger<X12_315> logger)
        {
            _ediCore = new EDICore(context, logger);
        }

        // GET: api/<EDIController>
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<List<ItemContainer>>> Get()
        {
            var response = await _ediCore.GetAllContainers();
            return StatusCode((int)response.StatusHttp, response);
        }

        // GET api/<EDIController>/5
        [HttpGet("{id}")]
        [ActionName("GetContainer")]
        public async Task<ActionResult<List<ItemContainer>>> GetById(string id)
        {
            var response = await _ediCore.GetContainerById(id);
            return StatusCode((int)response.StatusHttp, response);
        }

        // POST api/<EDIController>
        [HttpPost]
        [ActionName("PostContainer")]
        public async Task<ActionResult<Tuple<List<ItemContainer>, bool>>> Post()
        {
            var response = await _ediCore.PostContainers();
            return StatusCode((int)response.StatusHttp, response);
        }
    }
}
