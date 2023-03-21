using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zaipay.Configurations;
using Zaipay.Models;

namespace Zaipay.Service
{

    public interface IHangfire 
    {
        Task UpdateToken();
    }
    public class HangfireService : IHangfire
    {
        private readonly IZaiPayPlatform _platform;
        private readonly ZaipayDbContext _zaipayDbContext;
        public HangfireService(IZaiPayPlatform zaiPayPlatform, ZaipayDbContext zaipayDbContext)
        {
            _platform = zaiPayPlatform;
            _zaipayDbContext = zaipayDbContext;
        }

        public async Task UpdateToken()
        {
            var token = await _platform.GenerateToken(new TokenRequest());

            
            //var config = new ConfigurationBuilder()
            //                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //                .AddJsonFile("appsettings.json")
            //                .Build()
            //                .Get<Config>();

            //config.ZaiConfig.BearerToken = token;
            //var jsonWriteOptions = new JsonSerializerOptions()
            //{
            //    WriteIndented = true
            //};
            //jsonWriteOptions.Converters.Add(new JsonStringEnumConverter());

            //var newJson = JsonSerializer.Serialize(config, jsonWriteOptions);

            //var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            //File.WriteAllText(appSettingsPath, newJson);
        }
    }
}
