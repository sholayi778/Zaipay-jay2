using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Zaipay.Service
{
    public interface ILoggedInUserService
    {
        string UserId { set; get; }
        string Email { set; get; }
        string Role { get; }
        string Token { set; get; }
        string Ip { get; }

    }
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            Token = httpContextAccessor.HttpContext?.Request?.Headers["Authorization"];
            Ip = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            string[] tokens = httpContextAccessor.HttpContext?.Request?.Headers["Authorization"].ToString()?.Split(" ");

          //  if (tokens != null && tokens[0] != "")
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(Token);
                var tokenS = jsonToken as JwtSecurityToken;

//                Token = tokens[0];

                foreach (var claim in tokenS.Claims)
                {
                    if (claim.Type == "Id")
                    {
                        UserId = claim.Value;
                        //break;
                        //continue;
                    }
                    if (claim.Type == "Email")
                    {
                        Email = claim.Value;
                        //break;
                        //continue;
                    }
                }
            }
        }

        public string UserId { set; get; }
        public string Email { set; get; }
        public string Role { get; }
        public string Token { set; get; }
        public string Ip { get; }


    }
}
