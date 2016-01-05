#pragma warning disable CS0169, CS0649

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace TwitterApi.Objects
{
    [DataContract]
    public class TwitterUser
    {
        /// <summary>
        /// Get a user that has already been requested from the server.
        /// </summary>
        /// <param name="id">The user id to look for.</param>
        /// <returns>The cached user, or null if the user has never been cached.</returns>
        public static TwitterUser GetCachedUser(long id)
        {
            TwitterUser foundUser;
            cachedUsers.TryGetValue(id, out foundUser);
            return foundUser;
        }

        /// <summary>
        /// Get a user that has already been requested from the server.
        /// </summary>
        /// <param name="name">The user name to look for.</param>
        /// <returns>The cached user, or null if the user has never been cached.</returns>
        public static TwitterUser GetCachedUser(string name)
        {
            return cachedUserIdLookup.ContainsKey(name) ? cachedUsers[cachedUserIdLookup[name]] : null;
        }

        public static void ClearCache()
        {
            cachedUsers.Clear();
            cachedUserIdLookup.Clear();
        }

        [DataMember(Name = "id", IsRequired = true)]
        public long Id
        {
            get { return id; }
            private set
            {
                id = value;

                TwitterUser oldUser;
                cachedUsers.TryGetValue(id, out oldUser);

                if (oldUser == null)
                {
                    cachedUserIdLookup.Remove(oldUser.Name);
                    cachedUsers.Remove(id);
                }

                cachedUsers.Add(id, this);
                cachedUserIdLookup.Add(Name, id);
            }
        }

        public string Name
        {
            get { return name; }
        }

        public string ScreenName
        {
            get { return screenName; }
        }

        public int FollowersCount
        {
            get { return followersCount; }
        }

        public int FriendsCount
        {
            get { return friendsCount; }
        }

        public bool IsFollowing
        {
            get { return isFollowing; }
        }

        public bool FollowRequestSent
        {
            get { return followRequestSent; }
        }

        public string Language
        {
            get { return language; }
        }

        public int ListedCount
        {
            get { return listedCount; }
        }

        public string Location
        {
            get { return location; }
        }

        public int BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        [DataMember(Name = "profile_background_color")]
        private string BackgroundColorInternalJson
        {
            set
            {
                backgroundColor = int.Parse(value, NumberStyles.HexNumber);
            }
            get { return null; }
        }

        public string ProfileBackgroundImageUrl
        {
            get { return profileBackgroundImageUrl; }
        }

        public bool IsBackgroundTiled
        {
            get { return isBackgroundTiled; }
        }

        public string ProfileBannerUrl
        {
            get { return profileBannerUrl; }
        }

        public string ProfileImageUrl
        {
            get { return profileImageUrl; }
        }

        public int ProfileLinkColor
        {
            get { return profileLinkColor; }
        }

        [DataMember(Name = "profile_link_color")]
        private string ProfileLinkColorInternalJson
        {
            set
            {
                profileTextColor = int.Parse(value, NumberStyles.HexNumber);
            }
            get { return null; }
        }

        public int ProfileTextColor
        {
            get { return profileTextColor; }
        }

        [DataMember(Name = "profile_text_color")]
        private string ProfileTextColorInternalJson
        {
            set { profileTextColor = int.Parse(value, NumberStyles.HexNumber); }
            get { return null; }
        }

        public bool IsProtected
        {
            get { return isProtected; }
        }

        public int StatusCount
        {
            get { return statusCount; }
        }

        public int? UtcOffset
        {
            get { return utcOffset; }
        }

        public string Zone
        {
            get { return timeZone; }
        }

        public string Url
        {
            get { return url; }
        }

        private long id;

        [DataMember]
        private string name;

        [DataMember(Name = "screen_name")]
        private string screenName;

        [DataMember(Name = "followers_count")]
        private int followersCount;

        [DataMember(Name = "friends_count")]
        private int friendsCount;

        [DataMember(Name = "following")]
        private bool isFollowing;

        [DataMember(Name = "follow_request_sent")]
        private bool followRequestSent;

        [DataMember(Name = "lang")]
        private string language;

        [DataMember(Name = "listed_count")]
        private int listedCount;

        [DataMember(Name = "location")]
        private string location;

        private int backgroundColor;

        [DataMember(Name = "profile_background_image_url")]
        private string profileBackgroundImageUrl;

        [DataMember(Name = "profile_background_tile")]
        private bool isBackgroundTiled;

        [DataMember(Name = "profile_banner_url")]
        private string profileBannerUrl;

        [DataMember(Name = "profile_image_url")]
        private string profileImageUrl;

        private int profileLinkColor;

        private int profileTextColor;

        [DataMember(Name = "protected")]
        private bool isProtected;

        [DataMember]
        private Tweet status;

        [DataMember(Name = "statuses_count")]
        private int statusCount;

        [DataMember(Name = "time_zone")]
        private string timeZone;

        [DataMember(Name = "utc_offset")]
        private int? utcOffset;

        [DataMember]
        private string url;

        [DataMember(Name = "verified")]
        private bool isVerified;

        private static Dictionary<long, TwitterUser> cachedUsers = new Dictionary<long, TwitterUser>();
        private static Dictionary<string, long> cachedUserIdLookup = new Dictionary<string, long>();
    }
}
