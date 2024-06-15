using Application;
using DataAcess;
using Implementation.UseCases;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class JwtAuthorization : EfUseCase,IApplicationActorProvider
    {
        private string authorizationHeader;
        public JwtAuthorization(string header,ASPContext context) : base(context)
        {
            this.authorizationHeader = header;

        }

        public IApplicationActor GetActor()
        {
            if (authorizationHeader == null|| authorizationHeader=="")
            {
                return new UnathorizedActor();
            }
            try
            {
                var token = authorizationHeader.Split(' ').Last();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken == null)
                {
                    return new UnathorizedActor();
                }
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");
                int userIdClaimInt = userIdClaim != null ? Convert.ToInt32(userIdClaim.Value) : 0;
                var userFirstName = jwtToken.Claims.FirstOrDefault(c => c.Type == "FirstName");
                var userLasttName = jwtToken.Claims.FirstOrDefault(c => c.Type == "LastName");
                var userEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email");
                var userAllowedCases = jwtToken.Claims.FirstOrDefault(c => c.Type == "AllowedUseCases");
                var userAllowedCasesDeseralize = JsonConvert.DeserializeObject<IEnumerable<int>>(userAllowedCases.Value);
                if(userIdClaim == null) return new UnathorizedActor();
                return new Actor
                {
                    Id=userIdClaimInt,
                    FirstName = userFirstName.Value,
                    LastName = userLasttName.Value,
                    Email = userEmail.Value,
                    AllowedUseCases = userAllowedCasesDeseralize,
                };
            }
            catch(Exception ex)
            {
                return new UnathorizedActor();
            }



        }
    }
}
