using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Objects;

namespace TwitterApi
{
    public class SearchRequest : ApiRequest<TweetSearchResults>
    {
        public SearchRequest(ApiKey key, string searchQuerey, int resultCount = 20) : base(key)
        {
            this.searchQuerey = searchQuerey;
            this.resultCount = resultCount;
        }

        public SearchRequest(ApiKey key, string searchQuerey, DateTime beforeDate, int resultCount = 20) : this(key, searchQuerey, resultCount)
        {
            this.beforeDate = beforeDate;
        }

        public SearchRequest(ApiKey key, string searchQuerey, long afterTweet, long beforeTweet, int resultCount = 20) : this(key, searchQuerey, resultCount)
        {
            this.afterTweet = afterTweet;
            this.beforeTweet = beforeTweet;
        }

        protected override string ResourceUrl
        {
            get { return "https://api.twitter.com/1.1/search/tweets.json"; }
        }

        protected override string Method
        {
            get { return "GET"; }
        }

        protected override void PackProperties()
        {
            base.PackProperties();

            AddUrlProperty("q", searchQuerey);

            if (beforeDate != default(DateTime))
                AddUrlProperty("until", beforeDate.ToString("yyyy-MM-dd"));
            else
            {
                if(afterTweet >= 0)
                    AddUrlProperty("since_id", afterTweet.ToString());
                if(beforeTweet >= 0)
                    AddUrlProperty("max_id", beforeTweet.ToString());
            }

            AddUrlProperty("count", resultCount.ToString());
        }

        private string searchQuerey;
        private int resultCount;
        private DateTime beforeDate;
        private long beforeTweet = -1;
        private long afterTweet = -1;
    }
}
