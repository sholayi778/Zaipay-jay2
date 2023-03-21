using System.Collections.Generic;

namespace Zaipay.Responses
{ 
    public class MarketplacePayId
    {
        public string pay_id { get; set; }
        public string type { get; set; }
    }

    public class NppDetails
    {
        public string pay_id { get; set; }
        public List<MarketplacePayId> marketplace_pay_ids { get; set; }
        public string reference { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
    }

    public class WalletAccountNppResponse
    {
        public WalletAccounts wallet_accounts { get; set; }
    }

    public class WalletAccounts
    {
        public string id { get; set; }
        public NppDetails npp_details { get; set; }
    }
}
