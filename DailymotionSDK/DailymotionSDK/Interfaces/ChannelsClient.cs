using DailymotionSDK.JSON;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public class ChannelsClient : IChannels
    {

        private string Channel { get; set; }
        public ChannelsClient(ChannelsEnum? Channel) => this.Channel = Channel.ToString();



        public async Task<JSON_ChannelMetadata> Metadata()
        {
            var parameters = new Dictionary<string, string> { { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsChannels))) } };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/channel/{Channel.ToString()}", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ChannelMetadata>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_ListUsers> Subscribers(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers))) },
                { "limit", Limit.ToString() },
                { "page", OffSet.ToString() },
                { "sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/channel/{Channel.ToString()}/users", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListUsers>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_ListVideos> Videos(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            var parameters = new Dictionary<string, string>
            {
            {"fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList())},
            {"limit", Limit.ToString() },
            {"page", OffSet.ToString() },
            {"sort", Sort.ToString() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/channel/{Channel.ToString()}/videos", parameters)).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_ListVideos>(result, JSONhandler) : throw ShowError(result);
            }
        }

    }
}
