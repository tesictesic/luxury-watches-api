using DataAcess;
using Domain;
using Implementation;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Core
{
    public class JWTManager
    {
        private ASPContext context;
        public JWTManager(ASPContext context)
        {
                this.context = context;
        }
        public string MakeToken(string email, string password)
        {

            User user=context.Users.Where(x=>x.Email==email).FirstOrDefault();
            SecurityToken token = null;
            if (user!=null) { 
                bool isPaswordValid=BCrypt.Net.BCrypt.Verify(password,user.Password);
                if (isPaswordValid)
                {
                    string issuer = "luxury_watches_api";
                    string secretkey = "31kldkasldlaskdlkalk3241l414kldkasldklaskdlsakdlkasl34214214";
                    var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString(),ClaimValueTypes.String,issuer),
                new Claim(JwtRegisteredClaimNames.Iss,"luxury_watches_api",ClaimValueTypes.String,issuer),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),ClaimValueTypes.Integer),
                new Claim("UserId",user.Id.ToString(),ClaimValueTypes.String,issuer),
                new Claim("FirstName",user.First_Name,ClaimValueTypes.String,issuer),
                new Claim("LastName",user.Last_Name,ClaimValueTypes.String,issuer),
                new Claim("Email",user.Email,ClaimValueTypes.String,issuer),
                new Claim("AllowedUseCases",JsonConvert.SerializeObject(user.UserUseCases.Select(y=>y.UseCaseId)),ClaimValueTypes.String,issuer),

            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var now = DateTime.UtcNow;
                     token = new JwtSecurityToken(
                        issuer: issuer,
                        audience: "Any",
                        claims: claims,
                        notBefore: now,
                        expires: now.AddSeconds(600),
                        signingCredentials: credentials
                        );
                   
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                 new UnathorizedActor();
            }
            return new JwtSecurityTokenHandler().WriteToken(token);



        }
        
    }
}
