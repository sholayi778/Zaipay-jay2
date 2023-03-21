using System.ComponentModel.DataAnnotations;

namespace Zaipay.Requests
{
    public class CreatePayoutRequest
    {
        public CreatePayoutRequest()
        {
            this.business = "62c2d46d3acaf70bd6329611"; 

        }
        [Required]
        public string sourceCurrency { get; set; }

        [Required]
        public string destinationCurrency { get; set; }

        [Required]
        public string beneficiaryId { get; set; }
         

        [Required]
        public string amount { get; set; }

        [Required]
        public string business { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string customerReference { get; set; }
    }
}
