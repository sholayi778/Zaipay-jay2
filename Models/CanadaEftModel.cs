using System;
using System.Collections.Generic;

public class CreateCustomerObj
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TransitNumber { get; set; }
        public string InstitutionNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
    }
    public class CustomerResult
    {
        public string Message { get; set; }
        public string CustomerNumber { get; set; }
        public int CustomerAccountId { get; set; }
    }

    public class CustomerEFTResponseObj
    {
        public int StatusCode { get; set; }
        public bool IsError { get; set; }
        public CustomerResult Result { get; set; }
    }


    public class TransactionObj
    {
        public string Email { get; set; }
        public int CustomerAccountId { get; set; }
        public string TransactionTypeCode { get; set; }
        public double Amount { get; set; }
    }

    public class TransactionResult
    {
        public string Message { get; set; }
        public string TransactionNumber { get; set; }
    }

    public class TransactionResponseObj
    {
        public int StatusCode { get; set; }
        public bool IsError { get; set; }
        public TransactionResult Result { get; set; }
    }

    public class SearchTransactionObj
    {
        public string TransactionNumber { get; set; }
    }

    public class SearchResult
    {
        public string TransactionId { get; set; }
        public string TransactionNumber { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerAccountId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeDescription { get; set; }
        public string EftTypeCode { get; set; }
        public string EftTypeDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionDescription { get; set; }
         public string Message { get; set; }
    }

    public class SearchResponseObj
    {
        public int StatusCode { get; set; }
        public bool IsError { get; set; }
        public List<SearchResult> Result { get; set; }
    }

    public class CancelTransactionObj
    {
        public string TransactionNumber { get; set; }
        public string CustomerNumber { get; set; }
    }

    public class UpdateCustomerObj
    {
        public int CustomerAccountId { get; set; }
        public string CustomerNumber { get; set; }
        public string TransitNumber { get; set; }
        public string InstitutionNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
    }


    