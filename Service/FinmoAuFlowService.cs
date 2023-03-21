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
    public class FinmoAuFlowService : IFinmoAuFlow
    {
        private readonly HttpClient apiClient;
        public IConfiguration Configuration { get; }
        private string baseUrl;
        private string apiKey;
        private string secret;
    
        public FinmoAuFlowService(IConfiguration configuration)
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

        private async Task<string> GenerateSignature()
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

        public async Task<CustomerResponseObject> CreateCustomer(FinmoCustomerObj request)
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
                     var response = JsonConvert.DeserializeObject<CustomerResponseObject>(responseStr);
                    
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

        public async Task<WalletResponseObj> CreateWallet(WalletRequestObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/v1/wallet";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<WalletResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the wallet: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PayResponseObj> CreatePay(PayRequestObj request)
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
                     var response = JsonConvert.DeserializeObject<PayResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the pay: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<VirtualAccountResponseObj> CreateVirtualAccount(VirtualAccountRequestObj request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/v1/virtual-account";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<VirtualAccountResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the virtual account: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PoliPayResponseObj> CreatePayinPoli(PoliPayRequestObj request)
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
                     var response = JsonConvert.DeserializeObject<PoliPayResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the pay poli: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<WalletResponseObj> GetWalletById(string wallet_id)
        {
            try
            {
                var url = this.baseUrl + $"/v1/wallet?wallet_id={wallet_id}";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.GetAsync(url);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<WalletResponseObj>(responseStr);
                    
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

        public async Task<WalletFundTransferResponseObj> WalletFundTransfer(WalletFundTransferRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + $"/v1/wallet-fund-transfer";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<WalletFundTransferResponseObj>(responseStr);
                    
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

        public async Task<VirtualAccountResponseObj2> VirtualAccountSimulate(VirtualAccountRequest2 request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + $"/v1/virtual-account/simulate-payin";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<VirtualAccountResponseObj2>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the virtual account: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SimulatePayIdResponseObj> SimulatePayIn(SimulatePayIdRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + $"/v1/payin/simulate";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();
                if (responseMsg.IsSuccessStatusCode)
                {
                     var response = JsonConvert.DeserializeObject<SimulatePayIdResponseObj>(responseStr);
                    
                    return response;
                }
                else
                    throw new Exception($"Error while creating the siminulating: Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
