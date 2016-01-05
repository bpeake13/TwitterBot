using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Objects;

namespace TwitterApi
{
    public class UserSearchRequest : ApiRequest<TwitterUser[]>
    {
        public UserSearchRequest(ApiKey key, string query, int count = 5, int page = 1) : base(key)
        {
            this.query = query;
            this.count = count;
            this.page = page;
        }

        protected override string ResourceUrl
        {
            get { return "https://api.twitter.com/1.1/users/search.json"; }
        }

        protected override string Method
        {
            get { return "GET"; }
        }

        protected override void PackProperties()
        {
            base.PackProperties();

            AddUrlProperty("q", query);
            AddUrlProperty("count", count.ToString());
            AddUrlProperty("page", page.ToString());
        }

        private string query;
        private int count;
        private int page;
    }
}
