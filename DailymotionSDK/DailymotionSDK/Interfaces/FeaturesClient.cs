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
    public class FeaturesClient : IFeatures
    {
        //private string VideoID { get; set; }
        //public FeaturesClient() => this.VideoID = VideoID;


       

        public async Task<bool> Add(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post ,new pUri($"/me/features/{VideoID}"));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> Remove(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.DeleteAsync(new pUri($"/me/features/{VideoID}")).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> Exists(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/me/features/{VideoID}")).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JObject.Parse(result).SelectToken("list").ToList().Count > 0 : throw ShowError(result);
            }
        }

    }
}