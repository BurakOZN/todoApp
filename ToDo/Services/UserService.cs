using ApiModels;
using Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MssqlRepository;
using MssqlRepository.ReturnModel;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<BaseResponse> AddUser(AddUserRequest model)
        {
            var user = new User();
            user.Username = model.Username;
            user.Password = HashManager.GetHash(model.Password);
            var result = await _userRepository.Add(user);
            return result;
        }
        public async Task<BaseResponse> Login(LoginInfo loginInfo)
        {
            var userResult = await _userRepository.Get(x => x.Username == loginInfo.Username);
            if (userResult.State != State.Success)
                return userResult;

            var users = (List<User>)userResult.Result;
            if (!users.Any())
                return new ErrorResponse(State.NotFound, "Not Found", "Invalid username");

            var user = users.First();
            if (user.Password != HashManager.GetHash(loginInfo.Password))
                return new ErrorResponse(State.ProcessError, "Password Error", "Password is incorrect");

            var loginResponse = new LoginResponse();
            loginResponse.Token = BuildToken(user.Id);
            return new SuccessResponse(loginResponse);
        }
        private string BuildToken(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

            var token = new JwtSecurityToken(
              _configuration["JwtToken:Issuer"],
              _configuration["JwtToken:Issuer"],
              expires: DateTime.Now.AddHours(8),
              signingCredentials: creds,
              claims: claims
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
