using System.Collections.Generic;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtProperties.AuthScheme)]
    public class AccessController : ControllerBase
    {
        public IActionResult Index()
        {
            var example = new List<string>
            {
                $"Access",
                $"From",
                $"Authenticated"
            };
            return new JsonResult(example);
        }
    }
}