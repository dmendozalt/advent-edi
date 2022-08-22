using Newtonsoft.Json;

namespace EDI.Entities.Entities
{
    public class ItemContainer
    {
        [JsonProperty(PropertyName = "id")]
        public string ContainerId { get; set; }

        [JsonProperty(PropertyName = "Origin")]
        public string? Origin { get; set; }

        [JsonProperty(PropertyName = "Destination")]
        public string? Destination { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "Dimensions")]
        public string Dimensions { get; set; }

        [JsonProperty(PropertyName = "Book")]
        public bool Book { get; set; }

        [JsonProperty(PropertyName = "IssuedBy")]
        public string IssuedBy { get; set; }

        [JsonProperty(PropertyName = "Fee")]
        public int Fee { get; set; }
    }
}
