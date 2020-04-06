using DailymotionSDK.JSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public class PlaylistClient : IPlaylist
    {
        private string PlaylistID { get; set; }
        public PlaylistClient(string PlaylistID)
        {
            this.PlaylistID = PlaylistID;
        }





        public async Task<JSON_PlayListMetadata> Edit(string Name = null, string Description = null, bool? SetPrivate = null)
        {
            var parameters = new Dictionary<string, string>
            {
                { "name", Name ?? null },
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsPlaylist))) },
                { "description", Description ?? null },
                { "private", SetPrivate.HasValue ? (SetPrivate.Value==true?"1":"0") : null }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/playlist/{PlaylistID}"));
                HtpReqMessage.Content = new FormUrlEncodedContent(parameters.RemoveEmptyValues());
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_PlayListMetadata>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<bool> Delete()
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new pUri($"/playlist/{PlaylistID}");
                HttpResponseMessage ResPonse = await localHttpClient.DeleteAsync(RequestUri).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> Add(string SourceVideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/playlist/{PlaylistID}/videos/{SourceVideoID}"));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> AddMultiple(string[] VideoIDs)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/playlist/{PlaylistID}/videos?ids={string.Join(",", VideoIDs)}"));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> Remove(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new pUri($"/playlist/{PlaylistID}/videos/{VideoID}");
                HttpResponseMessage ResPonse = await localHttpClient.DeleteAsync(RequestUri).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<JSON_ListVideos> ListVideos(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_")).ToList());
            return await Core.SDK.ListPlaylistVideos(PlaylistID, flds, Sort, Limit, OffSet);
        }

        public async Task<JSON_PlayListMetadata> Metadata()
        {
            var parameters = new Dictionary<string, string> { { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsPlaylist))) } };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new pUri($"/playlist/{PlaylistID}", parameters);
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(RequestUri).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_PlayListMetadata>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<List<json_VideoResolutionUrls>> VideosDirectUrls(int Limit = 100, int OffSet = 1, IProgress<ReportStatus> ReportCls = null, CancellationToken token = default)
        {
            ReportCls = ReportCls ?? new Progress<ReportStatus>();

            var parameters = new Dictionary<string, string>
            {
                { "id", PlaylistID },
                { "fields", string.Join(",", new List<string>() { FieldsVideo.title.ToString(), FieldsVideo.id.ToString() }) },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", VideoSortEnum.recent.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/playlist/{PlaylistID}/videos", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();

                if (ResPonse.IsSuccessStatusCode)
                {
                    var fin = JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler);

                    List<json_VideoResolutionUrls> vUrlz = new List<json_VideoResolutionUrls>();
                    foreach (JSON_VideoMetadata vid in fin.VideoList)
                    {
                        if (token.IsCancellationRequested)
                        {
                            return vUrlz;
                        }

                        DClient client = new DClient(accessToken: authToken, Settings: ConnectionSetting);
                        JSON_GetVideoDirectLink vurl = await client.Mine.Video(vid.id).DirectLink();
                        vurl.VideoResolutionUrls.Name = vurl.metadata.title;
                        vUrlz.Add(vurl.VideoResolutionUrls);
                        int current = Convert.ToInt32(fin.VideoList.IndexOf(vid));
                        ReportCls.Report(new ReportStatus() { ProgressPercentage = current / fin.VideoList.Count * 100, BytesTransferred = current, TotalBytes = fin.VideoList.Count, TextStatus = $"Generating Links for {vid.name}..." });
                    }
                    return vUrlz;
                }
                else
                {
                    ShowError(result);
                    return null;
                }
            }
        }

        public async Task<bool> VideoExists(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/playlist/{PlaylistID}/videos/{VideoID}")).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JObject.Parse(result).SelectToken("list").ToList().Count > 0 : throw ShowError(result);
            }
        }

    }
}
