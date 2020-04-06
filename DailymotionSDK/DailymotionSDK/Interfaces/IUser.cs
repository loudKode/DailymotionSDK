using DailymotionSDK.JSON;
using System;
using System.Threading.Tasks;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public interface IUser
    {

        /// <summary>
        /// get User Metadata By providing User Page url
        /// </summary>
        Task<JSON_UserMetadata> Metadata(Uri UserPage);
        /// <summary>
        /// get User Metadata By providing UserID
        /// </summary>
        Task<JSON_UserMetadata> Metadata();



        Task<JSON_ListVideos> ListLikes(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1);

        /// <summary>
        /// List User Feature Videos
        /// </summary>
        Task<JSON_ListVideos> ListFeatures(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1);

        /// <summary>
        /// List User Followers
        /// </summary>
        Task<JSON_ListUsers> ListFollowers(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1);

        /// <summary>
        /// List User Following
        /// </summary>
        Task<JSON_ListUsers> ListFollowing(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1);

        /// <summary>
        /// List User Videos
        /// </summary>
        Task<JSON_ListVideos> ListVideos(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1);

        /// <summary>
        /// List User Playlists
        /// </summary>
        Task<JSON_ListPlaylists> ListPlaylists(PlaylistSortEnum Sort = PlaylistSortEnum.recent, int Limit = 100, int OffSet = 1);

        /// <summary>
        /// List Playlist Videos
        /// </summary>
        Task<JSON_ListVideos> ListPlaylistVideos(string PlaylistID, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1);

        /// <summary>
        /// Search User Playlists
        /// </summary>
        Task<JSON_ListPlaylists> SearchPlaylists(string Keyword, PlaylistSortEnum Sort = PlaylistSortEnum.recent, int OffSet = 1);

        /// <summary>
        /// Search User videos
        /// </summary>
        Task<JSON_ListVideos> SearchVideos(string ExactKeyword, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1);





    }
}
