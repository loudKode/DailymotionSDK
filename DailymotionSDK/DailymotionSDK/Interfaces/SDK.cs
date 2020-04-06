using DailymotionSDK.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK.Core
{
    internal class SDK
    {


        public static async Task<JSON_ListVideos> ListVideos(string UserID, string Fields, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields",Fields },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}/videos", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListUsers> ListFollowers(string UserID, string Fields, UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", Fields },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}/followers", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListUsers>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListUsers> ListFollowing(string UserID, string Fields, UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", Fields },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}/following", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListUsers>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListPlaylists> ListPlaylists(string UserID, string Fields, PlaylistSortEnum Sort = PlaylistSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", Fields },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}/playlists", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListPlaylists>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListVideos> ListPlaylistVideos(string PlaylistID, string Fields, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "id", PlaylistID },
                { "fields", Fields },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/playlist/{PlaylistID}/videos", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListPlaylists> SearchForAPlaylist(string UserID, string Fields, string Keyword, PlaylistSortEnum Sort = PlaylistSortEnum.recent, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "owner", UserID },
                { "search", Keyword },
                { "fields", Fields },
                { "limit", "100" },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/playlists", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListPlaylists>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListVideos> SearchForAVideo(string UserID, string Fields, string Keyword,  VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", Fields },
             { "search", Keyword },//   { "search", SearchType == SearchTypesEnum.Contains ? Keyword : string.Concat("\"", Keyword.Replace(" ", "+"), "\"") },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}/videos", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListVideos> ListFeatures(string UserID, string Fields,VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", Fields },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}/features", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_ListVideos> ListLikes(string UserID, string Fields,VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", Fields },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}/likes", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_VideoMetadata> VideoMetadata(string VideoID, string Fields)
        {
            var parameters = new Dictionary<string, string>() { { "fields", Fields } };
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/video/{VideoID}", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_VideoMetadata>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public static async Task<JSON_GetVideoDirectLink> VideoDirectLink(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new Uri($"https://www.dailymotion.com/embed/video/{VideoID}");
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(RequestUri).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();

                if (ResPonse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string partONE = "var config = {";
                    string partTWO = "};";
                    var ExtractTheDlink = Between(result, partONE, partTWO);
                    ExtractTheDlink = string.Concat("{", ExtractTheDlink, "}");
                    var TheRsult = JsonConvert.DeserializeObject<JSON_GetVideoDirectLink>(ExtractTheDlink, JSONhandler);

                    if (TheRsult.metadata.qualities.auto == null)
                    {
                        throw new DailymotionException("video maybe private or password protected", (int)ResPonse.StatusCode);
                    }
                    TheRsult.VideoResolutionUrls = await FormatDirectLnks(TheRsult.metadata.qualities.auto[0].url);
                    return TheRsult;
                }
                else
                {
                    throw new DailymotionException(ResPonse.ReasonPhrase, (int)ResPonse.StatusCode);
                }
            }
        }

        private static async Task<json_VideoResolutionUrls> FormatDirectLnks(string lnk)
        {
            Dictionary<string, string> nonDub = new Dictionary<string, string>();
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(lnk));
                using (HttpResponseMessage response = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                {
                    var result = await response.Content.ReadAsStringAsync();


                    var remove_And = result.Replace("#EXTM3U", "").Replace("#EXT-X-STREAM-INF:", "*").Split('*');
                    foreach (string x in remove_And)
                    {
                        if (string.IsNullOrEmpty(x) || x.Length < 10)
                        {
                            continue;
                        }

                        var clr = x.Split(new string[] { ",RESOLUTION" }, StringSplitOptions.None);
                        foreach (string y in clr)
                        {
                            if (y.StartsWith("="))
                            {
                                var xx = string.Concat("RESOLUTION", y);
                                var Reslution = Between(xx, "NAME=\"", "\",");
                                if (!nonDub.ContainsKey(Reslution)) { nonDub.Add(Reslution, Between(xx, "PROGRESSIVE-URI=\"", "#")); }
                            }
                        }
                    }

                    json_VideoResolutionUrls final = new json_VideoResolutionUrls();
                    {
                        var withBlock = final;
                        withBlock.R_144 = (from r in nonDub where r.Key == "144" select r.Value).FirstOrDefault();
                        withBlock.R_240 = (from r in nonDub where r.Key == "240" select r.Value).FirstOrDefault();
                        withBlock.R_380 = (from r in nonDub where r.Key == "380" select r.Value).FirstOrDefault();
                        withBlock.R_480 = (from r in nonDub where r.Key == "480" select r.Value).FirstOrDefault();
                        withBlock.R_720 = (from r in nonDub where r.Key == "720" select r.Value).FirstOrDefault();
                        withBlock.R_1080 = (from r in nonDub where r.Key == "1080" select r.Value).FirstOrDefault();
                        withBlock.R_1440 = (from r in nonDub where r.Key == "1440" select r.Value).FirstOrDefault();
                        withBlock.R_2160 = (from r in nonDub where r.Key == "2160" select r.Value).FirstOrDefault();
                    }
                    return final;
                }
            }
        }

        public static async Task<List<JSON_GetVideoDirectLink>> VideoDirectLinkMultiple(List<string> VideosIDs)
        {
            List<JSON_GetVideoDirectLink> retLst = new List<JSON_GetVideoDirectLink>();
            foreach (var vid in VideosIDs)
            {
                retLst.Add(await VideoDirectLink(vid));
            }
            return retLst;
        }







    }
}
