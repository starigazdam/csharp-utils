using Newtonsoft.Json;

namespace JsonManipulation.Model
{
    public class OrderGuid
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string? OrderNumber { get; set; }
    }
}
