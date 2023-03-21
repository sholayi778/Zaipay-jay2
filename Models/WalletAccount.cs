using System;

namespace Zaipay.Models
{
    //public class WalletAccount
    //{
    //}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Links
    {
        public string self { get; set; }
        public string users { get; set; }
        public string batch_transactions { get; set; }
        public string transactions { get; set; }
        public string bpay_details { get; set; }
        public string npp_details { get; set; }
        public string payin_details { get; set; }
        public string virtual_accounts { get; set; }
    }

    public class WalletAccount
    {
        public WalletAccounts wallet_accounts { get; set; }
    }

    public class WalletAccounts
    {
        public string id { get; set; }
        public bool active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int balance { get; set; }
        public string currency { get; set; }
        public int total_amount { get; set; }
        public Links links { get; set; }
    }


}
