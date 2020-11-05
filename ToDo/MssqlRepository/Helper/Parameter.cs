using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MssqlRepository.Helper
{
    public class Parameter : IParameter
    {
        private readonly IHttpContextAccessor _context;
        public Parameter(IHttpContextAccessor context)
        {
            _context = context;
            UserId = GetUserId();
        }
        public string UserId { get; set; }

        private string GetUserId()
        {
            if (_context == null)
                return "allow";
            var claims = _context.HttpContext.User.Identity as ClaimsIdentity;
            var id = claims.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            return id;
        }
    }
}
