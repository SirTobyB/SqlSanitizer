using System;
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

            // Order the parameters by the character length of the parameter names (to get the parameters with the longest names first), 
            // to avoid replacing the parameter value for '@column10' with the value for '@column1' (as an example).
            Array.Sort(request.Parameter, (x, y) => y.Name.Length.CompareTo(x.Name.Length));

            foreach (var parameter in request.Parameter)
            {
                if (string.IsNullOrWhiteSpace(parameter.Value))
                {
                    continue;
                }
                
                formattedSqlQuery = formattedSqlQuery.Replace(parameter.Name, parameter.Value);
            }
            
            var payloadData = new Dictionary<string, string>
            {
                { "reindent", Convert.ToInt32(request.Reindent).ToString() },
                { "indent_width", request.IndentWidth.ToString() },
                { "sql", formattedSqlQuery },
                { "strip_comments", Convert.ToInt32(request.StripComments).ToString()}
            };
            
            if (request.IdentifierCase != Casing.Default)
            {
                payloadData.Add("identifier_case", request.IdentifierCase.ToString().ToLower());
            }
            
            if (request.KeywordCase != Casing.Default)
            {
                payloadData.Add("keyword_case", request.KeywordCase.ToString().ToLower());
            }

            var httpClient = _clientFactory.CreateClient();
            var payload = new FormUrlEncodedContent(payloadData);

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