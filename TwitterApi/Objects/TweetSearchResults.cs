#pragma warning disable CS0169, CS0649

using System.Runtime.Serialization;

namespace TwitterApi.Objects
{
    [DataContract]
    public class TweetSearchResults
    {
        public Tweet[] FoundTweets
        {
            get { return (Tweet[])foundTweets.Clone(); }
        }

        [DataMember(Name = "statuses", IsRequired = true)]
        private Tweet[] foundTweets;
    }
}
