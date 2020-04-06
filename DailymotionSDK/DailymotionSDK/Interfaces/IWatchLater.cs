using DailymotionSDK.JSON;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface IWatchLater
    {


        /// <summary>
        /// add video to watch later list
        /// </summary>
        Task<bool> Add(string VideoID);
        
        /// <summary>
        /// remove video from watch later list
        /// </summary>
        Task<bool> Remove(string VideoID);
        
        /// <summary>
        /// check if video exists in watch later list
        /// </summary>
        Task<bool> Exists(string VideoID);
    }
}