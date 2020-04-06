
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace DailymotionSDK.JSON
{

    #region JSON_Error
    public class JSON_Error
    {
        [Browsable(false)] [Bindable(false)] [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] [EditorBrowsable(EditorBrowsableState.Never)] public _Error error { get; set; }
        public string _ErrorMessage { get { return error.message; } }
        public int _ERRORCODE { get { return error.code; } }
    }
    public class _Error
    {
        public string more_info { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public Error_Data error_data { get; set; }
    }
    public class Error_Data
    {
        public string reason { get; set; }
    }
    #endregion

    #region ExchangingVerificationCodeForToken
    public class JSON_ExchangingVerificationCodeForToken
    {
        [JsonProperty("error")] public _Error _error { get; set; }
        public string _ErrorMessage { get { return _error.message; } }
        public int _ERRORCODE { get { return _error.code; } }
        public string token_type { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }
    #endregion

    public class JSON_UserInfo
    {
        public string id { get; set; }
        public int created_time { get; set; }
        public string email { get; set; }
        public object fullname { get; set; }
        public JSON_UserInfo_Limits limits { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public bool verified { get; set; }
        public int videos_total { get; set; }
        public int views_total { get; set; }
        public int playlists_total { get; set; }
        public bool EmailVerified
        {
            get
            {
                // pending -activation
                // active
                return status.Equals("pending-activation") ? false : true;
            }
        }
    }
    public class JSON_UserInfo_Limits
    {
        public long video_duration { get; set; }
        public long video_size { get; set; }
    }

    public partial class JSON_TokenfromUsernamePassword
    {
        [JsonProperty("error")]public string _error { get; set; }
        [JsonProperty("error_description")]public string _ErrorMessage { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string uid { get; set; }
    }

    public class JSON_CheckAccessToken
    {
        public string id { get; set; }
        public List<string> scope { get; set; }
        public List<object> roles { get; set; }
        public string username { get; set; }
        public string screenname { get; set; }
    }

    public class JSON_RemoteUpload
    {
        public string id { get; set; }
        public string title { get; set; }
        public string owner { get; set; }
        public Utilitiez.ChannelsEnum? channel { get; set; }
    }

    public class JSON_APIrateLimits
    {
        [JsonProperty("id")]public string UserID { get; set; }
        public JSON_UserInfo_Limits limits { get; set; }
    }

    public class JSON_ListVideos
    {
        public int page { get; set; }
        public int limit { get; set; }
        public bool @explicit { get; set; }
        public int total { get; set; }
        public bool has_more { get; set; }
        [JsonProperty("list")]public List<JSON_VideoMetadata> VideoList { get; set; }
    }

    public class JSON_GetUploadUrl
    {
        public string upload_url { get; set; }
        public string progress_url { get; set; }
    }

    public class JSON_CompleteUpload
    {
        public string id { get; set; }
        public string title { get; set; }
        public string owner { get; set; }
        public Utilitiez.ChannelsEnum? channel { get; set; }
    }

    public class JSON_UploadLocalFile
    {
        public string id { get; set; }
        public string acodec { get; set; }
        public string bitrate { get; set; }
        public string dimension { get; set; }
        public string duration { get; set; }
        public string format { get; set; }
        public string hash { get; set; }
        public string name { get; set; }
        public string seal { get; set; }
        public string size { get; set; }
        public string streamable { get; set; }
        public string url { get; set; }
        public string vcodec { get; set; }
    }

    public class JSON_PlaylistExtractor
    {
        public int page { get; set; }
        public int limit { get; set; }
        public bool @explicit { get; set; }
        public bool has_more { get; set; }
        [JsonProperty("list")]public List<JSON_VideosList> VideosList { get; set; }
    }
    public class JSON_VideosList
    {
        public long duration { get; set; }
        public string embed_url { get; set; }
        public int height { get; set; }
        public string id { get; set; }
        public string thumbnail_60_url { get; set; }
        public string thumbnail_url { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public int views_total { get; set; }
        public int width { get; set; }
    }

    #region VideoDirectLink
    public class JSON_GetVideoDirectLink
    {
        public JSON_DirectMetadata metadata { get; set; }
        public json_VideoResolutionUrls VideoResolutionUrls { get; set; }
    }
    public class JSON_DirectMetadata
    {
        public string filmstrip_url { get; set; }
        public string poster_url { get; set; }
        public bool protected_delivery { get; set; }
        public string channel { get; set; }
        public int created_time { get; set; }
        public long duration { get; set; }
        public List<string> tags { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string mode { get; set; }
        public bool @private { get; set; }
        public Posters posters { get; set; }
        public Owner owner { get; set; }
        public string stream_type { get; set; }
        public Qualities qualities { get; set; }
    }
    public class Posters
    {
        [JsonProperty("60")]
        public string x60 { get; set; }
        [JsonProperty("120")]
        public string x120 { get; set; }
        [JsonProperty("180")]
        public string x180 { get; set; }
        [JsonProperty("240")]
        public string x240 { get; set; }
        [JsonProperty("360")]
        public string x360 { get; set; }
        [JsonProperty("480")]
        public string x480 { get; set; }
        [JsonProperty("720")]
        public string x720 { get; set; }
        [JsonProperty("1080")]
        public string x1080 { get; set; }
        [JsonProperty("1440")]
        public string x1440 { get; set; }
        [JsonProperty("2160")]
        public string x2160 { get; set; }
    }
    public class Owner
    {
        public string id { get; set; }
        public object parent { get; set; }
        public string screenname { get; set; }
        public string url { get; set; }
        public string username { get; set; }
    }
    public class json_VideoResolutionUrls
    {
        public string Name { get; set; }
        public string R_144 { get; set; }
        public string R_240 { get; set; }
        public string R_380 { get; set; }
        public string R_480 { get; set; }
        public string R_720 { get; set; }
        public string R_1080 { get; set; }
        public string R_1440 { get; set; }
        public string R_2160 { get; set; }
    }
    public class Qualities
    {
        public List<_Auto> auto { get; set; }
    }
    public class _Auto
    {
        public string type { get; set; }
        public string url { get; set; }
    }
    #endregion

    public enum VideoStatusEnum
    {
        waiting,
        processing,
        ready,
        published,
        rejected,
        deleted,
        encoding_error
    }
    public class JSON_VideoMetadata
    {
        public string preview_360p_url { get; set; }
        public string preview_480p_url { get; set; }
        public bool @private { get; set; }
        public object publish_date { get; set; }
        [JsonProperty("channel.id")]public Utilitiez.ChannelsEnum channelid { get; set; }
        public string checksum { get; set; }
        public int duration { get; set; }
        public int created_time { get; set; }
        public string description { get; set; }
        public object expiry_date_deletion { get; set; }
        public string embed_url { get; set; }
        public int encoding_progress { get; set; }
        public object expiry_date { get; set; }
        public VideoStatusEnum status { get; set; }
        public string filmstrip_60_url { get; set; }
        public int height { get; set; }
        public string id { get; set; }
        public string item_type { get; set; }
        public string thumbnail_1080_url { get; set; }
        public object live_publish_url { get; set; }
        public int likes_total { get; set; }
        public bool password_protected { get; set; }
        public object password { get; set; }
        [JsonProperty("owner.avatar_80_url")]public string owner_avatar_80_url { get; set; }
        [JsonProperty("owner.id")]public string owner_id { get; set; }
        [JsonProperty("owner.url")]public string owner_url { get; set; }
        [JsonProperty("owner.screenname")]public string owner_screenname { get; set; }
        [JsonProperty("owner.username")]public string owner_username { get; set; }
        [JsonProperty("owner.playlists_total")]public int owner_playlists_total { get; set; }
        [JsonProperty("owner.views_total")]public int owner_views_total { get; set; }
        [JsonProperty("owner.videos_total")]
        public int owner_videos_total { get; set; }
        public string media_type { get; set; }
        public bool published { get; set; }
        public string preview_240p_url { get; set; }
        public string private_id { get; set; }
        public string sprite_320x_url { get; set; }
        public string thumbnail_120_url { get; set; }
        public string sprite_url { get; set; }
        public string thumbnail_360_url { get; set; }
        public string tiny_url { get; set; }
        public List<string> tags { get; set; }
        public string url { get; set; }
        public string thumbnail_240_url { get; set; }
        public string thumbnail_180_url { get; set; }
        public string thumbnail_720_url { get; set; }
        public string thumbnail_60_url { get; set; }
        public string thumbnail_480_url { get; set; }
        public string thumbnail_url { get; set; }
        [JsonProperty("title")]public string name { get; set; }
        public int updated_time { get; set; }
        public int views_total { get; set; }
        public int width { get; set; }
        public int publishing_progress { get; set; }
        public float aspect_ratio { get; set; }
        // Public Property available_formats As List(Of utilitiez.AvailableFormatsEnum) 'metadata
        public List<string> available_formats { get; set; }
    }

    public class JSON_PlayListMetadata
    {
        public int created_time { get; set; }
        public string thumbnail_180_url { get; set; }
        public string thumbnail_240_url { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string thumbnail_360_url { get; set; }
        [JsonProperty("owner.avatar_80_url")]
        public string owner_avatar_80_url { get; set; }
        [JsonProperty("owner.id")]
        public string owner_id { get; set; }
        [JsonProperty("owner.url")]
        public string owner_url { get; set; }
        [JsonProperty("owner.screenname")]
        public string owner_screenname { get; set; }
        [JsonProperty("owner.username")]
        public string owner_username { get; set; }
        [JsonProperty("owner.playlists_total")]
        public int owner_playlists_total { get; set; }
        [JsonProperty("owner.views_total")]
        public int owner_views_total { get; set; }
        [JsonProperty("owner.videos_total")]
        public int owner_videos_total { get; set; }
        public string name { get; set; }
        public bool @private { get; set; }
        public string thumbnail_1080_url { get; set; }
        public string thumbnail_120_url { get; set; }
        public string thumbnail_480_url { get; set; }
        public string thumbnail_60_url { get; set; }
        public string thumbnail_720_url { get; set; }
        public string thumbnail_url { get; set; }
        public string item_type { get; set; }
        public int updated_time { get; set; }
        public int videos_total { get; set; }
        public string url
        {
            get
            {
                return string.Concat("https://www.dailymotion.com/playlist/", id);
            }
        }
    }

    [System.Runtime.Serialization.DataContract]
    public enum UserStatusEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "pending-activation")]
        PendingActivation,
        disabled,
        active,
        unknown
    }
    public class JSON_UserMetadata
    {
        public string avatar_80_url { get; set; }
        public int created_time { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public int playlists_total { get; set; }
        public UserStatusEnum status { get; set; }
        public string url { get; set; }
        public string screenname { get; set; }
        public string username { get; set; }
        public bool verified { get; set; }
        public int videos_total { get; set; }
        public int views_total { get; set; }

        // 'user info
        public JSON_UserInfo_Limits limits { get; set; }
        public bool EmailVerified
        {
            get
            {
                // pending -activation
                // active
                // Return If(status.Equals("pending-activation"), False, True)
                return status == UserStatusEnum.PendingActivation ? false : true;
            }
        }
    }

    public class JSON_ListPlaylists
    {
        public int page { get; set; }
        public int limit { get; set; }
        public bool @explicit { get; set; }
        public bool has_more { get; set; }
        [JsonProperty("list")]
        public List<JSON_PlayListMetadata> PlaylistsList { get; set; }
    }

    public class JSON_ListUsers
    {
        public int page { get; set; }
        public int limit { get; set; }
        public bool @explicit { get; set; }
        public int total { get; set; }
        public bool has_more { get; set; }
        [JsonProperty("list")]
        public List<JSON_UserMetadata> UsersList { get; set; }
    }

    public class JSON_ListChannels
    {
        public int page { get; set; }
        public int limit { get; set; }
        public bool @explicit { get; set; }
        public int total { get; set; }
        public bool has_more { get; set; }
        [JsonProperty("list")]public List<JSON_ChannelMetadata> ChannelsList { get; set; }
    }
    public class JSON_ChannelMetadata
    {
        public int created_time { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string item_type { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int updated_time { get; set; }
    }

    public class JSON_Vimeo
    {
        public JSON_VimeoRequest request { get; set; }
        public JSON_VimeoVideo video { get; set; }
    }
    public class JSON_VimeoRequest
    {
        public JSON_VimeoFiles files { get; set; }
    }

    public class JSON_VimeoFiles
    {
        public List<JSON_VimeoProgressive> progressive { get; set; }
        public List<JSON_VimeoProgressive> progressive_avc { get; set; }
    }
    public class JSON_VimeoProgressive
    {
        public int width { get; set; }
        public string mime { get; set; }
        public string url { get; set; }
        public string quality { get; set; }
        public int height { get; set; }
        public string Size_str { get; set; }
    }
    public class JSON_VimeoVideo
    {
        public int height { get; set; }
        public int duration { get; set; }
        public JSON_VimeoThumbs thumbs { get; set; }
        public JSON_VimeoOwner owner { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public int width { get; set; }
        public string url { get; set; }
        public string privacy { get; set; }
        public string bypass_token { get; set; }
    }
    public class JSON_VimeoThumbs
    {
        [JsonProperty("1280")]
        public string _1280 { get; set; }
        [JsonProperty("960")]
        public string _960 { get; set; }
        [JsonProperty("640")]
        public string _640 { get; set; }
        public string @base { get; set; }
    }
    public class JSON_VimeoOwner
    {
        public string account_type { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string url { get; set; }
        public string img_2x { get; set; }
        public int id { get; set; }
    }

    public class JSON_Youtube
    {
        public JSON_YoutubePlayabilitystatus playabilityStatus { get; set; }
        public JSON_YoutubeStreamingdata streamingData { get; set; }
        public JSON_YoutubeVideodetails videoDetails { get; set; }
        // Public Property microformat As Microformat
        public string trackingParams { get; set; }
    }
    public class JSON_YoutubePlayabilitystatus
    {
        public string status { get; set; }
    }
    public class JSON_YoutubeStreamingdata
    {
        public string expiresInSeconds { get; set; }
        public List<JSON_YoutubeFormat> formats { get; set; }
        public List<JSON_YoutubeAdaptiveformat> adaptiveFormats { get; set; }
    }
    public class JSON_YoutubeFormat
    {
        public int itag { get; set; }
        public string url { get; set; }
        public string mimeType { get; set; }
        public int bitrate { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string lastModified { get; set; }
        public string contentLength { get; set; }
        public string quality { get; set; }
        public string qualityLabel { get; set; }
        public string projectionType { get; set; }
        public int averageBitrate { get; set; }
        public string audioQuality { get; set; }
        public string approxDurationMs { get; set; }
        public string audioSampleRate { get; set; }
        public int audioChannels { get; set; }
    }
    public class JSON_YoutubeAdaptiveformat
    {
        public int itag { get; set; }
        public string url { get; set; }
        public string mimeType { get; set; }
        public int bitrate { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        // Public Property initRange As JSON_YoutubeInitrange
        // Public Property indexRange As JSON_YoutubeIndexrange
        public string lastModified { get; set; }
        public string contentLength { get; set; }
        public string quality { get; set; }
        public int fps { get; set; }
        public string qualityLabel { get; set; }
        public string projectionType { get; set; }
        public int averageBitrate { get; set; }
        public string approxDurationMs { get; set; }
        public bool highReplication { get; set; }
        public string audioQuality { get; set; }
        public string audioSampleRate { get; set; }
        public int audioChannels { get; set; }
    }
    // Public Class JSON_YoutubeInitrange
    // Public Property start As String
    // Public Property _end As String
    // End Class
    // Public Class JSON_YoutubeIndexrange
    // Public Property start As String
    // Public Property _end As String
    // End Class
    public class JSON_YoutubeVideodetails
    {
        public string videoId { get; set; }
        public string title { get; set; }
        public string lengthSeconds { get; set; }
        public List<string> keywords { get; set; }
        public string channelId { get; set; }
        public bool isOwnerViewing { get; set; }
        public string shortDescription { get; set; }
        public bool isCrawlable { get; set; }
        public JSON_YoutubeThumbnail thumbnail { get; set; }
        public float averageRating { get; set; }
        public bool allowRatings { get; set; }
        public string viewCount { get; set; }
        public string author { get; set; }
        public bool isPrivate { get; set; }
        public bool isUnpluggedCorpus { get; set; }
        public bool isLiveContent { get; set; }
    }
    public class JSON_YoutubeThumbnail
    {
        public List<JSON_YoutubeThumbnailData> thumbnails { get; set; }
    }
    public class JSON_YoutubeThumbnailData
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}



