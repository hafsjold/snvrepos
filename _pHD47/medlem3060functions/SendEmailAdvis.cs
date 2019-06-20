using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace medlem3060functions
{
    public static class SendEmailAdvis
    {
        [FunctionName("SendEmailAdvis")]
        public static void Run([TimerTrigger("0 01 18,21 * * *")]TimerInfo myTimer,
                               [ServiceBus("medlemqueue", Connection = "ServiceBusConnectionString")]out Message msg,
                                ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            msg = new Message(Encoding.UTF8.GetBytes(enumTask.SendEmailAdvis.ToString()))
            {
                ScheduledEnqueueTimeUtc = DateTime.UtcNow.AddSeconds(15)
            };
        }
    }
}
