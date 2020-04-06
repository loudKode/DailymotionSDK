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

namespace DailymotionSDK
{
   public  class Authentication
    {

      public   enum ScopesEnum
        {
            email,
            userinfo,
            manage_videos,
            // manage_comments
            manage_playlists,
            manage_tiles,
            manage_subscriptions,
            manage_friends,
            manage_likes,
            manage_features,
            manage_history,
            feed,
            // read_insights
            // manage_domains
            // manage_applications
            // manage_app_connections
            // manage_analytics
            // manage_claim_rules
            // manage_player
            // manage_records
            manage_subtitles
        }
     public    enum ResponseType
        {
            token,
            code,
            authorization_code,
            password,
            refresh_token
        }
        public static string By_AddressBar(string ClientID, List<ScopesEnum> Scope = null)
        {
            string URL = "https://www.dailymotion.com/oauth/authorize";
            var parameters = new Dictionary<string, string>();
            parameters.Add("response_type", ResponseType.token.ToString());
            parameters.Add("client_id", ClientID);
            parameters.Add("redirect_uri", "https://unlimitedillegal.altervista.org/Dailymotion/app.html");
            parameters.Add("display", "popup");
            parameters.Add("scope", string.Join(" ", Scope.Cast<string>()));
            return URL + Utilitiez.AsQueryString(parameters);
        }
        public static string By_VerificationCode(string ClientID, string Scope = null)
        {
            string URL = "https://www.dailymotion.com/oauth/authorize";
            var parameters = new Dictionary<string, string>();
            parameters.Add("response_type", ResponseType.code.ToString());
            parameters.Add("client_id", ClientID);
            parameters.Add("redirect_uri", "https://unlimitedillegal.altervista.org/Dailymotion/app.html");
            parameters.Add("display", "popup");
            parameters.Add("scope", Scope);
            return URL + Utilitiez.AsQueryString(parameters);
        }

        public static async Task<JSON_ExchangingVerificationCodeForToken> ExchangingVerificationCode_For_Token(string AuthorizationCode, string ClientID, string ClientSecret)
        {
            string URL = "https://www.dailymotion.com/oauth/authorize";
            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", ResponseType.authorization_code.ToString());
            parameters.Add("client_id", ClientID);
            parameters.Add("client_secret", ClientSecret);
            parameters.Add("redirect_uri", "https://unlimitedillegal.altervista.org/Dailymotion/app.html");
            parameters.Add("code", AuthorizationCode);

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(URL + Utilitiez.AsQueryString(parameters)));
                using (HttpResponseMessage response = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var TheRsult = JsonConvert.DeserializeObject<JSON_ExchangingVerificationCodeForToken>(result,JSONhandler );
                    if (response.IsSuccessStatusCode)
                    {
                        return TheRsult;
                    }
                    else
                    {
                        throw new DailymotionException(TheRsult._ErrorMessage, (int)response.StatusCode);
                    }
                }
            }
        }

        public static async Task<JSON_TokenfromUsernamePassword> By_UsernameAndPassword(string APIKey, string APISecret, string Username, string Password)
        {
            string URL = "https://api.dailymotion.com/oauth/token";
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", ResponseType.password.ToString() },
                { "client_id", APIKey },
                { "client_secret", APISecret },
                { "redirect_uri", "https://unlimitedillegal.altervista.org/Dailymotion/app.html" },
                { "username", Username },
                { "password", Password },
                { "scope", "email userinfo manage_videos manage_comments manage_playlists manage_tiles manage_subscriptions manage_friends manage_likes manage_features manage_history manage_subtitles" } // String.Join(" ", [Enum].GetValues(GetType(ScopesEnum)).Cast(Of ScopesEnum)().Select(Function(x) x.ToString()).ToArray())) '"email userinfo manage_videos manage_comments manage_playlists manage_tiles manage_subscriptions manage_friends manage_likes manage_features")
            };
            var encodedContent = new FormUrlEncodedContent(parameters);

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(URL)){Content = encodedContent};
                using (HttpResponseMessage response = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var TheRsult = JsonConvert.DeserializeObject<JSON_TokenfromUsernamePassword>(result);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (TheRsult._error != "invalid_request")
                        {
                            return TheRsult;
                        }
                        else
                        {
                            throw new DailymotionException(TheRsult._ErrorMessage, (int)response.StatusCode);
                        }
                    }
                    else
                    {
                        throw new DailymotionException(response.ReasonPhrase, (int)response.StatusCode);
                    }
                }
            }
        }

        public static async Task<JSON_ExchangingVerificationCodeForToken> RenewExpiredToken(string TheRefreshToken, string ClientID, string ClientSecret)
        {
            string URL = "https://api.dailymotion.com/oauth/token";
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", ResponseType.refresh_token.ToString() },
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "refresh_token", TheRefreshToken }
            };
            var encodedContent = new FormUrlEncodedContent(parameters);

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                var HtpReqMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(URL)){Content = encodedContent};
                using (HttpResponseMessage response = await localHttpClient.SendAsync(HtpReqMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var TheRsult = JsonConvert.DeserializeObject<JSON_ExchangingVerificationCodeForToken>(result);
                    if (response.IsSuccessStatusCode)
                    {
                        return TheRsult;
                    }
                    else
                    {
                        throw new DailymotionException(TheRsult._ErrorMessage, (int)response.StatusCode);
                    }
                }
            }
        }

    }
}
