using indice.Edi.Serialization;

namespace EDI.Entities.Entities
{
    public class X12_315
    {
        public List<FunctionalGroup> Groups { get; set; }

        [EdiGroup]
        public class FunctionalGroup
        {

            [EdiValue("X(2)", Path = "GS/0", Description = "GS01 - Functional Identifier Code")]
            public string FunctionalIdentifierCode { get; set; }

            [EdiValue("X(15)", Path = "GS/1", Description = "GS02 - Application Sender's Code")]
            public string ApplicationSenderCode { get; set; }

            [EdiValue("X(15)", Path = "GS/2", Description = "GS03 - Application Receiver's Code")]
            public string ApplicationReceiverCode { get; set; }

            [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "GS04 - Date")]
            [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "GS05 - Time")]
            public DateTime Date { get; set; }

            [EdiValue("9(9)", Path = "GS/5", Format = "HHmm", Description = "GS06 - Group Control Number")]
            public int GroupControlNumber { get; set; }

            [EdiValue("X(2)", Path = "GS/6", Format = "HHmm", Description = "GS07 Responsible Agency Code")]
            public string AgencyCode { get; set; }

            [EdiValue("X(2)", Path = "GS/7", Format = "HHmm", Description = "GS08 Version / Release / Industry Identifier Code")]
            public string Version { get; set; }

            public List<Order> Orders { get; set; }


            [EdiValue("9(1)", Path = "GE/0", Description = "97 Number of Transaction Sets Included")]
            [EdiCount(EdiCountScope.Messages)]
            public int TransactionsCount { get; set; }

            [EdiValue("9(9)", Path = "GE/1", Description = "28 Group Control Number")]
            public int GroupTrailerControlNumber { get; set; }
        }

        [EdiMessage]
        public class Order
        {
            #region Header Trailer

            [EdiValue("X(3)", Path = "ST/0", Description = "ST01 - Transaction set ID code")]
            public string TransactionSetCode { get; set; }

            [EdiValue("X(9)", Path = "ST/1", Description = "ST02 - Transaction set control number")]
            public string TransactionSetControlNumber { get; set; }

            [EdiValue(Path = "SE/0", Description = "SE01 - Number of included segments")]
            [EdiCount(EdiCountScope.Segments)]
            public int SegmentsCounts { get; set; }

            [EdiValue("X(9)", Path = "SE/1", Description = "SE02 - Transaction set control number (same as ST02)")]
            public string TrailerTransactionSetControlNumber { get; set; }
            #endregion

            [EdiValue("X(2)", Path = "B4/0", Description = "B401 - Special Handling Code")]
            public string SpecialHandlingCode { get; set; }

            [EdiValue("X(2)", Path = "B4/1", Description = "B402 - Inquiry Request Number")]
            public string InquiryRequestNumber { get; set; }

            [EdiValue(Path = "B4/2", Description = "B403 - Shipment Status Code")]
            public string ShipmentStatusCode { get; set; }

            [EdiValue("9(8)", Path = "B4/3", Format = "yyyyMMdd", Description = "B404 - Date")]
            [EdiValue("9(4)", Path = "B4/4", Format = "HHmm", Description = "B405 - Time")]
            public DateTime Date { get; set; }
            [EdiValue(Path = "B4/5", Description = "B406 - Status Location ")]
            public string StatusLocation  { get; set; }
            [EdiValue(Path = "B4/6", Description = "B407 - Equipment Initial - Prefix or alphabetic part of an equipment unit's identifying number")]
            public string EquipmentInitial { get; set; }
            [EdiValue(Path = "B4/7", Description = "B408 - Equipment Number - Sequencing or serial part of an equipment unit's identifying number")]
            public string EquipmentNumber { get; set; }
            [EdiValue(Path = "B4/8", Description = "B409 - Equipment Status Code")]
            public string EquipmentStatusCode { get; set; }
            [EdiValue(Path = "B4/9", Description = "B410 - Equipment Type")]
            public string EquipmentType { get; set; }
            [EdiValue(Path = "MSG/0", Description = "MSG01 - Message Text")]
            public string OrderHeaderMessageText { get; set; }
            public List<ReferenceId> ReferenceIds { get; set; }
            public List<StatusDetails> StatusDetails { get; set; }
            public List<PortOrTerminal> PortsOrTerminal { get; set; }

            [EdiValue(Path = "B4/10", Description = "B411 - Location Identifier")]
            public string LocationIdentifier { get; set; }
            [EdiValue(Path = "B4/11", Description = "B412 - Location Qualifier")]
            public string LocationQualifier { get; set; }
            [EdiValue(Path = "B4/12", Description = "B413 - Equipment Number Check Digit")]
            public string EquipmentNumberCheckDigit { get; set; }
        }

        [EdiSegment, EdiSegmentGroup("N9", SequenceEnd = "R4")]
        public class ReferenceId
        {
            [EdiValue(Path = "N9/0", Description = "N901 - Reference Identification Qualifier")]
            public string ReferenceIdentificationQualifier { get; set; }
            [EdiValue(Path = "N9/1", Description = "N902 - Reference Identification")]
            public string ReferenceIdentification { get; set; }
            [EdiValue(Path = "N9/2", Description = "N903 - Free-form Description")]
            public string FreeFormDescription { get; set; }
            [EdiValue("9(8)", Path = "N9/3", Format = "yyyyMMdd", Description = "N9/04 - Date")]
            [EdiValue("9(4)", Path = "N9/4", Format = "HHmm", Description = "N9/05 - Time")]
            public DateTime Date { get; set; }
            [EdiValue(Path = "N9/5", Description = "N906 - Time Code")]
            public string TimeCode { get; set; }
            [EdiValue(Path = "N9/6", Description = "N907 - Reference Identifier")]
            public string ReferenceIdentifier { get; set; }
        }

        [EdiSegment, EdiSegmentGroup("Q2", SequenceEnd = "R4")]
        public class StatusDetails
        {
            [EdiValue(Path = "Q2/0", Description = "Q201 - Vessel Code")]
            public string VeselCode { get; set; }
            [EdiValue(Path = "Q2/1", Description = "Q202 - Country Code")]
            public string CountryCode { get; set; }
            [EdiValue("9(8)", Path = "Q2/2", Format = "yyyyMMdd", Description = "Q2/03 - Date")]
            public DateTime Date { get; set; }
            [EdiValue("9(8)", Path = "Q2/3", Format = "yyyyMMdd", Description = "Q2/04 - Date 2")]
            public DateTime Date2 { get; set; }
            [EdiValue("9(8)", Path = "Q2/4", Format = "yyyyMMdd", Description = "Q2/05 - Date 3")]
            public DateTime Date3 { get; set; }
            [EdiValue(Path = "Q2/5", Description = "Q206 - Landing Quantity")]
            public string LandingQuantity { get; set; }
            [EdiValue(Path = "Q2/6", Description = "Q207 - Weight")]
            public string Weight { get; set; }
            [EdiValue(Path = "Q2/7", Description = "Q208 - Weight Qualifier")]
            public string WeightQualifier { get; set; }
            [EdiValue(Path = "Q2/8", Description = "Q209 - Voyage Number")]
            public string VoyageNumber { get; set; }
            [EdiValue(Path = "Q2/9", Description = "Q210 - Reference Identification Qualifier")]
            public string ReferenceIdentificationQualifier { get; set; }
            [EdiValue(Path = "Q2/10", Description = "Q211 - Reference Identification")]
            public string ReferenceIdentification { get; set; }
            [EdiValue(Path = "Q2/11", Description = "Q212 - Vessel Code Qualifier")]
            public string VesselCodeQualifier { get; set; }
            [EdiValue(Path = "Q2/12", Description = "Q213 - Vessel Name")]
            public string VesselName { get; set; }
            [EdiValue(Path = "Q2/13", Description = "Q214 - Volume")]
            public string Volume { get; set; }
            [EdiValue(Path = "Q2/14", Description = "Q215 - Volume Unit Qualifier")]
            public string VolumeUnitQualifier { get; set; }
            [EdiValue(Path = "Q2/15", Description = "Q216 - Weight Unit Code")]
            public string WeightUnitCode { get; set; }
            public List<MSG> MSG { get; set; }
            public List<DTM> DTM { get; set; }
        }

        [EdiSegment, EdiSegmentGroup("R4", SequenceEnd = "DTM")]
        public class PortOrTerminal
        {

            [EdiValue(Path = "R4/0", Description = "R401 - Port or Terminal Function Code")]
            public string PortOrTerminalFunctionCode { get; set; }

            [EdiValue(Path = "R4/1", Description = "R402 - Location Qualifier")]
            public string LocationQualifier { get; set; }

            [EdiValue(Path = "R4/2", Description = "R403 - LocationIdentifier")]
            public string LocationIdentifier { get; set; }

            [EdiValue(Path = "R4/3", Description = "R404 - Port Name")]
            public string PortName { get; set; }

            [EdiValue(Path = "R4/4", Description = "R405 - Country Code")]
            public string CountryCode { get; set; }
            [EdiValue(Path = "R4/5", Description = "R406 - Terminal Name")]
            public string TerminalName { get; set; }
            [EdiValue(Path = "R4/6", Description = "R407 - Pier Number")]
            public string PierNumber { get; set; }
            [EdiValue(Path = "R4/7", Description = "R408 - State or Province Code")]
            public string StateOrProvinceCode { get; set; }

        }

        [EdiSegment, EdiSegmentGroup("DTM")]
        public class DTM
        {

            [EdiValue(Path = "DTM/0", Description = "DTM01 - Date/Time Qualifier")]
            public string DateTimeQualifier { get; set; }

            [EdiValue("9(8)", Path = "DTM/1", Format = "yyyyMMdd", Description = "DTM02 - Date")]
            [EdiValue("9(4)", Path = "DTM/2", Format = "HHmm", Description = "DTM03 - Time")]
            public DateTime Date { get; set; }
            [EdiValue(Path = "DTM/3", Description = "DTM04 - Time Code")]
            public string TimeCode { get; set; }
        }

        [EdiSegment, EdiPath("MSG")]
        public class MSG
        {

            [EdiValue(Path = "MSG/0", Description = "MSG01 - Message Text")]
            public string MessageText { get; set; }
        }
    }
}
