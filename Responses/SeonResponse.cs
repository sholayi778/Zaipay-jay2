using System;

namespace Zaipay.Responses
{
    public class Data
    {
        public Guid DataId { get; set; }

        public string id { get; set; }
        public string state { get; set; }
        public double fraud_score { get; set; }
    }


    public class SeonResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public bool success { get; set; }
        public Data data { get; set; }
    }
}
