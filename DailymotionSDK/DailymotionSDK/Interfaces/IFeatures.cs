using DailymotionSDK.JSON;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface IFeatures
    {


        /// <summary>
        /// add video to feature
        /// </summary>
        Task<bool> Add(string VideoID);

        /// <summary>
        /// remove video from feature
        /// </summary>
        Task<bool> Remove(string VideoID);

        /// <summary>
        /// check if video exists in feature
        /// </summary>
        Task<bool> Exists(string VideoID);
    }
}
