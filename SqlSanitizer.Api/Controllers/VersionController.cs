using System;
using Microsoft.AspNetCore.Mvc;

namespace SqlSanitizer.Api.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        [HttpGet]
        public IActionResult GetVersion()
        {
            var version = Environment.GetEnvironmentVariable("VERSION") ?? " ";

            return Ok(version);
        }
    }
}