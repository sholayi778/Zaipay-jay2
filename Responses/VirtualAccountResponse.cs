using System;

namespace Zaipay.Responses
{
    public class VirtualAccountResponse
    {
        public VirtualAccountResponse()
        {
            this.created_at = DateTime.UtcNow.ToLongDateString();
            this.updated_at = DateTime.UtcNow.ToLongDateString();
        }
        public string id { get; set; }
        public string routing_number { get; set; }
        public string account_number { get; set; }
        public string currency { get; set; }
        public string wallet_account_id { get; set; }
        public string user_external_id { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
