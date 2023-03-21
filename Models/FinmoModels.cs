using System;
using System.Collections.Generic;

    public class Individual
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string identification_type { get; set; }
        public string identification_custom_type { get; set; }
        public string identification_value { get; set; }
        public string country_of_residence { get; set; }
        public string nationality { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string address_city { get; set; }
        public string address_state { get; set; }
        public string address_zip_code { get; set; }
        public string address_country { get; set; }
        public string phone_country_code { get; set; }
        public string phone_number { get; set; }
    }

    public class FinmoCustomerObj
    {
        public string type { get; set; }
        public string organization_reference_id { get; set; }
        public Individual individual { get; set; }
    }

    public class CustomerResponseData
    {
        public string customer_id { get; set; }
        public string org_id { get; set; }
        public string type { get; set; }
        public object description { get; set; }
        public object webhook_url { get; set; }
        public object metadata { get; set; }
        public bool is_active { get; set; }
        public bool is_wallet_ready { get; set; }
        public string organization_reference_id { get; set; }
        public Individual individual { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class CustomerResponseObject
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public CustomerResponseData data { get; set; }
    }

    public class WalletAccountObj
    {
        public string wallet_account_id { get; set; }
        public string wallet_id { get; set; }
        public string org_id { get; set; }
        public string currency { get; set; }
        public int actual_balance { get; set; }
        public int available_balance { get; set; }
        public bool is_active { get; set; }
        public bool is_settlement_allowed { get; set; }
        public bool is_deleted { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object deleted_at { get; set; }
    }

        public class WalletResponseData
    {
        public string wallet_id { get; set; }
        public string org_id { get; set; }
        public string description { get; set; }
        public object metadata { get; set; }
        public string category { get; set; }
        public string scope { get; set; }
        public string created_by { get; set; }
        public string wallet_alias { get; set; }
        public string customer_id { get; set; }
        public object raw_data { get; set; }
        public object webhook_url { get; set; }
        public object deleted_at { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<WalletAccountObj> wallet_account { get; set; }
    }

    public class WalletResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public WalletResponseData data { get; set; }
    }

    public class WalletRequestObj
    {
        public string customer_id { get; set; }
        public string currency { get; set; }
    }

    public class PayinMethodParam
    {
        public string payid_reference { get; set; }
    }
    public class PayCode
    {
        public string text { get; set; }
    }

    public class PayRequestObj
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public string payin_method_name { get; set; }
        public string payin_type { get; set; }
        public string organization_reference_id { get; set; }
        public string customer_id { get; set; }
        public string credit_wallet_id { get; set; }
        public PayinMethodParam payin_method_param { get; set; }
    }

    public class PayResponseData
    {
        public string payin_id { get; set; }
        public string org_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public object description { get; set; }
        public object receipt_email { get; set; }
        public object metadata { get; set; }
        public string status { get; set; }
        public string payin_type { get; set; }
        public string payin_method_name { get; set; }
        public string payin_method_description { get; set; }
        public string payin_method_category { get; set; }
        public PayinMethodParam payin_method_param { get; set; }
        public bool is_refundable { get; set; }
        public bool is_partially_refundable { get; set; }
        public bool is_over_refundable { get; set; }
        public bool is_cancellable { get; set; }
        public string organization_reference_id { get; set; }
        public string credit_wallet_id { get; set; }
        public string customer_id { get; set; }
        public string fees_wallet_id { get; set; }
        public object redirect_url { get; set; }
        public object advanced_redirect_url { get; set; }
        public object checkout_id { get; set; }
        public DateTime expire_at { get; set; }
        public string payin_token { get; set; }
        public object virtual_account_id { get; set; }
        public object transaction_id { get; set; }
        public object credit_wallet_transaction_id { get; set; }
        public object fees_wallet_transaction_id { get; set; }
        public object webhook_url { get; set; }
        public object refund_status { get; set; }
        public PayCode pay_code { get; set; }
        public object paid_at { get; set; }
        public object paid_amount { get; set; }
        public object paid_currency { get; set; }
        public object fees_without_tax { get; set; }
        public object tax_on_fees { get; set; }
        public object fees_including_tax { get; set; }
        public object fees_currency { get; set; }
        public object refund_amount_requested { get; set; }
        public object refund_currency_requested { get; set; }
        public object refund_amount_completed { get; set; }
        public object refund_currency_completed { get; set; }
        public object sender_party_detail { get; set; }
        public object additional_data { get; set; }
        public object expired_at { get; set; }
        public object cancelled_at { get; set; }
        public bool is_paid { get; set; }
        public bool is_partially_paid { get; set; }
        public bool is_fees_charged { get; set; }
        public bool is_chargeable { get; set; }
        public bool is_refund_initiated { get; set; }
        public bool is_completely_refunded { get; set; }
        public bool is_reconciled { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

     public class PayResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public PayResponseData data { get; set; }
    }  

    public class VirtualAccountRequestObj
    {
        public string currency { get; set; }
        public string customer_id { get; set; }
        public string credit_wallet_id { get; set; }
        public string payin_method_name { get; set; }
        public string credit_wallet_category { get; set; }
        public string scope { get; set; }
    }

    public class VirtualAccountResponseData
    {
        public string virtual_account_id { get; set; }
        public string org_id { get; set; }
        public string scope { get; set; }
        public string customer_id { get; set; }
        public string credit_wallet_id { get; set; }
        public object description { get; set; }
        public object metadata { get; set; }
        public bool is_active { get; set; }
        public string currency { get; set; }
        public string payin_method_name { get; set; }
        public object payin_method_param { get; set; }
        public PayCode2 pay_code { get; set; }
        public string transaction_id { get; set; }
        public bool is_deleted { get; set; }
        public bool is_expired { get; set; }
        public bool is_refundable { get; set; }
        public int fees_without_tax { get; set; }
        public double tax_on_fees { get; set; }
        public double fees_including_tax { get; set; }
        public string fees_currency { get; set; }
        public bool is_fees_charged { get; set; }
        public bool is_chargeable { get; set; }
        public bool is_cancellable { get; set; }
        public object organization_reference_id { get; set; }
        public object webhook_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object expire_at { get; set; }
        public object expired_at { get; set; }
        public object deleted_at { get; set; }
    }

    public class AdditionalData
    {
        public string bank_reference_id { get; set; }
    }

    public class VirtualData2
    {
        public string payin_id { get; set; }
        public string payin_method_name { get; set; }
        public string payin_method_description { get; set; }
        public string payin_method_category { get; set; }
        public object payin_method_param { get; set; }
        public string org_id { get; set; }
        public string virtual_account_id { get; set; }
        public string customer_id { get; set; }
        public string transaction_id { get; set; }
        public string credit_wallet_id { get; set; }
        public string fees_wallet_id { get; set; }
        public string credit_wallet_transaction_id { get; set; }
        public string fees_wallet_transaction_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public object receipt_email { get; set; }
        public object redirect_url { get; set; }
        public object advanced_redirect_url { get; set; }
        public object webhook_url { get; set; }
        public string status { get; set; }
        public object refund_status { get; set; }
        public PayCode pay_code { get; set; }
        public string payin_type { get; set; }
        public bool is_paid { get; set; }
        public DateTime paid_at { get; set; }
        public bool is_partially_paid { get; set; }
        public int paid_amount { get; set; }
        public string paid_currency { get; set; }
        public double fees_without_tax { get; set; }
        public double tax_on_fees { get; set; }
        public double fees_including_tax { get; set; }
        public string fees_currency { get; set; }
        public bool is_fees_charged { get; set; }
        public bool is_chargeable { get; set; }
        public object refund_amount_requested { get; set; }
        public object refund_currency_requested { get; set; }
        public object refund_amount_completed { get; set; }
        public object refund_currency_completed { get; set; }
        public bool is_refundable { get; set; }
        public bool is_partially_refundable { get; set; }
        public bool is_over_refundable { get; set; }
        public bool is_refund_initiated { get; set; }
        public bool is_completely_refunded { get; set; }
        public object metadata { get; set; }
        public SenderPartyDetail sender_party_detail { get; set; }
        public object checkout_id { get; set; }
        public AdditionalData additional_data { get; set; }
        public object organization_reference_id { get; set; }
        public bool is_reconciled { get; set; }
        public bool is_cancellable { get; set; }
        public object payin_token { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object expire_at { get; set; }
        public object expired_at { get; set; }
        public object cancelled_at { get; set; }
    }

    public class SenderPartyDetail
    {
        public string bsb { get; set; }
        public string account_number { get; set; }
        public string sender_reference { get; set; }
        public string account_holder_name { get; set; }
    }

    public class PayCode2
    {
        public string bsb { get; set; }
        public string account_number { get; set; }
        public string account_holder_name { get; set; }
    }

    public class VirtualAccountResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public VirtualAccountResponseData data { get; set; }
    }

    public class VirtualAccountResponseObj2
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public VirtualData2 data { get; set; }
    }

    public class VirtualAccountRequest2
    {
        public string virtual_account_id { get; set; }
        public int amount { get; set; }
    }

    public class AdvancedRedirectUrl
    {
        public string completed_url { get; set; }
        public string expired_url { get; set; }
        public string cancelled_url { get; set; }
        public string failed_url { get; set; }
    }

    public class PoliPayRequestObj
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public string payin_method_name { get; set; }
        public string payin_type { get; set; }
        public string organization_reference_id { get; set; }
        public string customer_id { get; set; }
        public string credit_wallet_id { get; set; }
        public AdvancedRedirectUrl advanced_redirect_url { get; set; }
    }

        public class PoliPayDataResponse
    {
        public string payin_id { get; set; }
        public string org_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public object description { get; set; }
        public object receipt_email { get; set; }
        public object metadata { get; set; }
        public string status { get; set; }
        public string payin_type { get; set; }
        public string payin_method_name { get; set; }
        public string payin_method_description { get; set; }
        public string payin_method_category { get; set; }
        public object payin_method_param { get; set; }
        public bool is_refundable { get; set; }
        public bool is_partially_refundable { get; set; }
        public bool is_over_refundable { get; set; }
        public bool is_cancellable { get; set; }
        public string organization_reference_id { get; set; }
        public string credit_wallet_id { get; set; }
        public string customer_id { get; set; }
        public string fees_wallet_id { get; set; }
        public object redirect_url { get; set; }
        public AdvancedRedirectUrl advanced_redirect_url { get; set; }
        public object checkout_id { get; set; }
        public DateTime expire_at { get; set; }
        public string payin_token { get; set; }
        public object virtual_account_id { get; set; }
        public object transaction_id { get; set; }
        public object credit_wallet_transaction_id { get; set; }
        public object fees_wallet_transaction_id { get; set; }
        public object webhook_url { get; set; }
        public object refund_status { get; set; }
        public PayCode3 pay_code { get; set; }
        public object paid_at { get; set; }
        public object paid_amount { get; set; }
        public object paid_currency { get; set; }
        public object fees_without_tax { get; set; }
        public object tax_on_fees { get; set; }
        public object fees_including_tax { get; set; }
        public object fees_currency { get; set; }
        public object refund_amount_requested { get; set; }
        public object refund_currency_requested { get; set; }
        public object refund_amount_completed { get; set; }
        public object refund_currency_completed { get; set; }
        public object sender_party_detail { get; set; }
        public object additional_data { get; set; }
        public object expired_at { get; set; }
        public object cancelled_at { get; set; }
        public bool is_paid { get; set; }
        public bool is_partially_paid { get; set; }
        public bool is_fees_charged { get; set; }
        public bool is_chargeable { get; set; }
        public bool is_refund_initiated { get; set; }
        public bool is_completely_refunded { get; set; }
        public bool is_reconciled { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class PayCode3
    {
        public string redirect_url { get; set; }
    }

    public class PoliPayResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public PoliPayDataResponse data { get; set; }
    }

    public class WalletFundTransferRequest
    {
        public string debit_wallet_id { get; set; }
        public string credit_wallet_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string organization_reference_id { get; set; }
    }

    
    public class WalletFundTransferData
    {
        public string wallet_fund_transfer_id { get; set; }
        public string debit_org_id { get; set; }
        public string debit_wallet_id { get; set; }
        public string debit_wallet_alias_id { get; set; }
        public string debit_customer_id { get; set; }
        public object metadata { get; set; }
        public string credit_org_id { get; set; }
        public string credit_wallet_id { get; set; }
        public string credit_wallet_alias_id { get; set; }
        public object credit_customer_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string organization_reference_id { get; set; }
        public object webhook_url { get; set; }
        public object invoice_id { get; set; }
        public string credit_wallet_transaction_id { get; set; }
        public string debit_wallet_transaction_id { get; set; }
        public string debit_transaction_id { get; set; }
        public string credit_transaction_id { get; set; }
        public object fees_wallet_id { get; set; }
        public object fees_wallet_transaction_id { get; set; }
        public object fees_without_tax { get; set; }
        public object tax_on_fees { get; set; }
        public object fees_including_tax { get; set; }
        public object fees_currency { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class WalletFundTransferResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public WalletFundTransferData data { get; set; }
    }

    public class SimulatePayIdRequest
    {
        public string payin_id { get; set; }
        public string status { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string name { get; set; }
        public string message { get; set; }
    }

    public class SimulatePayIdResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public Error error { get; set; }
    }