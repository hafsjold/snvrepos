using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace medlem3060functions
{
    public enum enumTask
    {
        ReceiveFilesFromPBS = 1,
        SendKontingentFileToPBS,
        KontingentNyeMedlemmer,
        SendEmailRykker,
        UpdateMedlemStatus,
        SendEmailAdvis,
        UpdateKanSlettes,
        SendEmailKviteringer,
        OpdaterTransUserData,
        TimeTriggerTemplate
    }

    public static class nytmedlem
    {
        [FunctionName("nytmedlem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            try
            {
                var appParameter = "ServiceBusConnectionString";
                string ServiceBusConnectionString = config[$"{appParameter}"];

                var queueClient = new QueueClient(ServiceBusConnectionString, "medlemqueue");
                var msg = new Message(Encoding.UTF8.GetBytes(enumTask.KontingentNyeMedlemmer.ToString()))
                {
                    ScheduledEnqueueTimeUtc = DateTime.UtcNow.AddSeconds(180)
                };
                queueClient.SendAsync(msg).Wait();
            }
            catch { }

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
