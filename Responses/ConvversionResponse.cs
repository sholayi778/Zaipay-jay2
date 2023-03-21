using System.Collections.Generic;

namespace Zaipay.Responses
{
    public class ConvversionData
    {
        public string rate { get; set; }
    }

    public class ConvversionResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public ConvversionData data { get; set; }
        public List<object> error { get; set; }
    }
}
