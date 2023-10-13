using Common;
using Entites.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSettings _siteSettings;
        private readonly SignInManager<User> _signInManager;

        public JwtService(IOptionsSnapshot<SiteSettings> settings, SignInManager<User> SignInManager)
        {
            _siteSettings = settings.Value;
            _signInManager = SignInManager;
        }

        public async Task<string> GenerateAsync(User user)
        {
            var secretyKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey); //Longer than 16 Character or more 
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretyKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.Encryptkey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await _getClaimsAsync(user);
            var Descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSettings.JwtSettings.Issuer,
                Audience = _siteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = SigningCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)

            };


            //این روش برای غیر فعال کردن اوتوماتیک تبیدل کلیم به کلیم های jwt
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();


            var TokenHandler = new JwtSecurityTokenHandler();
            var securityToken = TokenHandler.CreateToken(Descriptor);
            var jwt = TokenHandler.WriteToken(securityToken);

            //var result = await _signInManager.PasswordSignInAsync(user.PhoneNumber, "Amin1376", true, true);

            return jwt;
        }
        private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
        {
            //مشابه کلیم ها ولی مال jwt
            //JwtRegisteredClaimNames.Sub

            //var secuirtyStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            //var list = new List<Claim> {
            //new Claim(ClaimTypes.Name , user.Email),
            //new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
            //new Claim(ClaimTypes.MobilePhone , user.PhoneNumber),
            //new Claim(secuirtyStampClaimType , user.SecurityStamp.ToString()),

            //};
            //var roles = new Role[] { new Role { Name = "Admin" } };
            //foreach (var item in roles)
            //{
            //    list.Add(new Claim(ClaimTypes.Role, item.Name));
            //}
            ////list.Add(new Claim("x", "y"));

            //return list;

            var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            // add custom clasims
            var list = new List<Claim>(result.Claims);
            list.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            return list;

        }
    }
}
