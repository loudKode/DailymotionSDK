using DailymotionSDK.JSON;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;

namespace DailymotionSDK
{
    public class AccountClient : IAccount
    {

        public async Task<JSON_CheckAccessToken> CheckInUseAccessToken()
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/auth")).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_CheckAccessToken>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_CheckAccessToken> RevokeAccessToken()
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/logout")).ConfigureAwait(false);
                var result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_CheckAccessToken>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_UserMetadata> UserInfo()
        {
            var parameters = new Dictionary<string, string>() { { "fields", string.Join(",", Utilitiez.GetStringsFromClassConstants(typeof(Utilitiez.FieldsUsers))) } };
            // parameters.Add("fields", String.Join(",", utilitiez.GetStringsFromClassConstants(GetType(utilitiez.FieldsUsers)))) ' "created_time,email,fullname,limits,status,url,verified,videos_total,views_total,playlists_total")

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri("/user/me", parameters));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_UserMetadata>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_APIrateLimits> APIrateLimits()
        {
            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpRequestMessage HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new pUri("/me?fields=limits"));
                HttpResponseMessage ResPonse = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_APIrateLimits>(result, JSONhandler) : throw ShowError(result);
            }
        }

    }
}