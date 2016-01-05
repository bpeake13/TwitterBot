#pragma warning disable CS0169, CS0649

using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;

namespace TwitterApi.Objects
{
    [DataContract]
    public class Tweet
    {
        public long Id
        {
            get { return id; }
        }

        public string Text
        {
            get { return text; }
        }

        public DateTime CreatedAt
        {
            get { return createdAt; }
        }

        [DataMember(Name = "created_at")]
        private string CreatedAtInternalJson
        {
            set
            {
                createdAt = DateTime.ParseExact(value,
                                  "ddd MMM dd HH:mm:ss zzz yyyy",
                                  CultureInfo.InvariantCulture);
            }
            get { return null; }
        }

        public TwitterUserSmall[] Contributors
        {
            get { return (TwitterUserSmall[])contributors.Clone(); }
        }

        public TweetEntities Entities
        {
            get { return entities; }
        }

        public int FavoriteCount
        {
            get { return favoriteCount; }
        }

        public bool IsFavorited
        {
            get { return isFavorited; }
            set { isFavorited = value; }
        }

        public long? ReplyToTweetId
        {
            get { return replyToTweetId; }
        }

        public long? ReplyToUserId
        {
            get { return replyToUserId; }
        }

        public long? QuotedTweetId
        {
            get { return quotedTweetId; }
        }

        public Tweet QuotedTweet
        {
            get { return quotedTweet; }
        }

        public int RetweetCount
        {
            get { return retweetCount; }
        }

        public Tweet SourceTweet
        {
            get { return sourceTweet; }
        }

        public TwitterUser User
        {
            get { return user; }
        }

        [DataMember(IsRequired = true)]
        private long id;

        [DataMember(IsRequired = true)]
        private string text;

        [DataMember]
        private TwitterUser user;

        private DateTime createdAt;

        [DataMember]
        private TwitterUserSmall[] contributors;

        [DataMember]
        private TweetEntities entities;

        [DataMember(Name = "favorite_count")]
        private int favoriteCount;

        [DataMember(Name = "favorited")]
        private bool isFavorited;

        [DataMember(Name = "in_reply_to_status_id")]
        private long? replyToTweetId;

        [DataMember(Name = "in_reply_to_user_id")]
        private long? replyToUserId;

        [DataMember(Name = "quoted_status_id")]
        private long? quotedTweetId;

        [DataMember(Name = "quoted_status")]
        private Tweet quotedTweet;

        [DataMember(Name = "retweet_count")]
        private int retweetCount;

        [DataMember(Name = "retweeted")]
        private bool isRetweeted;

        [DataMember(Name = "retweeted_status")]
        private Tweet sourceTweet;
    }
}
