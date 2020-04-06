using DailymotionSDK.JSON;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface IFavorites
    {
       
        Task<bool> Add(string VideoID);
        Task<bool> Remove(string VideoID);
        Task<bool> Exists(string VideoID);
    }
}
