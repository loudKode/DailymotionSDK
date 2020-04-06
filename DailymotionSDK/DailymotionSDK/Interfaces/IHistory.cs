using DailymotionSDK.JSON;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface IHistory
    {


        /// <summary>
        /// check if video exist in history
        /// </summary>
        Task<bool> Exists(string VideoID);

        /// <summary>
        /// add video to history
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        Task<bool> Add(string VideoID);

        /// <summary>
        /// remove video from history
        /// </summary>
        Task<bool> Remove(string VideoID);
    }
}
