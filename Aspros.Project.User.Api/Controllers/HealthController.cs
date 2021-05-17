using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Aspros.Project.User.Api.Controllers
{
    [Route("[controller]")]
    public class HealthController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get(bool useLocalFabio)
        {
            if (!useLocalFabio)
                return Ok("ok");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("KeepAlive", "True");
                client.DefaultRequestHeaders.Add("Timeout", "5");
                var message = await client.GetAsync("http://127.0.0.1:9998/health");
                if (message.IsSuccessStatusCode) return Ok("ok");

                return BadRequest("fabio error");
            }
        }
    }
}