using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Zaipay.Service
{
    public class FinmoNzFlowService : IFinmoNzFlowService
    {
        private readonly HttpClient apiClient;
        public IConfiguration Configuration { get; }
        private string baseUrl;
        private string apiKey;
        private string secret;
    
        public FinmoNzFlowService(IConfiguration configuration)
        { 
            Configuration = configuration;
            this.baseUrl = Configuration.GetSection("Finmo:baseUrl").Value;
            this.apiKey = Configuration.GetSection("Finmo:key").Value;
            this.secret = Configuration.GetSection("Finmo:secret").Value;

            //var signature = this.GenerateSignature().Result;

            apiClient = new HttpClient();

            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(apiKey + ":" + secret));

            apiClient.DefaultRequestHeaders.Add("Authorization", "Basic " + svcCredentials);
        }

        public async Task<CustomerResponseObj> CreateCustomer(CustomerRequestNZObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/v1/customer";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<CustomerResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the customer: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PayinResponseObj> CreatePayment(PayInPoliRequestObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/v1/payin";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<PayinResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the payment: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}