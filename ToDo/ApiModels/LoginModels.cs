using System;

namespace ApiModels
{
    public class LoginInfo
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
