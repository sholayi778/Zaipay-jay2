using System.ComponentModel.DataAnnotations;

namespace Zaipay.Requests
{
    public class CreateMFSPayoutRequest
    {
        [Required]
        public string sourceCurrency { get; set; }

        [Required]
        public string destinationCurrency { get; set; }

        [Required]
        public string beneficiaryId { get; set; }

        [Required]
        public string amount { get; set; }

        [Required]
        public string toCountry { get; set; }
        //[Required]
        public string customerReference { get; set; }
    }
}
