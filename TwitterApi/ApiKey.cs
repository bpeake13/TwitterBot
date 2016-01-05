using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApi
{
    public class ApiKey
    {
        public string ConsumerKey
        {
            get { return consumerKey; }
        }

        public string ConsumerSecret
        {
            get { return consumerSecret; }
        }

        public string Token
        {
            get { return token; }
        }

        public string TokenSecret
        {
            get { return tokenSecret; }
        }

        public ApiKey(string consumerKey, string consumerSecret, string token, string tokenSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.token = token;
            this.tokenSecret = tokenSecret;
        }

        private string consumerKey;
        private string consumerSecret;
        private string token;
        private string tokenSecret;
    }
}
