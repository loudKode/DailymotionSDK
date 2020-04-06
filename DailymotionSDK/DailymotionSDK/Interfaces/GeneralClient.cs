using DailymotionSDK.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
  public   class GeneralClient:IGeneral 
    {

        public async Task<JSON_ListChannels> GetChannelsList()
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsChannels))) },
                { "limit", "100" },
                { "page", "1" },
                { "sort", ChannelSortEnum.alpha.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/channels", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListChannels>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_Vimeo> GetVimeoDownloadUrls(string VimeoVideoUrl)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new Uri(string.Format("https://player.vimeo.com/video/{0}/config", VimeoVideoUrl.Split('/').Last()))).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();

                if (ResPonse.IsSuccessStatusCode)
                {
                    var fin = JsonConvert.DeserializeObject<JSON_Vimeo>(result, JSONhandler);
                    foreach (var vid in fin.request.files.progressive)
                    {
                        vid.Size_str = await Utilitiez.GetFileSize(vid.url);
                    }
                    return fin;
                }
                else
                {
                    ShowError(result);
                    return null;
                }
            }
        }
        public async Task<JSON_Youtube> GetYoutubeDownloadUrls(string YoutubeVideoUrl)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new Uri(string.Format("https://www.youtube.com/get_video_info?video_id={0}&el=embedded&ps=default&eurl=&gl=US&hl=en", Utilitiez.TryParseVideoId(YoutubeVideoUrl)))).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();

                if (ResPonse.IsSuccessStatusCode)
                {
                    result = WebUtility.UrlDecode(result);
                    var theTxt = string.Concat(Utilitiez.Between(result, "player_response=", "}}}&"), "}}}");
                    return JsonConvert.DeserializeObject<JSON_Youtube>(theTxt, JSONhandler);
                }
                else
                {
                    ShowError(result);
                    return null;
                }
            }
        }

        #region SearchVideos

        public class SearchOption
        {
            public TimeSpan? LongerThan_Mins { get; set; }
            public TimeSpan? ShorterThan_Mins { get; set; }

            public bool _360VideosOnly { get; set; } = false;
            public Utilitiez.ChannelsEnum? Channel { get; set; }

            public bool NonLiveStreamingVideos { get; set; }
            public bool HDVideosOnly { get; set; } = false;
            public bool VerifiedUserVideosOnly { get; set; } = false;
            public bool PremiumVideosOnly { get; set; } = false;

            public DateTime? CreatedBeforeDate { get; set; }
            public DateTime? CreatedAfterDate { get; set; }
        }
        public async Task<JSON_ListVideos> AdvancedVideoSearch(string Keyword, SearchTypesEnum SearchType, SearchOption SearchOption, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            bool KeepLooping = true;
            List<JSON_VideoMetadata> fin = new List<JSON_VideoMetadata>();
            JSON_ListVideos toreturn = new JSON_ListVideos();
            do
            {
                var parameters = new Dictionary<string, string>
                {
                    { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList()) },
                    { "search", SearchType == SearchTypesEnum.Contains ? Keyword : string.Concat("\"", Keyword.Replace(" ", "+"), "\"") },
                    { "longer_than", SearchOption.LongerThan_Mins.HasValue ? SearchOption.LongerThan_Mins.Value.TotalMinutes.ToString() : null },
                    { "shorter_than", SearchOption.ShorterThan_Mins.HasValue ? SearchOption.ShorterThan_Mins.Value.TotalMinutes.ToString() : null },
                    { "360_degree", SearchOption._360VideosOnly ? "1" : null },
                    { "channel", SearchOption.Channel.HasValue ? SearchOption.Channel.Value.ToString() : null },
                    { "no_live", SearchOption.NonLiveStreamingVideos ? "1" : null },
                    { "hd", SearchOption.HDVideosOnly ? "1" : null },
                    { "verified", SearchOption.VerifiedUserVideosOnly ? "1" : null },
                    { "premium", SearchOption.PremiumVideosOnly ? "1" : null },
                    { "created_before", SearchOption.CreatedBeforeDate.HasValue ? DateTimeToUnixTimestampMilliseconds(SearchOption.CreatedBeforeDate.Value).ToString() : null },
                    { "created_after", SearchOption.CreatedAfterDate.HasValue ? DateTimeToUnixTimestampMilliseconds(SearchOption.CreatedAfterDate.Value).ToString() : null },
                    { "limit", Limit.ToString() },
                    { "page", OffSet.ToString() },
                    { "sort", Sort.ToString() }
                };
                using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
                {
                    HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/videos", parameters.RemoveEmptyValues())).ConfigureAwait(false);
                    string result = await ResPonse.Content.ReadAsStringAsync();
                    return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
                }
            }
            while (!KeepLooping == false);
            return toreturn;
        }

        #endregion

        #region SearchForAUser

        public async Task<JSON_ListUsers> SearchForAUser(string Keyword, SearchTypesEnum SearchType, UsersSortEnum Sort = UsersSortEnum.popular, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers))) },
                { "search", SearchType == SearchTypesEnum.Contains ? Keyword : string.Concat("\"", Keyword.Replace(" ", "+"), "\"") },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/users", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListUsers>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_ListUsers> SearchForAUserByUsername(string Username)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers))) },
                { "usernames", Username },
                { "limit", "10" },
                { "page", "1" },
                { "sort", UsersSortEnum.recent.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/users", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListUsers>(result, JSONhandler) : throw ShowError(result);
            }
        }

        #endregion

        public async Task<JSON_VideoMetadata> VideoMetadata(string VideoID)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)));
            return await Core.SDK.VideoMetadata(VideoID, flds);
        }

        public  async Task<JSON_GetVideoDirectLink> VideoDirectLink(string VideoID)
        {
            return await Core.SDK.VideoDirectLink(VideoID);
        }

        public  async Task<List<JSON_GetVideoDirectLink>> VideoDirectLinkMultiple(List<string> VideosIDs)
        {
            return await Core.SDK.VideoDirectLinkMultiple(VideosIDs);
        }





    }
}
