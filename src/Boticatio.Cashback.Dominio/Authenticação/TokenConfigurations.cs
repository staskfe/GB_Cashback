using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Boticatio.Cashback.Dominio.Authenticação
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }


        public object CreateToken(string email, SigningConfigurations signingConfigurations)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                   new GenericIdentity(email, "Login"),
                   new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, email)
                   }
               );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(this.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = this.Issuer,
                Audience = this.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            var token = handler.WriteToken(securityToken);

            return new
            {
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
