using System.Collections.Generic;

public class RequestInteracObj
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }

    public class RequestResult
    {
        public string Message { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionNumber { get; set; }
    }

    public class RequestResponseObj
    {
        public int StatusCode { get; set; }
        public bool IsError { get; set; }
        public RequestResult Result { get; set; }
    }

    public class SearchRequestInteracObj
    {
        public List<string> TransactionNumbers { get; set; }
    }

    public class InteracTransaction
    {
        public string ReferenceNumber { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Description { get; set; }
    }

    public class SearchInteracResult
    {
        public List<InteracTransaction> InteracTransactions { get; set; }
    }

    public class SearchInteracResponseObj
    {
        public int StatusCode { get; set; }
        public bool IsError { get; set; }
        public SearchInteracResult Result { get; set; }
    }

