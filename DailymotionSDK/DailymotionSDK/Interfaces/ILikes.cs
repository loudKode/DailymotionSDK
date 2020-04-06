using DailymotionSDK.JSON;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface ILikes
    {

        /// <summary>
        /// check if video exists in your likes list
        /// </summary>
        Task<bool> Exists(string VideoID);
        /// <summary>
        /// like a video
        /// </summary>
        Task<bool> LikeVideo(string VideoID);
        /// <summary>
        /// unlike a video
        /// </summary>
        Task<bool> UnLikeVideo(string VideoID);
    }
}
