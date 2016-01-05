using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApi.Objects
{
    /// <summary>
    /// Similar to TwitterUser, however contains a more compact version of the users info.
    /// </summary>
    [DataContract]
    public class TwitterUserSmall
    {
        public long Id
        {
            get { return id; }
        }

        public string ScreenName
        {
            get { return screenName; }
        }

        [DataMember]
        private long id;

        [DataMember(Name = "screen_name")]
        private string screenName;
    }
}
