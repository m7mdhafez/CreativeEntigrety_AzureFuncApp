using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace dentsu.Function
{
    public class RemoveEmailhtml
    {
        private readonly ILogger<RemoveEmailhtml> _logger;

        public RemoveEmailhtml(ILogger<RemoveEmailhtml> logger)
        {
            _logger = logger;
        }

        [Function("RemoveEmailhtml")]
        // public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        // {
        //     _logger.LogInformation("C# HTTP trigger function processed a request.");
        //     return new OkObjectResult("Welcome to Azure Functions!");
        // }

         public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req, ILogger log) 
        {
            log.LogInformation("HttpWebhook triggered");

            // Parse query parameter
            string emailBodyContent = await new StreamReader(req.Body).ReadToEndAsync();

            // Replace HTML with other characters
            string updatedBody = Regex.Replace(emailBodyContent, "<.*?>", string.Empty);
            updatedBody = updatedBody.Replace("\\r\\n", " ");
            updatedBody = updatedBody.Replace(@"&nbsp;", " ");

            // Return cleaned text
            return new OkObjectResult(new { updatedBody });
        }
    }
}
