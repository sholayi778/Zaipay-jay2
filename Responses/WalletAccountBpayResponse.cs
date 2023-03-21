namespace Zaipay.Responses
{
     
    public class BpayDetails
    {
        public string biller_code { get; set; }
        public string reference { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
    }

    public class WalletAccountBpayResponse
    {
        public WalletAccountsBpay wallet_accounts { get; set; }
    }

    public class WalletAccountsBpay
    {
        public string id { get; set; }
        public BpayDetails bpay_details { get; set; }
    }
}
