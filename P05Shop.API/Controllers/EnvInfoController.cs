using Microsoft.AspNetCore.Mvc;

namespace P05Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvInfoController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetEnvInfo()
        {
            var envInfo = new
            {
                EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                MachineName = Environment.MachineName,
                OSVersion = Environment.OSVersion.ToString(),
                ProcessorCount = Environment.ProcessorCount,
                DotNetVersion = Environment.Version.ToString(),
                CurrentDirectory = Environment.CurrentDirectory,
                UserName = Environment.UserName,
                UserDomainName = Environment.UserDomainName,
                Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
                Is64BitProcess = Environment.Is64BitProcess,
                MyCustomValue = Environment.GetEnvironmentVariable("MY_CUSTOM_ENV_VAR")
            };
            return Ok(envInfo);
        }
    }
}
