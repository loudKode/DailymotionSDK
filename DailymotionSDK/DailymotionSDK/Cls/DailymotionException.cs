using System;

namespace DailymotionSDK
{
    public class DailymotionException : Exception
    {
        public DailymotionException(string errorMesage, int errorCode) : base(errorMesage) { }
    }
}
