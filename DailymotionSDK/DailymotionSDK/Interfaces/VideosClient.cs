using DailymotionSDK.JSON;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public class VideosClient : IVideos
    {

        private string VideoID { get; set; }
        public VideosClient(string VideoID) => this.VideoID = VideoID;



        public async Task<bool> Delete()
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage();
                HttpResponseMessage ResPonse = await localHttpClient.DeleteAsync(new pUri($"/video/{VideoID}")).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<JSON_VideoMetadata> Metadata()
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)));
            return await Core.SDK.VideoMetadata(VideoID, flds);
        }

        public async Task<bool> Edit(string VideoTitle = null, string[] VideoTags = null, ChannelsEnum? VideoChannel = default(ChannelsEnum?), PrivacyEnum? Privacy = default(PrivacyEnum?))
        {
            var parameters = new Dictionary<string, string>();
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

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}")) { Content = new FormUrlEncodedContent(parameters.RemoveEmptyValues()) };
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? true : throw ShowError(result);
            }
        }

        public async Task<ChannelsEnum> EditChannel(ChannelsEnum VideoChannel)
        {
            var parameters = new Dictionary<string, string> { { "channel", VideoChannel.ToString() } };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}")) { Content = new FormUrlEncodedContent(parameters.RemoveEmptyValues()) };
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JObject.Parse(result).Value<ChannelsEnum>("channel") : throw ShowError(result);
            }
        }

        public async Task<string> EditName(string VideoTitle)
        {
            var parameters = new Dictionary<string, string> { { "title", VideoTitle } };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}"));
                HtpReqMessage.Content = new FormUrlEncodedContent(parameters.RemoveEmptyValues());
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JObject.Parse(result).SelectToken("title").ToString() : throw ShowError(result);
            }
        }

        public async Task<bool> EditTag(string[] VideoTags)
        {
            var parameters = new Dictionary<string, string>() { { "tags", string.Join(",", VideoTags) } };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}")) { Content = new FormUrlEncodedContent(parameters) };
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? true : throw ShowError(result);
            }
        }

        public async Task<bool> ChangePrivacy(PrivacyEnum Privacy)
        {
            var parameters = new Dictionary<string, string>();
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

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}", parameters));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? true : throw ShowError(result);
            }
        }

        public async Task<bool> SetPassword(string Password)
        {
            var parameters = new Dictionary<string, string>() { { "password", Password } };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}"));
                HtpReqMessage.Content = new FormUrlEncodedContent(parameters);
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> RemovePassword()
        {
            var parameters = new Dictionary<string, string>() { { "password", string.Empty } };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}"));
                HtpReqMessage.Content = new FormUrlEncodedContent(parameters);
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> SetThumbnail(Uri ThumbnailUrl)
        {
            var encodedContent = new FormUrlEncodedContent(new Dictionary<string, string>() { { "thumbnail_url", ThumbnailUrl.ToString() } });

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}")) { Content = encodedContent };
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? true : throw ShowError(result);
            }
        }

        public async Task<bool> SchedulingPublicity(DateTime FromDate, DateTime ToDate)
        {
            var parameters = new Dictionary<string, string>
            {
                { "publish_date", Convert.ToString(DateTimeToUnixTimestampMilliseconds(FromDate)) },
                { "expiry_date", Convert.ToString(DateTimeToUnixTimestampMilliseconds(ToDate)) },
                { "expiry_date_availability", "0" }, // 'set private after expiry_date
                { "publish_date_keep_private", "1" }, // 'set private after expiry_date
                { "expiry_date_deletion", "0" } // 'dont delete after expiry_date
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}"));
                HtpReqMessage.Content = new FormUrlEncodedContent(parameters);
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> SchedulingLiveStream(DateTime StartDate, DateTime EndDate)
        {
            var parameters = new Dictionary<string, string>
            {
                { "start_time", Convert.ToString(DateTimeToUnixTimestampMilliseconds(StartDate)) },
                { "end_time", Convert.ToString(DateTimeToUnixTimestampMilliseconds(EndDate)) }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/video/{VideoID}"));
                HtpReqMessage.Content = new FormUrlEncodedContent(parameters);
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<JSON_GetVideoDirectLink> DirectLink()
        {
            return await Core.SDK.VideoDirectLink(VideoID);
        }



    }
}
