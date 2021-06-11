using AuthorizationAPI.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class AuthRepo
    {
        private readonly IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthRepo));
        private readonly IRepo _repo;
        public AuthRepo(IConfiguration config, IRepo repo)
        {
            _config = config;
            _repo = repo;
        }

        public string GenerateJSONWebToken(Credentials userInfo)
        {
            _log4net.Info("Token Is Generated!");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Credentials AuthenticateUser(Credentials login)
        {
            _log4net.Info("Validating the User!");

            //Validate the User Credentials 
            Credentials usr = _repo.GetCred(login);
            return usr;
        }
    }
}
