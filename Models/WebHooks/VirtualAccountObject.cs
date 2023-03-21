using Newtonsoft.Json;

namespace Zaipay.Models.WebHooks
{
    public class VirtualAccountObject
    {
        [JsonProperty("event")]
        public string eventType { get;set; }
        public string id { get;set; }
        public string link { get;set; }
        public string name { get;set; }
        public string updatedAt { get;set; }
    }
}
