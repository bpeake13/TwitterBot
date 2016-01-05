using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Objects;

namespace TwitterApi
{
    public class UserRequest : ApiRequest<TwitterUser[]>
    {
        protected override string ResourceUrl
        {
            get { return "https://api.twitter.com/1.1/users/lookup.json"; }
        }

        protected override string Method
        {
            get { return "GET"; }
        }

        public UserRequest(ApiKey key, string screenName)
            : base(key)
        {
            this.screenName = screenName;
        }

        public UserRequest(ApiKey key, ulong id)
            : base(key)
        {
            this.id = id;
        }

        protected override void PackProperties()
        {
            base.PackProperties();

            if (string.IsNullOrEmpty(screenName))
                AddUrlProperty("user_id", id.ToString());
            else
                AddUrlProperty("screen_name", screenName);
        }

        private string screenName;
        private ulong id;
    }
}
