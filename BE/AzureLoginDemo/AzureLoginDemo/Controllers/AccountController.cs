using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace AzureLoginDemo.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("LoginAzure")]
        public async Task<IActionResult> LoginAzure([FromBody] LoginAzureRequest request)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, request.AccessToken);

            var getUserInfoResponse = await client.PostAsync("https://graph.microsoft.com/oidc/userinfo", null);

            getUserInfoResponse.EnsureSuccessStatusCode();

            // If response status code is not Ok => throw err or something

            // If resposne is Ok => Generate JWT Token here

            return Ok(new LoginAzureResponse
            {
                Token = "Sample Token"
            });
        }
    }

    public class LoginAzureRequest
    {
        public string AccessToken { get; set; }
    }

    public class LoginAzureResponse
    {
        public string Token { get; set; }
    }
}
