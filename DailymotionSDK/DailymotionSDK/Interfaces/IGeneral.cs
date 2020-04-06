using DailymotionSDK.JSON;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DailymotionSDK.Utilitiez;


namespace DailymotionSDK
{
    public interface IGeneral
    {

        Task<JSON_ListChannels> GetChannelsList();
        Task<JSON_Vimeo> GetVimeoDownloadUrls(string VimeoVideoUrl);
        Task<JSON_Youtube> GetYoutubeDownloadUrls(string YoutubeVideoUrl);

        Task<JSON_ListVideos> AdvancedVideoSearch(string Keyword, SearchTypesEnum SearchType, GeneralClient.SearchOption SearchOption, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1);

        /// <summary>
        /// search for a user
        /// </summary>
        Task<JSON_ListUsers> SearchForAUser(string Keyword, SearchTypesEnum SearchType, UsersSortEnum Sort = UsersSortEnum.popular, int Limit = 100, int OffSet = 1);

        /// <summary>
        /// search for a user by its username
        /// </summary>
        Task<JSON_ListUsers> SearchForAUserByUsername(string Username);

        Task<JSON_VideoMetadata> VideoMetadata(string VideoID);

        Task<JSON_GetVideoDirectLink> VideoDirectLink(string VideoID);
        Task<List<JSON_GetVideoDirectLink>> VideoDirectLinkMultiple(List<string> VideosIDs);



    }
}
