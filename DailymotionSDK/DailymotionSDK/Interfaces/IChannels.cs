using DailymotionSDK.JSON;
using System.Threading.Tasks;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public interface IChannels
    {
        
        Task<JSON_ChannelMetadata> Metadata();
        Task<JSON_ListUsers> Subscribers(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1);
        Task<JSON_ListVideos> Videos(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1);
    }
}
