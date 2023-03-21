using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Zaipay.Models.WebHooks
{   
    public partial class Transactions
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("payee_name")]
        public string PayeeName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("type_method")]
        public string TypeMethod { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("user_id")]
        public Guid UserId { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("debit_credit")]
        public string DebitCredit { get; set; }

        [JsonProperty("marketplace")]
        public Marketplace Marketplace { get; set; }

        [JsonProperty("related")]
        public Related Related { get; set; }

        [JsonProperty("payin_details")]
        public PayinDetails PayinDetails { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self")]
        public object Self { get; set; }

        [JsonProperty("users")]
        public string Users { get; set; }

        [JsonProperty("fees")]
        public string Fees { get; set; }

        [JsonProperty("wallet_accounts")]
        public string WalletAccounts { get; set; }

        [JsonProperty("card_accounts")]
        public string CardAccounts { get; set; }

        [JsonProperty("paypal_accounts")]
        public string PaypalAccounts { get; set; }

        [JsonProperty("bank_accounts")]
        public string BankAccounts { get; set; }

        [JsonProperty("items")]
        public string Items { get; set; }

        [JsonProperty("marketplaces")]
        public string Marketplaces { get; set; }

        [JsonProperty("npp_payin_transaction_detail")]
        public string NppPayinTransactionDetail { get; set; }
    }

    public partial class Marketplace
    {
        [JsonProperty("group_name")]
        public object GroupName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short_name")]
        public object ShortName { get; set; }

        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
    }

    public partial class PayinDetails
    {
        [JsonProperty("debtor_name")]
        public string DebtorName { get; set; }

        [JsonProperty("debtor_legal_name")]
        public string DebtorLegalName { get; set; }

        [JsonProperty("debtor_bsb")]
        public string DebtorBsb { get; set; }

        [JsonProperty("debtor_account")]
        public string DebtorAccount { get; set; }

        [JsonProperty("clearing_system_transaction_id")]
        public string ClearingSystemTransactionId { get; set; }

        [JsonProperty("remittance_information")]
        public string RemittanceInformation { get; set; }

        [JsonProperty("pay_id")]
        public string PayId { get; set; }

        [JsonProperty("pay_id_type")]
        public string PayIdType { get; set; }

        [JsonProperty("end_to_end_id")]
        public string EndToEndId { get; set; }

        [JsonProperty("npp_payin_internal_id")]
        public Guid NppPayinInternalId { get; set; }
    }

    public partial class Related
    {
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }
    }

    public partial class Transaction
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("user_id")]
        public Guid UserId { get; set; }
    }
    

}
