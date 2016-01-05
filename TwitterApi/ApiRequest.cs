using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace TwitterApi
{
    public abstract class ApiRequest<T>
    {
        public T CachedValue
        {
            get { return cachedValue; }
        }

        protected ApiRequest(ApiKey key)
        {
            this.key = key;
        }

        public void SendRequest()
        {
            oauthProperties.Clear();
            bodyProperties.Clear();
            urlProperties.Clear();

            PackProperties();

            StringBuilder baseStringBuilder = new StringBuilder();
            SortedDictionary<string, string> sortedProperties = new SortedDictionary<string, string>();

            foreach (KeyValuePair<string, string> pair in oauthProperties)
                sortedProperties.Add(pair.Key, pair.Value);
            foreach (KeyValuePair<string, string> pair in urlProperties)
                sortedProperties.Add(pair.Key, pair.Value);
            foreach (KeyValuePair<string, string> pair in bodyProperties)
                sortedProperties.Add(pair.Key, pair.Value);

            // Append the start of our message body
            baseStringBuilder.Append(string.Format("{0}&", Method));
            baseStringBuilder.Append(string.Format("{0}&", Uri.EscapeDataString(ResourceUrl)));

            foreach (KeyValuePair<string, string> propertyPair in sortedProperties)
            {
                baseStringBuilder.Append(Uri.EscapeDataString(string.Format("{0}={1}&", Uri.EscapeDataString(propertyPair.Key), Uri.EscapeDataString(propertyPair.Value))));
            }

            string baseString = baseStringBuilder.ToString(0, baseStringBuilder.Length - 3);
            string signingKey = string.Format("{0}&{1}", Uri.EscapeDataString(key.ConsumerSecret), Uri.EscapeDataString(key.TokenSecret));

            HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(signingKey));
            byte[] hashedData = hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString));
            string oauthSigniture = Convert.ToBase64String(hashedData);

            ServicePointManager.Expect100Continue = false;

            AddOAuthProperty("oauth_signature", oauthSigniture);

            StringBuilder authorizationHeaderBuilder = new StringBuilder();
            authorizationHeaderBuilder.Append("OAuth");

            foreach (KeyValuePair<string, string> pair in oauthProperties)
            {
                authorizationHeaderBuilder.Append(" ");
                authorizationHeaderBuilder.Append(Uri.EscapeDataString(pair.Key));
                authorizationHeaderBuilder.Append("=\"");
                authorizationHeaderBuilder.Append(Uri.EscapeDataString(pair.Value));
                authorizationHeaderBuilder.Append("\"");
                authorizationHeaderBuilder.Append(",");
            }

            string authorizationHeader = authorizationHeaderBuilder.ToString(0, authorizationHeaderBuilder.Length - 1);

            StringBuilder urlExtensionBuilder = new StringBuilder(urlProperties.Count > 0 ? "?" : "");
            foreach (KeyValuePair<string, string> pair in urlProperties)
            {
                urlExtensionBuilder.Append(Uri.EscapeDataString(pair.Key));
                urlExtensionBuilder.Append("=");
                urlExtensionBuilder.Append(Uri.EscapeDataString(pair.Value));
                urlExtensionBuilder.Append("&");
            }

            string urlExtension = urlExtensionBuilder.Length > 0 ? urlExtensionBuilder.ToString(0, urlExtensionBuilder.Length - 1) : "";
            string fullUrl = ResourceUrl + urlExtension;

            StringBuilder bodyStringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in bodyProperties)
            {
                bodyStringBuilder.Append(Uri.EscapeDataString(pair.Key));
                bodyStringBuilder.Append("=");
                bodyStringBuilder.Append(Uri.EscapeDataString(pair.Value));
                bodyStringBuilder.Append("&");
            }

            string body = bodyStringBuilder.Length > 0 ? bodyStringBuilder.ToString(0, bodyStringBuilder.Length - 1) : "";

            HttpWebRequest request = WebRequest.Create(fullUrl) as HttpWebRequest;
            request.Headers.Add("Authorization", authorizationHeader);
            request.Method = Method;
            request.ContentType = "application/x-www-form-urlencoded";

            if (Method == "POST")
            {
                using (Stream bodyStream = request.GetRequestStream())
                {
                    byte[] bodyData = new ASCIIEncoding().GetBytes(body);
                    bodyStream.Write(bodyData, 0, bodyData.Length);
                }
            }

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    ProcessResponse(responseStream);
                }
            }
            catch(WebException e)
            {
                Stream errorStream = e.Response.GetResponseStream();
                StreamReader reader = new StreamReader(errorStream);
                string error = reader.ReadToEnd();
                throw;
            }
        }

        protected abstract string ResourceUrl
        {
            get;
        }

        protected abstract string Method
        {
            get;
        }

        private void ProcessResponse(Stream stream)
        {
            DataContractJsonSerializer deSerializer = new DataContractJsonSerializer(typeof(T));
            cachedValue = (T)deSerializer.ReadObject(stream);
        }

        protected void AddOAuthProperty(string key, string value)
        {
            oauthProperties.Add(key, value);
        }

        protected void AddUrlProperty(string key, string value)
        {
            urlProperties.Add(key, value);
        }

        protected void AddBodyProperty(string key, string value)
        {
            bodyProperties.Add(key, value);
        }

        protected virtual void PackProperties()
        {
            string oauthNonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            System.TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string oauthTimestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            AddOAuthProperty("oauth_consumer_key", key.ConsumerKey);
            AddOAuthProperty("oauth_nonce", oauthNonce);
            AddOAuthProperty("oauth_signature_method", SignitureMethod);
            AddOAuthProperty("oauth_token", key.Token);
            AddOAuthProperty("oauth_timestamp", oauthTimestamp);
            AddOAuthProperty("oauth_version", OauthVersion);
        }


        private SortedDictionary<string, string> oauthProperties = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> bodyProperties = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> urlProperties = new SortedDictionary<string, string>();

        private ApiKey key;

        private T cachedValue = default(T);

        private const string OauthVersion = "1.0";
        private const string SignitureMethod = "HMAC-SHA1";
    }
}
