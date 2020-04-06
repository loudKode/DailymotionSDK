using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;

namespace DailymotionSDK
{
    public class Utilitiez
    {

        public static string AsQueryString(Dictionary<string, string> parameters)
        {
            if (!parameters.Any()) { return string.Empty; }

            var builder = new StringBuilder("?");
            var separator = string.Empty;
            foreach (var kvp in parameters.Where(P => !string.IsNullOrEmpty(P.Value)))
            {
                builder.AppendFormat("{0}{1}={2}", separator, System.Net.WebUtility.UrlEncode(kvp.Key), System.Net.WebUtility.UrlEncode(kvp.Value.ToString()));
                separator = "&";
            }
            return builder.ToString();
        }

        public static string Between(System.String source, string leftString, string rightString)
        {
            return System.Text.RegularExpressions.Regex.Match(source, string.Format("{0}(.*){1}", leftString, rightString)).Groups[1].Value;
        }

        public static long DateTimeToUnixTimestampMilliseconds(DateTime DT)
        {
            return Convert.ToInt32((DT.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        public static async Task<string> GetFileSize(string uriPath)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(new HttpMethod("HEAD"), new Uri(uriPath));
                using (HttpResponseMessage response = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                {
                    var fileSize = response.Content.Headers.ContentLength;
                    var fileSizeInMegaByte = Math.Round(Convert.ToDouble(fileSize) / 1024.0 / 1024.0, 2);
                    return fileSizeInMegaByte + " MB";
                }
            }
        }

        #region youtube
        // Tries to parse video ID from a YouTube video URL
        public static string TryParseVideoId(string videoUrl)
        {
            string videoId = "default";
            if (string.IsNullOrWhiteSpace(videoUrl)) { return null; }

            // // https://www.youtube.com/watch?v=yIVRs6YSbOM
            var regularMatch = System.Text.RegularExpressions.Regex.Match(videoUrl, @"youtube\..+?/watch.*?v=(.*?)(?:&|/|$)").Groups[1].Value;
            if (!string.IsNullOrWhiteSpace(regularMatch) && ValidateVideoId(regularMatch))
            {
                videoId = regularMatch;
            }

            // // https://youtu.be/yIVRs6YSbOM
            var shortMatch = System.Text.RegularExpressions.Regex.Match(videoUrl, @"youtu\.be/(.*?)(?:\?|&|/|$)").Groups[1].Value;
            if (!string.IsNullOrWhiteSpace(shortMatch) && ValidateVideoId(shortMatch))
            {
                videoId = shortMatch;
            }

            // // https://www.youtube.com/embed/yIVRs6YSbOM
            var embedMatch = System.Text.RegularExpressions.Regex.Match(videoUrl, @"youtube\..+?/embed/(.*?)(?:\?|&|/|$)").Groups[1].Value;
            if (!string.IsNullOrWhiteSpace(embedMatch) && ValidateVideoId(embedMatch))
            {
                videoId = embedMatch;
            }

            return videoId;
        }
        // 'Verifies that the given string is syntactically a valid YouTube video ID
        private static bool ValidateVideoId(string videoId)
        {
            if (string.IsNullOrWhiteSpace(videoId)) { return false; }
            // // Video IDs are always 11 characters
            if (!videoId.Length.Equals(11)) { return false; }

            return !System.Text.RegularExpressions.Regex.IsMatch(videoId, @"[^0-9a-zA-Z_\-]");
        }
        #endregion

        public static string[] GetStringsFromClassConstants(System.Type type)
        {
            ArrayList constants = new ArrayList();
            System.Reflection.FieldInfo[] fieldInfos = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy);
            foreach (System.Reflection.FieldInfo fi in fieldInfos)
            {
                if (fi.IsLiteral && !fi.IsInitOnly)
                {
                    constants.Add(fi);
                }
            }
            System.Collections.Specialized.StringCollection ConstantsStringArray = new System.Collections.Specialized.StringCollection();
            foreach (System.Reflection.FieldInfo fi in (System.Reflection.FieldInfo[])constants.ToArray(typeof(System.Reflection.FieldInfo)))
            {
                ConstantsStringArray.Add(System.Convert.ToString(fi.GetValue(null)));
            }

            string[] retVal = new string[ConstantsStringArray.Count - 1 + 1];
            ConstantsStringArray.CopyTo(retVal, 0);
            return retVal;
        }

        public enum PlaylistSortEnum
        {
            recent,
            relevance,
            alpha,
            most,
            least,
            alphaaz,
            alphaza,
            changed
        }
        public enum VideoSortEnum
        {
            recent,
            visited,
            relevance,
            random,
            trending,
            old
        }
        public enum UsersSortEnum
        {
            recent,
            relevance,
            popular,
            activity
        }
        public enum ChannelSortEnum
        {
            popular,
            alpha
        }
        public enum SearchTypesEnum
        {
            Contains,
            Exact
        }
        public enum UploadTypes
        {
            FilePath,
            Stream,
            BytesArry
        }
        public enum ChannelsEnum
        {
            kids,
            shortfilms,
            news,
            videogames,
            school,
            fun,
            tech,
            tv,
            animals,
            sport,
            travel,
            auto,
            creation,
            people,
            music,
            lifestyle,
            webcam
        }
        public enum AvailableFormatsEnum
        {
            l2 = 144,
            ld = 240,
            sd = 380,
            hq = 480,
            hd720 = 720,
            hd1080 = 1080,
            uhd1440 = 1440,
            uhd2160 = 2160
        }
        public enum PrivacyEnum
        {
            Public,
            Private
        }
        public enum FieldsEnum
        {
            active,
            address,
            avatar_120_url,
            avatar_190_url,
            avatar_240_url,
            avatar_25_url,
            avatar_360_url,
            avatar_480_url,
            avatar_60_url,
            avatar_720_url,
            avatar_80_url,
            avatar_url,
            birthday,
            children_total,
            city,
            country,
            cover_100_url,
            cover_150_url,
            cover_200_url,
            cover_250_url,
            cover_url,
            created_time,
            description,
            email,
            facebook_url,
            first_name,
            followers_total,
            following_total,
            fullname,
            gender,
            googleplus_url,
            id,
            instagram_url,
            is_following,
            item_type,
            language,
            last_name,
            limits,
            linkedin_url,
            parent,
            partner,
            pinterest_url,
            playlists_total,
            reposts_total,
            revenues_claim_last_day,
            revenues_claim_last_month,
            revenues_claim_last_week,
            revenues_claim_total,
            revenues_paidcontent_last_day,
            revenues_paidcontent_last_month,
            revenues_paidcontent_last_week,
            revenues_paidcontent_total,
            revenues_video_last_day,
            revenues_video_last_month,
            revenues_video_last_week,
            revenues_video_total,
            revenues_website_last_day,
            revenues_website_last_month,
            revenues_website_last_week,
            revenues_website_total,
            screenname,
            status,
            twitter_url,
            url,
            username,
            verified,
            videos_total,
            views_total,
            website_url
        }

        // 'https://api.dailymotion.com/user/xxpg7ix/videos?fields=preview_360p_url%2Cpreview_480p_url%2Cprivate%2Cpublish_date%2Cchannel%2Cchannel.id%2Cchannel.item_type%2Cchannel.description%2Cchannel.name%2Cchecksum%2Cduration%2Ccreated_time%2Cdescription%2Cexpiry_date_deletion%2Cembed_url%2Cencoding_progress%2Cexpiry_date%2Cexpiry_date_availability%2Cstatus%2Cfilmstrip_60_url%2Cheight%2Cid%2Citem_type%2Cstream_h264_l1_url%2Clive_publish_url%2Clikes_total%2Cstream_h264_url%2Cpassword_protected%2Cpassword%2Cowner%2Cowner.avatar_80_url%2Cowner.id%2Cowner.url%2Cowner.screenname%2Cowner.username%2Cmedia_type%2Cstream_premium_preview_hls_url%2Cpublished%2Cpreview_240p_url%2Cprivate_id%2Csprite_320x_url%2Cthumbnail_120_url%2Cstream_h264_hd_url%2Cstream_h264_hd1080_url%2Csprite_url%2Cstart_time%2Cthumbnail_360_url%2Cstream_audio_url%2Cstream_h264_uhd_url%2Cstream_h264_qhd_url%2Cstream_h264_hq_url%2Cstream_h264_l2_url%2Cstream_h264_ld_url%2Cthumbnail_1080_url%2Cstream_hls_url%2Cstream_live_smooth_url%2Cstream_live_hls_url%2Cstream_live_rtmp_url%2Ctiny_url%2Ctags%2Cstream_source_url%2Cstream_premium_preview_mp4_url%2Cstream_premium_preview_web_url%2Curl%2Cthumbnail_240_url%2Cthumbnail_180_url%2Cthumbnail_720_url%2Cthumbnail_60_url%2Cthumbnail_480_url%2Cthumbnail_url%2Ctitle%2Cupdated_time%2Cviews_total%2Cwidth%2Cend_time%2Cpublishing_progress%2Caspect_ratio
        public class FieldsVideo
        {
            public const string preview_360p_url = "preview_360p_url";
            public const string preview_480p_url = "preview_480p_url";
            public const string @private = "private";
            public const string publish_date = "publish_date";
            public const string channel_id = "channel.id";
            public const string checksum = "checksum";
            public const string duration = "duration";
            public const string created_time = "created_time";
            public const string description = "description";
            public const string expiry_date_deletion = "expiry_date_deletion";
            public const string embed_url = "embed_url";
            public const string encoding_progress = "encoding_progress";
            public const string expiry_date = "expiry_date";
            public const string status = "status";
            public const string filmstrip_60_url = "filmstrip_60_url";
            public const string height = "height";
            public const string id = "id";
            public const string item_type = "item_type";
            public const string thumbnail_1080_url = "thumbnail_1080_url";
            public const string live_publish_url = "live_publish_url";
            public const string likes_total = "likes_total";
            public const string password_protected = "password_protected";
            public const string password = "password";
            // Public Const owner As String = "owner"
            public const string owner_avatar_80_url = "owner.avatar_80_url";
            public const string owner_id = "owner.id";
            public const string owner_url = "owner.url";
            public const string owner_screenname = "owner.screenname";
            public const string owner_username = "owner.username";
            public const string owner_playlists_total = "owner.playlists_total";
            public const string owner_views_total = "owner.views_total";
            public const string owner_videos_total = "owner.videos_total";
            public const string media_type = "media_type";
            public const string published = "published";
            public const string preview_240p_url = "preview_240p_url";
            public const string private_id = "private_id";
            public const string sprite_320x_url = "sprite_320x_url";
            public const string thumbnail_120_url = "thumbnail_120_url";
            public const string sprite_url = "sprite_url";
            public const string thumbnail_360_url = "thumbnail_360_url";
            public const string tiny_url = "tiny_url";
            public const string tags = "tags";
            public const string url = "url";
            public const string thumbnail_240_url = "thumbnail_240_url";
            public const string thumbnail_180_url = "thumbnail_180_url";
            public const string thumbnail_720_url = "thumbnail_720_url";
            public const string thumbnail_60_url = "thumbnail_60_url";
            public const string thumbnail_480_url = "thumbnail_480_url";
            public const string thumbnail_url = "thumbnail_url";
            public const string title = "title";
            public const string updated_time = "updated_time";
            public const string views_total = "views_total";
            public const string width = "width";
            public const string publishing_progress = "publishing_progress";
            public const string aspect_ratio = "aspect_ratio";

            public const string available_formats = "available_formats";
        }

        // 'https://api.dailymotion.com/playlists?fields=thumbnail_180_url%2Ccreated_time%2Cthumbnail_240_url%2Cid%2Cdescription%2Cthumbnail_360_url%2Cowner%2Cowner.avatar_80_url%2Cowner.description%2Cowner.created_time%2Cowner.id%2Cowner.playlists_total%2Cowner.url%2Cowner.screenname%2Cowner.username%2Cowner.views_total%2Citem_type%2Cname%2Cprivate%2Cthumbnail_1080_url%2Cthumbnail_120_url%2Cthumbnail_480_url%2Cthumbnail_60_url%2Cthumbnail_720_url%2Cthumbnail_url%2Cupdated_time%2Cvideos_total
        public class FieldsPlaylist
        {
            public const string thumbnail_180_url = "thumbnail_180_url";
            public const string created_time = "created_time";
            public const string thumbnail_240_url = "thumbnail_240_url";
            public const string id = "id";
            public const string description = "description";
            public const string thumbnail_360_url = "thumbnail_360_url";
            public const string owner = "owner";
            public const string owner_avatar_80_url = "owner.avatar_80_url";
            public const string owner_id = "owner.id";
            public const string owner_url = "owner.url";
            public const string owner_screenname = "owner.screenname";
            public const string owner_username = "owner.username";
            // Public Const owner_created_time As String = "owner.created_time"
            public const string owner_playlists_total = "owner.playlists_total";
            public const string owner_views_total = "owner.views_total";
            public const string owner_videos_total = "owner.videos_total";
            public const string item_type = "item_type";
            public const string name = "name";
            public const string @private = "private";
            public const string thumbnail_1080_url = "thumbnail_1080_url";
            public const string thumbnail_120_url = "thumbnail_120_url";
            public const string thumbnail_480_url = "thumbnail_480_url";
            public const string thumbnail_60_url = "thumbnail_60_url";
            public const string thumbnail_720_url = "thumbnail_720_url";
            public const string thumbnail_url = "thumbnail_url";
            public const string updated_time = "updated_time";
            public const string videos_total = "videos_total";
        }

        // 'https://api.dailymotion.com/video/ftrgds/subtitles?fields=format%2Cid%2Citem_type%2Clanguage%2Clanguage_label%2Curl
        public class FieldsSubtitle
        {
            public const string format = "format";
            public const string id = "id";
            public const string item_type = "item_type";
            public const string language = "language";
            public const string language_label = "language_label";
            public const string url = "url";
        }

        public class FieldsUsers
        {
            public const string avatar_80_url = "avatar_80_url";
            public const string created_time = "created_time";
            public const string description = "description";
            public const string id = "id";
            public const string playlists_total = "playlists_total";
            public const string status = "status";
            public const string url = "url";
            public const string screenname = "screenname";
            public const string username = "username";
            public const string verified = "verified";
            public const string videos_total = "videos_total";
            public const string views_total = "views_total";
        }

        public class FieldsChannels
        {
            public const string created_time = "created_time";
            public const string description = "description";
            public const string id = "id";
            public const string item_type = "item_type";
            public const string name = "name";
            public const string slug = "slug";
            public const string updated_time = "updated_time";
        }





    }
}
