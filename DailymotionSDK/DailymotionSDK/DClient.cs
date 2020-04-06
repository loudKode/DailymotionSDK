using DailymotionSDK.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public class DClient : IClient
    {

        public DClient(string accessToken, ConnectionSettings Settings = null)
        {
            ServicePointManager.Expect100Continue = true; ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            authToken = accessToken;
            ConnectionSetting = Settings;

            if (Settings == null)
            {
                m_proxy = null;
            }
            else
            {
                m_proxy = Settings.Proxy;
                m_CloseConnection = Settings.CloseConnection ?? true;
                m_TimeOut = Settings.TimeOut ?? TimeSpan.FromMinutes(60);
            }
        }



        public IMine Mine { get { return new MineClient(); } }
        public IUser Users(string UserID) => new UserClient(UserID);
        public IChannels Channel(ChannelsEnum Channel) => new ChannelsClient(Channel);
        public IGeneral General { get { return new GeneralClient(); } }



    }
}
