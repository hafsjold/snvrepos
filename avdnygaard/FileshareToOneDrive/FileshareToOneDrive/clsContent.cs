/*
 * https://apps.dev.microsoft.com
 *
 *  Admin Consent:2adaae07-abcc-4271-a6b8-c22f61984c1c
 * https://login.microsoftonline.com/common/adminconsent?client_id=2adaae07-abcc-4271-a6b8-c22f61984c1c&state=12345&redirect_uri=http://localhost
 */
using Microsoft.Graph;
using Microsoft.Identity.Client;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FileshareToOneDrive
{
    public class clsContent
    {
        const string clientId = "2adaae07-abcc-4271-a6b8-c22f61984c1c";
        const string AuthorityFormat = "https://login.microsoftonline.com/{0}/v2.0";
        const string tenantId = @"18d360c2-3c65-46df-a7c8-070227d36e0b";
        const string clientSecret = @"ximziaGCP86{xNUSG352^-$";
        const string redirectUri = @"http://localhost";
        const string MSGraphScope = "https://graph.microsoft.com/.default";
        const string appCachId = "3ad5b2df-1234-4b4d-96ea-8013cc6931bd";


        public static GraphServiceClient graphClient;
        public static NLog.Logger logger = null;

        public static MSALCache cliTokenCache = new MSALCache(clientId);
        public static MSALCache appTokenCache = new MSALCache(appCachId);

        public static DateTime TokenAcquired = DateTime.Now;

        public static dbSharePointEntities m_db = null;

        public static void initlogger()
        {
            if (logger == null)
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);
                fileTarget.FileName = "${basedir}/Logs/file.txt";
                //fileTarget.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}";
                fileTarget.Layout = "${longdate}|${level:uppercase=true}|${message}";
                var rule = new LoggingRule("*", NLog.LogLevel.Debug, fileTarget);
                config.LoggingRules.Add(rule);
                LogManager.Configuration = config;
                logger = LogManager.GetCurrentClassLogger();
            }
        }

        public static void init()
        {
            m_db = new dbSharePointEntities();
            m_db.tblPath.Load();

            initlogger();
            logger.Info("TestLog");

            //LogInAsync().Wait();
            DamonLogInAsync().Wait();
        }

        public static async Task RenewAccessToken()
        {
            if (DateTime.Now.Subtract(TokenAcquired).TotalSeconds >= 300)
            {
                clsContent.DamonLogInAsync().Wait();
            };
        }

        public static async Task DamonLogInAsync()
        {
            AuthenticationResult result;

            var daemonClient = new ConfidentialClientApplication(
                    clientId
                    , string.Format(AuthorityFormat, tenantId)
                    , redirectUri
                    , new ClientCredential(clientSecret)
                    , cliTokenCache.GetMsalCacheInstance()
                    , appTokenCache.GetMsalCacheInstance()
                );
            try
            {
                result = await daemonClient.AcquireTokenForClientAsync(new[] { MSGraphScope });
                //Console.WriteLine(result.AccessToken);

                graphClient = new GraphServiceClient(
                    new DelegateAuthenticationProvider(
                        async (requestMessage) =>
                        {
                            // Append the access token to the request.
                            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);

                            // Some identifying header
                            requestMessage.Headers.Add("SampleID", "aspnet-connect-sample");
                        }
                    )
                );
                TokenAcquired = DateTime.Now;
            }

            // Unable to retrieve the access token silently.
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.Error(e.Message);
            }
        }





    }
}
