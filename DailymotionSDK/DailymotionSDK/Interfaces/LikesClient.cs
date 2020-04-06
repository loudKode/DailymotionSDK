using DailymotionSDK.JSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public class LikesClient : ILikes
    {

        public async Task<bool> Exists(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new pUri($"/user/me/likes/{VideoID}");
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(RequestUri).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JObject.Parse(result).SelectToken("list").ToList().Count > 0 : throw ShowError(result);
            }
        }

        public async Task<bool> LikeVideo(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri($"/me/likes/{VideoID}"));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> UnLikeVideo(string VideoID)
        {
            using (HttpClient localHttpClient = new HttpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.DeleteAsync(new pUri($"/me/likes/{VideoID}")).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }


    }
}
