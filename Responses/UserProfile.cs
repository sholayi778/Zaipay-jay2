using Newtonsoft.Json;
using System;

namespace Zaipay.Responses
{
    public class UserProfile
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonProperty("CountryOfResidence")]
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
