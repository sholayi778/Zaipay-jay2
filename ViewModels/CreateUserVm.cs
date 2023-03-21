using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Zaipay.ViewModels
{
    public class CreateUserVm
    {

        [Required]
        [JsonProperty("id")]
        public string TopRateId { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string mobile { get; set; }
        [Required]
        public string country { get; set; }
    }
}
