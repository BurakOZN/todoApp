using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ToDo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            var result = await _userService.Login(loginInfo);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUser)
        {
            var result = await _userService.AddUser(addUser);
            return Ok(result);
        }
    }
}
