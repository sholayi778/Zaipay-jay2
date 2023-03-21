
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
    public class CanadaEftPaymentService : ICanadaEftPaymentService
    {
        private readonly HttpClient apiClient;
        public IConfiguration Configuration { get; }
        private string baseUrl ;
    
        public CanadaEftPaymentService(IConfiguration configuration)
        { 
            Configuration = configuration;
            this.baseUrl = Configuration.GetSection("Apaylo:baseUrl").Value;
            var apiKey = Configuration.GetSection("Apaylo:key").Value;

            var signature = this.GenerateSignature().Result;

            apiClient = new HttpClient();

            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("signature", signature); 
            apiClient.DefaultRequestHeaders.Add("key", apiKey); 
        }

        public async Task<string> GenerateSignature()
        {
            try
            {
                var apiKey = Configuration.GetSection("Apaylo:key").Value;
                var secret = Configuration.GetSection("Apaylo:secret").Value;

                string currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                string concatedString = apiKey + secret + currentDate;

                byte[] concatedBytes = Encoding.UTF8.GetBytes(concatedString);
                byte[] hashBytes = new SHA512Managed().ComputeHash(concatedBytes);

                string signature = Convert.ToBase64String(hashBytes);
                return signature;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerEFTResponseObj> CreateCustomer(CreateCustomerObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/EFT/CreateCustomer";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                
                if (responseMsg.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<CustomerEFTResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while processing: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TransactionResponseObj> CreateEFTTransaction(TransactionObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/EFT/CreateEFTTransaction";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                
                if (responseMsg.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<TransactionResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while processing: Response message is: {responseStr}");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SearchResponseObj> SearchEFTTransaction(SearchTransactionObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/EFT/SearchEFTTransaction";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<SearchResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while processing: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public async Task<CustomerEFTResponseObj> CancelEFTTransaction(CancelTransactionObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/EFT/CancelEFTTransaction";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<CustomerEFTResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while processing: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerEFTResponseObj> UpdateEFTCustomerAccount(UpdateCustomerObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/EFT/UpdateEFTCustomerAccount";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<CustomerEFTResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while processing: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
