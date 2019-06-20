using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace medlem3060functions
{
    public static class KontingentNyeMedlemmer
    {
        [FunctionName("KontingentNyeMedlemmer")]
        public static void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer,
                               [ServiceBus("medlemqueue", Connection = "ServiceBusConnectionString")]out Message msg,
                                ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            msg = new Message(Encoding.UTF8.GetBytes(enumTask.KontingentNyeMedlemmer.ToString()))
            {
                ScheduledEnqueueTimeUtc = DateTime.UtcNow.AddSeconds(15)
            };
        }
    }
}
