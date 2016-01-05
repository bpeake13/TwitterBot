using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TwitterApi.Objects;

namespace TwitterApi
{
	// Gets the timeline of the requested user
    public class TimelineRequest : ApiRequest<Tweet[]>
    {
        public TimelineRequest(ApiKey key, string screenName)
            : this(key, screenName, 0)
        {
           
        }

        public TimelineRequest(ApiKey key, string screenName, int responseCount)
            : base(key)
        {
            this.screenName = screenName;
            this.responseCount = responseCount;
		}

		public TimelineRequest(ApiKey key, ulong userId, int responseCount)
			: base(key)
		{
			this.userId = userId;
			this.responseCount = responseCount;
		}

		public TimelineRequest(ApiKey key, ulong userId)
			: this(key, userId, 0)
		{

		}

        protected override string ResourceUrl
        {
            get { return "https://api.twitter.com/1.1/statuses/user_timeline.json"; }
        }

        protected override string Method
        {
            get { return "GET"; }
        }

        protected override void PackProperties()
        {
            base.PackProperties();

			if (!string.IsNullOrEmpty (screenName))
				AddUrlProperty ("screen_name", screenName);
			else
				AddUrlProperty ("id", userId.ToString());

            if (responseCount > 0)
                AddUrlProperty("count", responseCount.ToString());
        }

        private ulong userId;
        private string screenName;
        private int responseCount;
    }
}
