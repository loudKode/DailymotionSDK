using DailymotionSDK.JSON;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface IClient
    {
        IMine Mine { get; }
        IUser Users(string UserID);
        IChannels Channel(Utilitiez.ChannelsEnum Channel);
        IGeneral General { get; }

        // ReadOnly Property Favorites As IFavorites









    }
}
