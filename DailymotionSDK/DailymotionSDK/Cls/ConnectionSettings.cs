using System;
using System.Collections.Generic;
using System.Text;
using DailymotionSDK;

namespace DailymotionSDK
{
    public class ConnectionSettings
    {
        public TimeSpan? TimeOut = null;
        public bool? CloseConnection = true;
        public ProxyConfig Proxy = null;
    }
}
