using DailymotionSDK.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DailymotionSDK
{
    public static class Basic
    {

        public static string APIbase = "https://api.dailymotion.com";
        public static JsonSerializerSettings JSONhandler = new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
        public static string authToken = null;
        public static string TheUserID = null;
        public static TimeSpan m_TimeOut = System.Threading.Timeout.InfiniteTimeSpan; //' TimeSpan.FromMinutes(60)
        public static bool m_CloseConnection = true;
        public static ConnectionSettings ConnectionSetting = null;


        private static ProxyConfig _proxy;
        public static ProxyConfig m_proxy
        {
            get
            {
                return _proxy ?? new ProxyConfig();
            }
            set
            {
                _proxy = value;
            }
        }

        public class HCHandler : System.Net.Http.HttpClientHandler
        {
            public HCHandler() : base()
            {
                if (m_proxy.SetProxy)
                {
                    base.MaxRequestContentBufferSize = 1 * 1024 * 1024;
                    base.Proxy = new System.Net.WebProxy($"http://{m_proxy.ProxyIP}:{m_proxy.ProxyPort}", true, null, new System.Net.NetworkCredential(m_proxy.ProxyUsername, m_proxy.ProxyPassword));
                    base.UseProxy = m_proxy.SetProxy;
                }
            }
        }

        public class HtpClient : System.Net.Http.HttpClient
        {
            public HtpClient(HCHandler HCHandler) : base(HCHandler)
            {
                base.DefaultRequestHeaders.UserAgent.ParseAdd("DailymotionSDK");
                base.DefaultRequestHeaders.ConnectionClose = m_CloseConnection;
                base.Timeout = m_TimeOut;
                base.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(authToken)) { base.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken); }
            }
            public HtpClient(System.Net.Http.Handlers.ProgressMessageHandler progressHandler) : base(progressHandler)
            {
                base.DefaultRequestHeaders.UserAgent.ParseAdd("DailymotionSDK");
                base.DefaultRequestHeaders.ConnectionClose = m_CloseConnection;
                base.Timeout = m_TimeOut;
                base.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(authToken)) { base.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken); }
            }
        }

        public class pUri : Uri
        {
            public pUri(string ApiAction, Dictionary<string, string> Parameters) : base(APIbase + ApiAction + Utilitiez.AsQueryString(Parameters)) { }
            public pUri(string ApiAction) : base(APIbase + ApiAction) { }
        }

        public static DailymotionException ShowError(string result)
        {
            var errorInfo = JsonConvert.DeserializeObject<JSON_Error>(result, JSONhandler);
            return new DailymotionException(errorInfo._ErrorMessage, errorInfo._ERRORCODE);
        }

        public static Dictionary<string, string> RemoveEmptyValues(this Dictionary<string, string> dictionary)
        {
            var badKeys = dictionary.Where(P => string.IsNullOrEmpty(P.Value)).Select(P => P.Key).ToList();
            badKeys.ForEach(k => dictionary.Remove(k));
            return dictionary;
        }


    }
}
