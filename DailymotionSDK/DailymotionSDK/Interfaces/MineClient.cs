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
    public class MineClient : IMine
    {

        public IAccount Account { get { return new AccountClient(); } }
        public IFavorites Favorites { get { return new FavoritesClient(); } }
        public IFeatures Features { get { return new FeaturesClient(); } }
        public IHistory History { get { return new HistoryClient(); } }
        public IWatchLater WatchLater { get { return new WatchLaterClient(); } }
        public IPlaylist Playlist(string PlaylistID) => new PlaylistClient(PlaylistID);
        public ILikes Likes { get { return new LikesClient(); } }
        public IVideos Video(string VideoID) => new VideosClient(VideoID);


        #region ListVideos
        public async Task<JSON_ListVideos> ListVideos(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)));
            return await Core.SDK.ListVideos("me", flds, Sort, Limit, OffSet);
        }
        #endregion

        #region ListFollowers
        public async Task<JSON_ListUsers> ListFollowers(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers)));
            return await Core.SDK.ListFollowers("me", flds, Sort, Limit, OffSet);
        }
        #endregion

        #region ListFollowing
        public async Task<JSON_ListUsers> ListFollowing(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList());  // parameters.Add("fields", String.Join(",", (GetStringsFromClassConstants(GetType(FieldsVideo))).ToList().Where(Function(u) Not u.StartsWith("preview_")).ToList()))
            return await Core.SDK.ListFollowing("me", flds, Sort, Limit, OffSet);
        }
        #endregion

        #region ListPlaylists
        public async Task<JSON_ListPlaylists> ListPlaylists(PlaylistSortEnum Sort = PlaylistSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsPlaylist)));
            return await Core.SDK.ListPlaylists("me", flds, Sort, Limit, OffSet);
        }
        #endregion

        #region ListFavorites
        public async Task<JSON_ListVideos> ListFavorites(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo))) },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new pUri(string.Format("/user/me/favorites"), parameters);
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(RequestUri).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }
        #endregion

        #region ListFeatures
        public async Task<JSON_ListVideos> ListFeatures(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)));
            return await Core.SDK.ListFeatures("me", flds, Sort, Limit, OffSet);
        }
        #endregion

        #region ListHistories
        public async Task<JSON_ListVideos> ListHistories(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo))) },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new pUri(string.Format("/user/me/history"), parameters);
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(RequestUri).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }
        #endregion

        #region ListLikes
        /// <summary>
        /// List of videos liked by me
        /// </summary>
        /// <param name="Sort"></param>
        /// <param name="Limit"></param>
        /// <param name="OffSet"></param>
        /// <returns></returns>
        public async Task<JSON_ListVideos> ListLikes(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_")).ToList());
            return await Core.SDK.ListLikes ("me", flds, Sort, Limit, OffSet);
        }
        #endregion

        #region ListWatchLater
        public async Task<JSON_ListVideos> ListWatchLater(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_")).ToList()) },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                pUri RequestUri = new pUri(string.Format("/user/me/watchlater"), parameters);
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(RequestUri).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }
        #endregion

        #region SearchPlaylists
        public async Task<JSON_ListPlaylists> SearchPlaylists(string Keyword, PlaylistSortEnum Sort = PlaylistSortEnum.recent, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsPlaylist)));
            return await Core.SDK.SearchForAPlaylist("me", flds, Keyword, Sort, OffSet);
        }
        #endregion

        #region SearchVideos
        public async Task<JSON_ListVideos> SearchVideos(string ExactKeyword,  VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)));
            return await Core.SDK.SearchForAVideo("me", flds, ExactKeyword,  Sort, Limit, OffSet);
        }
        #endregion


        public async Task<bool> Follow(string UserID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/me/following/{UserID}"));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }
        public async Task<bool> UnFollow(string UserID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.DeleteAsync(new pUri($"/me/following/{UserID}")).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        #region Upload

        private async Task<JSON_GetUploadUrl> GetUploadUrl()
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/file/upload")).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_GetUploadUrl>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_UploadLocalFile> UploadLocal(object FileToUpload, UploadTypes UploadType, string VideoTitle = null, List<string> VideoTags = null, ChannelsEnum? VideoChannel = null, PrivacyEnum? Privacy = null, IProgress<ReportStatus> ReportCls = null, CancellationToken token = default)
        {
            var uploadUrl = await GetUploadUrl();
            ReportCls = ReportCls ?? new Progress<ReportStatus>();
            ReportCls.Report(new ReportStatus() { Finished = false, TextStatus = "Initializing..." });
            try
            {
                System.Net.Http.Handlers.ProgressMessageHandler progressHandler = new System.Net.Http.Handlers.ProgressMessageHandler(new HCHandler());
                progressHandler.HttpSendProgress += (sender, e) => { ReportCls.Report(new ReportStatus() { ProgressPercentage = e.ProgressPercentage, BytesTransferred = e.BytesTransferred, TotalBytes = e.TotalBytes ?? 0, TextStatus = "Uploading..." }); };
                using (HttpClient localHttpClient = new HtpClient(progressHandler))
                {
                    HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(uploadUrl.upload_url));
                    var MultipartsformData = new MultipartFormDataContent();
                    HttpContent streamContent = null;
                    switch (UploadType)
                    {
                        case UploadTypes.FilePath:
                            streamContent = new StreamContent(new System.IO.FileStream(FileToUpload.ToString(), System.IO.FileMode.Open, System.IO.FileAccess.Read));
                            break;
                        case UploadTypes.Stream:
                            streamContent = new StreamContent((System.IO.Stream)FileToUpload);
                            break;
                        case UploadTypes.BytesArry:
                            streamContent = new StreamContent(new System.IO.MemoryStream((byte[])FileToUpload));
                            break;
                    }
                    MultipartsformData.Add(streamContent, "file");

                    HtpReqMessage.Content = MultipartsformData;
                    // ''''''''''''''''will write the whole content to H.D WHEN download completed'''''''''''''''''''''''''''''
                    using (HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
                    {
                        string result = await ResPonse.Content.ReadAsStringAsync();

                        token.ThrowIfCancellationRequested();

                        var TheRsult = JsonConvert.DeserializeObject<JSON_UploadLocalFile>(result);
                        if (ResPonse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ReportCls.Report(new ReportStatus() { Finished = true, TextStatus = string.Format("[{0}] Uploaded successfully", VideoTitle) });
                            // ''''''''Complete file upload'''''''''''' 
                            using (HtpClient localHttpClient2 = new HtpClient(new HCHandler()))
                            {
                                using (HttpRequestMessage HtpReqMessage2 = new HttpRequestMessage(HttpMethod.Post, new pUri("/me/videos")))
                                {
                                    var parameters = new Dictionary<string, string>();
                                    parameters.Add("url", TheRsult.url);
                                    parameters.Add("title", VideoTitle);
                                    parameters.Add("tags", VideoTags != null ? string.Join(",", VideoTags) : null);
                                    parameters.Add("channel", VideoChannel.HasValue ? VideoChannel.ToString() : null);
                                    if (Privacy.HasValue)
                                    {
                                        switch (Privacy)
                                        {
                                            case PrivacyEnum.Public:
                                                parameters.Add("published", "1");
                                                parameters.Add("private", "0");
                                                break;
                                            case PrivacyEnum.Private:
                                                parameters.Add("published", "0");
                                                parameters.Add("private", "1");
                                                break;
                                        }
                                    }

                                    HtpReqMessage2.Content = new FormUrlEncodedContent(parameters.RemoveEmptyValues());
                                    using (HttpResponseMessage ResPonse2 = await localHttpClient2.SendAsync(HtpReqMessage2, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
                                    {
                                        string resu = await ResPonse2.Content.ReadAsStringAsync();

                                        if (ResPonse2.StatusCode == System.Net.HttpStatusCode.OK)
                                        {
                                            var TheRlt = JsonConvert.DeserializeObject<JSON_CompleteUpload>(resu, JSONhandler);
                                            TheRsult.id = TheRlt.id;
                                            return TheRsult;
                                        }
                                        else
                                        {
                                            throw new DailymotionException(JObject.Parse(resu).SelectToken("error.message").ToString(), (int)ResPonse2.StatusCode);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ReportCls.Report(new ReportStatus() { Finished = true, TextStatus = string.Format("The request returned with HTTP status code {0}", ResPonse.StatusCode) });
                            throw ShowError(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportCls.Report(new ReportStatus() { Finished = true });
                if (ex.Message.ToString().ToLower().Contains("a task was canceled"))
                {
                    ReportCls.Report(new ReportStatus() { TextStatus = ex.Message });
                }
                else
                {
                    throw new DailymotionException(ex.Message, 1001);
                }
                return null;
            }
        }

        public async Task<JSON_RemoteUpload> UploadRemote(string VideoUrl, string VideoTitle, List<string> VideoTags = null, ChannelsEnum? VideoChannel = null, PrivacyEnum? Privacy = null)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("url", VideoUrl);
            parameters.Add("title", VideoTitle);
            //if (VideoTags == null){VideoTags = new List<string>();}
            parameters.Add("tags", VideoTags != null ? string.Join(",", VideoTags) : null);
            parameters.Add("channel", VideoChannel.HasValue ? VideoChannel.ToString() : null);
            if (Privacy.HasValue)
            {
                switch (Privacy)
                {
                    case PrivacyEnum.Public:
                        parameters.Add("published", "1");
                        parameters.Add("private", "0");
                        break;
                    case PrivacyEnum.Private:
                        parameters.Add("published", "0");
                        parameters.Add("private", "1");
                        break;
                }
            }

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri("/me/videos", parameters.RemoveEmptyValues()));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_RemoteUpload>(result, JSONhandler) : throw ShowError(result);
            }
        }

        #endregion

        #region Download

        public async Task Download(string VideoUrl, string FileSaveDir, string FileName, IProgress<ReportStatus> ReportCls = null, CancellationToken token = default)
        {
            ReportCls = ReportCls ?? new Progress<ReportStatus>();
            ReportCls.Report(new ReportStatus() { Finished = false, TextStatus = "Initializing..." });
            try
            {
                System.Net.Http.Handlers.ProgressMessageHandler progressHandler = new System.Net.Http.Handlers.ProgressMessageHandler(new HCHandler());
                progressHandler.HttpReceiveProgress += (sender, e) => { ReportCls.Report(new ReportStatus() { ProgressPercentage = e.ProgressPercentage, BytesTransferred = e.BytesTransferred, TotalBytes = e.TotalBytes ?? 0, TextStatus = "Downloading..." }); };
                using (HtpClient localHttpClient = new HtpClient(progressHandler))
                {
                    HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(VideoUrl));
                    // ''''''''''''''''will write the whole content to H.D WHEN download completed'''''''''''''''''''''''''''''
                    using (HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();
                        if (ResPonse.IsSuccessStatusCode)
                        {
                            ReportCls.Report(new ReportStatus() { Finished = true, TextStatus = (string.Format("[{0}] Downloaded successfully.", FileName)) });
                        }
                        else
                        {
                            ReportCls.Report(new ReportStatus() { Finished = true, TextStatus = ((string.Format("Error code: {0}", ResPonse.StatusCode))) });
                        }

                        ResPonse.EnsureSuccessStatusCode();
                        var stream_ = await ResPonse.Content.ReadAsStreamAsync();
                        string FPathname = System.IO.Path.Combine(FileSaveDir, FileName);
                        using (var fileStream = new System.IO.FileStream(FPathname, System.IO.FileMode.Append, System.IO.FileAccess.Write))
                        {
                            stream_.CopyTo(fileStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportCls.Report(new ReportStatus() { Finished = true });
                if (ex.Message.ToString().ToLower().Contains("a task was canceled"))
                {
                    ReportCls.Report(new ReportStatus() { TextStatus = ex.Message });
                }
                else
                {
                    throw new DailymotionException(ex.Message, 1001);
                }
            }
        }

        public async Task DownloadLarge(string VideoUrl, string FileSaveDir, string FileName, IProgress<ReportStatus> ReportCls = null, CancellationToken token = default)
        {
            ReportCls = ReportCls ?? new Progress<ReportStatus>();
            ReportCls.Report(new ReportStatus() { Finished = false, TextStatus = "Initializing..." });
            try
            {
                System.Net.Http.Handlers.ProgressMessageHandler progressHandler = new System.Net.Http.Handlers.ProgressMessageHandler(new HCHandler());
                progressHandler.HttpReceiveProgress += (sender, e) => { ReportCls.Report(new ReportStatus() { ProgressPercentage = e.ProgressPercentage, BytesTransferred = e.BytesTransferred, TotalBytes = e.TotalBytes ?? 0, TextStatus = "Downloading..." }); };
                using (HtpClient localHttpClient = new HtpClient(progressHandler))
                {
                    using (HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new Uri(VideoUrl), HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();
                        if (ResPonse.IsSuccessStatusCode)
                        {
                            ResPonse.EnsureSuccessStatusCode();
                            // ''''''''''''''' write byte by byte to H.D '''''''''''''''''''''''''''''
                            string FPathname = System.IO.Path.Combine(FileSaveDir, FileName);
                            using (System.IO.Stream streamToReadFrom = await ResPonse.Content.ReadAsStreamAsync())
                            {
                                using (System.IO.Stream streamToWriteTo = System.IO.File.Open(FPathname, System.IO.FileMode.Create))
                                {
                                    await streamToReadFrom.CopyToAsync(streamToWriteTo, 1024, token);
                                }
                            }

                            ReportCls.Report(new ReportStatus() { Finished = true, TextStatus = (string.Format("[{0}] Downloaded successfully.", FileName)) });
                        }
                        else
                        {
                            ReportCls.Report(new ReportStatus() { Finished = true, TextStatus = ((string.Format("Error code: {0}", ResPonse.ReasonPhrase))) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportCls.Report(new ReportStatus() { Finished = true });
                if (ex.Message.ToString().ToLower().Contains("a task was canceled"))
                {
                    ReportCls.Report(new ReportStatus() { TextStatus = ex.Message });
                }
                else
                {
                    throw new DailymotionException(ex.Message, 1001);
                }
            }
        }

        #endregion

        public async Task<JSON_PlayListMetadata> CreatePlaylist(string Name, string Description, bool SetPrivate)
        {
            var parameters = new Dictionary<string, string>
            {
                { "name", Name },
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsPlaylist))) },
                { "description", Description },
                { "private", SetPrivate ? "1" : "0" }
            };
            var encodedContent = new FormUrlEncodedContent(parameters);

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri("/me/playlists"));
                HtpReqMessage.Content = encodedContent;
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_PlayListMetadata>(result, JSONhandler) : throw ShowError(result);
            }
        }
        public async Task<JSON_ListVideos> ListVideosInSubscriptedChannels(ChannelsEnum Channel, int? DurationShorterOrEqual_inMins = null, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "shorter_than", DurationShorterOrEqual_inMins.HasValue ? DurationShorterOrEqual_inMins.Value.ToString() : null },
                { "channel", Channel.ToString() },
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo))) },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/user/me/subscriptions", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }

    }
}
