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
    public class HistoryClient : IHistory
    {


        public async Task<bool> Exists(string VideoID)
        {
            var parameters = new Dictionary<string, string>();
            // parameters.Add("fields", String.Join(",", utilitiez.GetStringsFromClassConstants(GetType(FieldsVideo))))

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/me/history/{VideoID}", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JObject.Parse(result).SelectToken("list").ToList().Count > 0 : throw ShowError(result);
            }
        }

        public async Task<bool> Add(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(HttpMethod.Post ,new pUri(string.Format("/me/history/{0}", VideoID)));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> Remove(string VideoID)
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var RequestUri = new pUri(string.Format("/me/history/{0}", VideoID));
                HttpResponseMessage ResPonse = await localHttpClient.DeleteAsync(RequestUri).ConfigureAwait(false);
                return ResPonse.StatusCode == System.Net.HttpStatusCode.OK ? true : throw ShowError(await ResPonse.Content.ReadAsStringAsync());
            }
        }

    }
}