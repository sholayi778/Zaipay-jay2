using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Zaipay.Configurations;
using Zaipay.Models;
using Zaipay.Models.WebHooks;
using Zaipay.Requests;
using Zaipay.Responses;
using Zaipay.ViewModels;

namespace Zaipay.Service
{
    public class ZaiPayPlatformService : IZaiPayPlatform
    {
        
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IMapper _mapper;
        private readonly HttpClient apiClient;

        private readonly ZaiConfig _config;
        private string baseUrl ;
        private readonly string username ;
        private readonly string password ;

        private readonly InternalConfig _internalConfig;

        private readonly ZaipayDbContext _zaipayDbContext;
        public ZaiPayPlatformService(ZaipayDbContext zaipayDbContext, 
            IOptions<ZaiConfig> _zaiConfig, IOptions<InternalConfig> _internalConfig,
            ILoggedInUserService loggedInUserService, IMapper mapper)
        { 
            this.baseUrl = _zaiConfig.Value.BaseUrl ;
            this.username = _zaiConfig.Value.Username ;
            this.password = _zaiConfig.Value.Password ;
            this._config = _zaiConfig.Value;

            this._internalConfig = _internalConfig.Value;

            this._loggedInUserService = loggedInUserService;
            _zaipayDbContext = zaipayDbContext;

            var data = _zaipayDbContext.Tokens.FirstOrDefault();
            var bearerToken = data?.BearerToken;
            
            if(data is null)
            {
                bearerToken = this.GenerateToken(new TokenRequest()).Result;
            }
            else if( DateTime.UtcNow.Date != data.MaintainedAt.Date || (DateTime.UtcNow.Hour - data.MaintainedAt.Hour) >= 1 )
            {
                bearerToken = this.GenerateToken(new TokenRequest()).Result;
            }
            apiClient = new HttpClient();

            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", bearerToken);


            //var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
            //apiClient.DefaultRequestHeaders.Authorization = 
            //    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

            //apiClient.DefaultRequestHeaders.Add("Authorization", "Basic " + svcCredentials);
 
            _mapper = mapper;
        }

        public async Task<string> GenerateToken(TokenRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var authUrl = this._config.AuthUrl;

                HttpResponseMessage responseMsg = null;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + svcCredentials);

                responseMsg = await client.PostAsync(authUrl, data);

                if (responseMsg.IsSuccessStatusCode)
                {
                    var responseStr = await responseMsg.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<TokenRequestResponse>(responseStr);

                    //on receiving this token update it in app settings.json
                    //              OR
                    // if we use db table for the token we will update the token there

                    //apiClient.DefaultRequestHeaders.Accept.Clear();
                    //apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //apiClient.DefaultRequestHeaders.Authorization =
                    //    new AuthenticationHeaderValue("Bearer", Convert.ToString(response.access_token));

                    var tokenData = _zaipayDbContext.Tokens.FirstOrDefault();

                    if (tokenData is null)
                    {
                        _zaipayDbContext.Tokens.Add(new TokenModel { BearerToken = response.access_token, MaintainedAt = DateTime.UtcNow });
                        _zaipayDbContext.SaveChanges();

                    }
                    else
                    {
                        tokenData.BearerToken = response.access_token;
                        tokenData.MaintainedAt = DateTime.UtcNow;

                        _zaipayDbContext.Update(tokenData);
                        _zaipayDbContext.SaveChanges();
                    }
                    return response.access_token;
                }
                else
                    throw new Exception($"Error while generating the token(Zai pay): Response message is: {responseMsg.Content}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> CreateZaiUser(CreateUserVm request)
        {
            try
            {
                if (request.country != "AUS")
                {
                    throw new Exception("Zai is available for Australia/AUS only");
                }

                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = this.baseUrl + "/users";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url, data);

                var responseStr = await responseMsg.Content.ReadAsStringAsync();

                // string walletAccountId = await this.GetDigitalWalletAccountId(request.id);
                //string accountNumber = await this.CreateVirtualAccount(walletAccountId);

                if (responseMsg.IsSuccessStatusCode)
                {
                    var walletId = await this.GetDigitalWalletAccountId(request.TopRateId);
                    var message = await this.CreateVirtualAccount(walletId);
                    
                    return message;
                }
                else
                    throw new Exception($"Error while creating the user(Zai pay): Response message is: {responseStr}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<string> GetDigitalWalletAccountId(string id)
        {
            try
            {
                WalletAccount walletAccount = null;

                var url = _config.AssemblyPayUrl + $"users/{id}/wallet_accounts";

                var responseMessage = await apiClient.GetAsync(url);

                string responseStr = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                   walletAccount = JsonConvert.DeserializeObject<WalletAccount>(responseStr);

                    return walletAccount.wallet_accounts.id.ToString();
                }

                throw new Exception(responseStr);
                //if (walletAccount is null)
                //{
                //    throw new Exception("Wallet account with this ID does not exist");
                //}
                  

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<string> CreateVirtualAccount(string walletId)
        {
            try
            { 
                var url = _config.AssemblyPayUrl + $"wallet_accounts/{walletId}/virtual_accounts";

                HttpResponseMessage responseMsg = null;
                responseMsg = await apiClient.PostAsync(url,null);

                if (responseMsg.IsSuccessStatusCode)
                {
                    var responseStr = await responseMsg.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<VirtualAccountResponse>(responseStr);
                    
                    var payId = await this.GetPayIdDetails(walletId);
                    var bpay = await this.GetBPayIdDetails(walletId);

                    _zaipayDbContext.ZaiAccounts.Add(
                    new ZaiAccount
                    {
                        VirtualAccountId = response.id,
                        TopRateUserId = response.user_external_id,
                        ZaiAccountNumber = response.account_number,
                        ZaiWalletAccountId = response.wallet_account_id,
                        PayId = payId.wallet_accounts.npp_details.pay_id,
                        PayIdReference = payId.wallet_accounts.npp_details.reference,
                        BPayReference = bpay.wallet_accounts.bpay_details.reference,
                        Status = "Pending"
                    });

                    int result = await _zaipayDbContext.SaveChangesAsync();

                    if (result <= 0)
                    {
                        return "Unable to save zai user data";
                    }

                    return "Created Zai pay user";
                }
                else
                    throw new Exception($"Error while generating the token(Zai pay): Response message is: {responseMsg.Content}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
 
        /// <summary>
        /// This will be called when user paid to zai using his bank account
        /// </summary>
        /// <param name="virtualAccount"></param>
        public async void VirtualAccountStatus(VirtualAccountObject virtualAccount)
        {
            try
            {
                var user = _zaipayDbContext.ZaiAccounts.AsNoTracking()
                    .FirstOrDefault(x => x.VirtualAccountId == virtualAccount.id);

                user.Status = virtualAccount.eventType;

                _zaipayDbContext.ZaiAccounts.Update(user);

                await _zaipayDbContext.SaveChangesAsync().ConfigureAwait(false);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Select MFS or fincra based on business logic this webhook is same for bpay, virtual account and pay id
        /// </summary>
        /// <param name="transactions"></param>
        public async void IncomingFunds(Models.WebHooks.Transactions transactions)
        {
            try
            {

                var account = _zaipayDbContext.ZaiAccounts
                    .FirstOrDefault(x => x.VirtualAccountId == transactions.AccountId.ToString());

                if (account != null)
                {
                    //send request to seon use DebtorAccount from payin details
                    var record = _zaipayDbContext.TransactionRecords
                        .FirstOrDefault(x => x.TopRateUserId == account.TopRateUserId);
                    //  var seonResponse = await SendRequestToSeon(record);

                    _loggedInUserService.Token = record.AuthToken;
                    _loggedInUserService.UserId = record.UserId;
                    _loggedInUserService.Email = record.Email;
                    string result = string.Empty;

                    if (true)
                    {
                        //if (seonResponse.data.fraud_score < 10)
                        // {
                        CreatePayoutRequest request = new CreatePayoutRequest();
                        string desination = "";
                        request.destinationCurrency = record.DestinationCurrency;

                        request.amount = await GetRate(record.SourceCurrencyCode, record.DestinationCurrency, record.Amount.ToString());

                        request.beneficiaryId = record.BeneficiaryId;
                        request.customerReference = record.UserId;
                        request.description = "Zai payin";

                        if (record.DestinationCurrency == "NGN")
                        {
                            desination = "NGN";
                            request.sourceCurrency = desination;
                            result = await InitiatePayout(request);
                        }
                        else if (record.DestinationCurrency == "KES")
                        {
                            desination = "USD";
                            request.sourceCurrency = desination;
                            result = await InitiatePayout(request);
                        }
                        else if (record.DestinationCurrency == "XOF" || record.DestinationCurrency == "XOF"
                            || record.DestinationCurrency == "BIF" || record.DestinationCurrency == "XAF"
                            || record.DestinationCurrency == "XAF" || record.DestinationCurrency == "XOF"
                            || record.DestinationCurrency == "XOF" || record.DestinationCurrency == "SZL"
                            || record.DestinationCurrency == "ETB" || record.DestinationCurrency == "XAF"
                            || record.DestinationCurrency == "GNF" || record.DestinationCurrency == "KES"
                            || record.DestinationCurrency == "USD" || record.DestinationCurrency == "MGA"
                            || record.DestinationCurrency == "MWK" || record.DestinationCurrency == "XOF"
                            || record.DestinationCurrency == "RWF" || record.DestinationCurrency == "XOF"
                            || record.DestinationCurrency == "TZS" || record.DestinationCurrency == "XOF"
                            || record.DestinationCurrency == "UGX" || record.DestinationCurrency == "ZMW"
                            || record.DestinationCurrency == "USD" || record.DestinationCurrency == "XOF"
                            || record.DestinationCurrency == "BWP" || record.DestinationCurrency == "SSP"
                            || record.DestinationCurrency == "XAF" || record.DestinationCurrency == "GHS"
                            || record.DestinationCurrency == "XOF"
                            )
                        {
                            var mfsRequest = _mapper.Map<CreateMFSPayoutRequest>(request);
                            mfsRequest.sourceCurrency = record.SourceCurrencyCode;
                            mfsRequest.customerReference = _loggedInUserService.UserId;
                            mfsRequest.toCountry = record.ToCountry;



                            result = await SendRequestToMfs(mfsRequest, record.AuthToken);

                        }


                        //  await _transactionRecord.UpdateRecord(poliToken, "SUCCESS");
                      //  return result; send notification to user 
                        //   }
                        //else
                        //{
                        //    await DeVerifiyAccount(record.AuthToken, record.Email);

                        //    result = "Please contact the customer representative regarding seon";
                        //}
                    }
                    else
                    {
                        //await _transactionRecord.UpdateRecord(poliToken, "SEON ERROR");
                        throw new Exception("SEON not working");
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
 

        private async Task<WalletAccountNppResponse> GetPayIdDetails(string walletId)
        {
            try
            {
                WalletAccountNppResponse nppDetails = null;

                var url = _config.PromisePayUrl + $"wallet_accounts/{walletId}/npp_details";

                var responseMessage = await apiClient.GetAsync(url);

                string responseStr = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    nppDetails = JsonConvert.DeserializeObject<WalletAccountNppResponse>(responseStr);

                    return nppDetails;
                }

                throw new Exception(responseStr);
                //if (walletAccount is null)
                //{
                //    throw new Exception("Wallet account with this ID does not exist");
                //}


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async Task<WalletAccountBpayResponse> GetBPayIdDetails(string walletId)
        {
            try
            {
                WalletAccountBpayResponse nppDetails = null;

                var url = _config.PromisePayUrl + $"wallet_accounts/{walletId}/bpay_details";

                var responseMessage = await apiClient.GetAsync(url);

                string responseStr = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    nppDetails = JsonConvert.DeserializeObject<WalletAccountBpayResponse>(responseStr);

                    return nppDetails;
                }

                throw new Exception(responseStr);
                //if (walletAccount is null)
                //{
                //    throw new Exception("Wallet account with this ID does not exist");
                //}


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<ZaiAccount> GetZaiUser(string topRateId)
        {
            try
            {
                var user = await _zaipayDbContext.ZaiAccounts.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.TopRateUserId == topRateId);

                return user;
                 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ZaiAccount> InitiateTransaction(InitateTransaction initateTransaction)
        {
            try
            {
                var zaiAccount = await this.GetZaiUser(initateTransaction.TopRateUserId);
                if (zaiAccount is null)
                {
                    throw new Exception("Zai account for this user does not exist");
                }

                var record = new TransactionRecord();

                record.TransactionStatus = "Initiated";
                record.SourceCurrencyCode = initateTransaction.SourceCurrency;
                record.DestinationCurrency = initateTransaction.DestinationCurrency;
                record.ToCountry = initateTransaction.ToCountry;
                record.TransactionRefNo = initateTransaction.TopRateUserId;
                record.Amount = initateTransaction.Amount;
                record.BeneficiaryId = initateTransaction.BeneficaryId.ToString();
                record.UserId = initateTransaction.UserId.ToString();
                record.TopRateUserId = initateTransaction.TopRateUserId.ToString();
                record.Email = _loggedInUserService.Email;
                record.AuthToken = _loggedInUserService.Token;

                _zaipayDbContext.TransactionRecords.Add(record);
                int isSuccess = await _zaipayDbContext.SaveChangesAsync();
                if (isSuccess > 0)
                {
                    return zaiAccount;
                }
                throw new Exception($"Zai error: Unable to store transaction record {isSuccess}");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<string> GetRate(string source, string destination, string amount)
        {
            var url = _internalConfig.ConversionRateUrl + $"Get/CurrencyConverterRate?SendingCurrency={source}&ReceivingCurrency={destination}&Amount={amount}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string[] token = _loggedInUserService.Token.Split(" ");

            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token[0]);

            var responseMsg = await client.GetAsync(url);
            var responseStr = await responseMsg.Content.ReadAsStringAsync();

            if (responseMsg.IsSuccessStatusCode)
            {
                var rate = JsonConvert.DeserializeObject<ConvversionResponse>(responseStr);
                return rate.data.rate;
            }
            else
            {
                throw new Exception(responseStr.ToString());
            }
        }

        private async Task<UserProfile> GetUserProfile(string authToken)
        {
            var url = this.baseUrl + "Account/GetProfile";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);

            var responseMessage = await client.GetAsync(url);

            string responseStr = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(responseStr);

                return userProfile;
            }
            throw new Exception(responseStr);
        }

        private async Task<string> SendRequestToMfs(CreateMFSPayoutRequest request, string userToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", userToken);

            //string url = @"http://188.213.168.50:5015/api​/MFS​/CreateMfsPayout";
            string url = _internalConfig.MfsUrl + @"CreateMfsPayout";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var mfs = await response.Content.ReadAsStringAsync();
                return mfs;
            }
            else
            {
                throw new Exception(response.ToString());
            }

        }

        private async Task<string> InitiatePayout(CreatePayoutRequest payoutRequest)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string[] token = _loggedInUserService.Token.Split(" ");

                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token[0]);

                string url = _internalConfig.FincraUrl + @"CreatePayout";

                var json = JsonConvert.SerializeObject(payoutRequest);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMsg = await client.PostAsync(url, data);
                var responseStr = await responseMsg.Content.ReadAsStringAsync();

                if (responseMsg.IsSuccessStatusCode)
                {
                    return responseStr;
                }
                else
                {
                    throw new Exception(responseStr.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<SeonResponse> SendRequestToSeon(TransactionRecord record)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", record.AuthToken);

            string url = _internalConfig.SeonUrl + @"Fraud";

            var profile = await this.GetUserProfile(record.AuthToken);

            var json = JsonConvert.SerializeObject(new
            {
                Ip = _loggedInUserService.Ip,
                transaction_id = record.TransactionRefNo,
                email = profile.Email,
                phone_number = profile.PhoneNumber,
                user_fullname = profile.FirstName + " " + profile.LastName,
                user_id = profile.CustomerId,
                user_dob = profile.DateOfBirth.ToString("yyyy-MM-dd"),
                user_country = profile.Country,
                user_city = profile.City,
                user_region = profile.State,
                user_zip = profile.PostalCode,
                session = record.Id.ToString(),
                transaction_amount = record.Amount.ToString(),
                transaction_currency = record.DestinationCurrency,
                custom_fields = new
                {
                    tr_source = "Poli payment",
                    country_of_birth = profile.CountryOfBirth,
                }

            });
            var seonData = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync(url, seonData);
            var data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var seonResponse = JsonConvert.DeserializeObject<SeonResponse>(data);
                return seonResponse;
            }
            else
            {
                throw new Exception(data);
            }
        }

        public List<TokenModel> GetToken()
        {
            return _zaipayDbContext.Tokens.ToList();
        }
    }
}
