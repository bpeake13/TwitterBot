using System;
using System.Collections.Generic;
using System.Linq;
#pragma warning disable CS0169, CS0649

using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApi.Objects
{
    [DataContract]
    public class TweetEntities
    {
        public Hashtag[] Hashtags
        {
            get { return (Hashtag[])hashtags.Clone(); }
        }

        public TweetMedia[] Media
        {
            get { return (TweetMedia[])tweetMedia.Clone(); }
        }

        public EmbededUrl[] Urls
        {
            get { return (EmbededUrl[])urls.Clone(); }
        }

        public UserMention[] UserMentions
        {
            get { return (UserMention[])userMentions.Clone(); }
        }

        [DataMember]
        private Hashtag[] hashtags;

        [DataMember]
        private TweetMedia[] tweetMedia;

        [DataMember]
        private EmbededUrl[] urls;

        [DataMember(Name = "user_mentions")]
        private UserMention[] userMentions;
    }

    [DataContract]
    public class Hashtag
    {
        public string Text
        {
            get { return text; }
        }

        public int[] Indicies
        {
            get { return (int[])indicies.Clone(); }
        }

        [DataMember]
        private string text;

        [DataMember]
        private int[] indicies;
    }

    [DataContract]
    public class TweetMedia
    {
        public long Id
        {
            get { return id; }
        }

        public long SourceStatusId
        {
            get { return sourceStatusId; }
        }

        public string MediaType
        {
            get { return type; }
        }

        public string RawUrl
        {
            get { return url; }
        }

        public string DisplayUrl
        {
            get { return displayUrl; }
        }

        public string ExpandedUrl
        {
            get { return expandedUrl; }
        }

        public string MediaUrl
        {
            get { return mediaUrl; }
        }

        public int[] Indicies
        {
            get { return indicies; }
        }

        public Sizes MediaSizes
        {
            get { return sizes; }
        }

        [DataMember]
        private long id;

        [DataMember(Name = "source_status_id")]
        private long sourceStatusId;

        [DataMember]
        private string type;

        [DataMember]
        private string url;

        [DataMember(Name = "display_url")]
        private string displayUrl;

        [DataMember(Name = "expanded_url")]
        private string expandedUrl;

        [DataMember(Name = "media_url")]
        private string mediaUrl;

        [DataMember]
        private int[] indicies;

        [DataMember]
        private Sizes sizes;
    }

    [DataContract]
    public class Size
    {
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public string Resize
        {
            get { return resize; }
        }

        [DataMember(Name = "w")]
        private int width;

        [DataMember(Name = "h")]
        private int height;

        [DataMember]
        private string resize;
    }

    [DataContract]
    public class Sizes
    {
        public Size Thumb
        {
            get { return thumb; }
        }

        public Size Small
        {
            get { return small; }
        }

        public Size Medium
        {
            get { return medium; }
        }

        public Size Large
        {
            get { return large; }
        }

        [DataMember]
        private Size thumb;

        [DataMember]
        private Size small;

        [DataMember]
        private Size medium;

        [DataMember]
        private Size large;
    }

    [DataContract]
    public class EmbededUrl
    {
        public string Url
        {
            get { return url; }
        }

        public string DisplayUrl
        {
            get { return displayUrl; }
        }

        public string ExpandedUrl
        {
            get { return expandedUrl; }
        }

        public int[] Indicies
        {
            get { return indicies; }
        }

        [DataMember]
        private string url;

        [DataMember(Name = "display_url")]
        private string displayUrl;

        [DataMember(Name = "expanded_url")]
        private string expandedUrl;

        [DataMember]
        private int[] indicies;
    }

    [DataContract]
    public class UserMention
    {
        public long Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string ScreenName
        {
            get { return screenName; }
        }

        public int[] Indicies
        {
            get { return indicies; }
        }

        [DataMember]
        private long id;

        [DataMember]
        private string name;

        [DataMember(Name = "screen_name")]
        private string screenName;

        [DataMember]
        private int[] indicies;
    }
}
