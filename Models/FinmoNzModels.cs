
using System;

public class Individual_NZ
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

    public class CustomerRequestNZObj
    {
        public string type { get; set; }
        public string organization_reference_id { get; set; }
        public Individual_NZ individual { get; set; }
    }

        public class CustomerData
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

    public class CustomerResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public CustomerData data { get; set; }
    }


    public class AdvancedRedirectUrl_NZ
    {
        public string completed_url { get; set; }
        public string expired_url { get; set; }
        public string cancelled_url { get; set; }
        public string failed_url { get; set; }
    }

    public class PayInPoliRequestObj
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public string payin_method_name { get; set; }
        public string payin_type { get; set; }
        public string organization_reference_id { get; set; }
        public string customer_id { get; set; }
        public AdvancedRedirectUrl_NZ advanced_redirect_url { get; set; }
    }

        public class PayinData_NZ
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
        public AdvancedRedirectUrl_NZ advanced_redirect_url { get; set; }
        public object checkout_id { get; set; }
        public DateTime expire_at { get; set; }
        public string payin_token { get; set; }
        public object virtual_account_id { get; set; }
        public object transaction_id { get; set; }
        public object credit_wallet_transaction_id { get; set; }
        public object fees_wallet_transaction_id { get; set; }
        public object webhook_url { get; set; }
        public object refund_status { get; set; }
        public PayCode_NZ pay_code { get; set; }
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

    public class PayCode_NZ
    {
        public string redirect_url { get; set; }
    }

    public class PayinResponseObj
    {
        public string request_id { get; set; }
        public DateTime request_time { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_text { get; set; }
        public PayinData_NZ data { get; set; }
    }

