using DailymotionSDK.JSON;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface IAccount
    {
        Task<JSON_CheckAccessToken> CheckInUseAccessToken();
        Task<JSON_CheckAccessToken> RevokeAccessToken();
        Task<JSON_UserMetadata> UserInfo();
        Task<JSON_APIrateLimits> APIrateLimits();
    }
}