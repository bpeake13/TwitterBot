using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitterApi;
using TwitterApi.Objects;

namespace IWillTweetBotSoon
{
    class Program
    {
        static void Main(string[] args)
        {
            const string consumerKey = "fCgzzKtIveNeZTVsD5wKE9VvT";
            const string consumerSecret = "etwJ35k2eAnYkfKNeAEYZLE1d66bO0G1X389ZC2GNzTkYJMhtV";
            const string accessToken = "1599259255-qauXuOwgdOHs6tlIZe78E55GaY4hGstMxZ6QmWo";
            const string accessTokenSecret = "gFYhri2Mtgb7Dgdcv23ZA9s6RfAcNmWJpRdL1sZM1qpBq";

            ApiKey key = new ApiKey(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            UserSearchRequest userSearchRequest = new UserSearchRequest(key, "BennyPeake");
            userSearchRequest.SendRequest();

            Thread.Sleep(5000);
        }

        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
