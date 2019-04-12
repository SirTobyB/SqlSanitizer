using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlSanitizer.Api.Models;

namespace SqlSanitizer.Api.Controllers
{
    [Route("api/[controller]")]
    public class FormatController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public FormatController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        [HttpPost]
        public async Task<IActionResult> Format([FromBody] FormatRequest request)
        {
            var formattedSqlQuery = request.SqlQuery;
            
            foreach (var charToRemove in request.CharsToRemove)
            {
                formattedSqlQuery = formattedSqlQuery.Replace(charToRemove, "");
            }

            var httpClient = _clientFactory.CreateClient();
            var payload = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("reindent", "1"),
                new KeyValuePair<string, string>("sql", formattedSqlQuery), 
            });

            
            var response =
                await httpClient.PostAsync("https://sqlformat.org/api/v1/format", payload);

            if (!response.IsSuccessStatusCode)
                return BadRequest();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FormatResponse>(content).Result;
            
            return Ok(new {sql = result});
        }
    }
}