using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nsPbs3060.PayPalServiceReference;

namespace nsPbs3060
{
    public class clsPayPal
    {
        public clsPayPal() { }

        private CustomSecurityHeaderType credentials
        {
            get 
            {
                return new CustomSecurityHeaderType
                {
                    Credentials = new UserIdPasswordType
                    {
                        Username = @"regnskab_api1.puls3060.dk",
                        Password = @"CGUQ4L26WKN5SJ8D",
                        Signature = @"ABbgrojhuYSlaiWja8uyn14cPmQDA86Wix5yMj5ncIRQiRBdj.NRRgZz"
                    }
                };
            }
        }
 


        public void testPayPal()
        {
            var client = new PayPalAPIInterfaceClient();

            TransactionSearchReq request = new TransactionSearchReq();
            request.TransactionSearchRequest = new TransactionSearchRequestType();
            request.TransactionSearchRequest.StartDate = DateTime.UtcNow.AddDays(-360);
            request.TransactionSearchRequest.TransactionClass = PaymentTransactionClassCodeType.All;
            request.TransactionSearchRequest.Version = "124.0";
            request.TransactionSearchRequest.TransactionID = "7XJ58176Y65857210";
            var pw = credentials;
            TransactionSearchResponseType transactionSearchResponseType = client.TransactionSearch(ref pw, request);
        }

        public PaymentTransactionSearchResultType getPayPalTransaction(string pTransactionID)
        {
            TransactionSearchReq request = new TransactionSearchReq();
            request.TransactionSearchRequest = new TransactionSearchRequestType();
            request.TransactionSearchRequest.StartDate = DateTime.UtcNow.AddDays(-360);
            request.TransactionSearchRequest.TransactionClass = PaymentTransactionClassCodeType.All;
            request.TransactionSearchRequest.Version = "124.0";
            request.TransactionSearchRequest.TransactionID = pTransactionID;
            var pw = credentials;
            var client = new PayPalAPIInterfaceClient();
            TransactionSearchResponseType trans = client.TransactionSearch(ref pw, request);
            if (trans.Ack == AckCodeType.Success)
            {
                if (trans.PaymentTransactions.Count() == 1)
                {
                    return trans.PaymentTransactions[0];
                }
            }
            return null;
        }
    }
}
