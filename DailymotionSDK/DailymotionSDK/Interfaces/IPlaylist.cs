using DailymotionSDK.JSON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailymotionSDK
{
    public interface IPlaylist
    {


        

        /// <summary>
        /// edit playlist metadata
        /// </summary>
        Task<JSON_PlayListMetadata> Edit(string Name, string Description, bool? SetPrivate);
        
        /// <summary>
        /// delete playlist
        /// </summary>
        Task<bool> Delete();

        /// <summary>
        /// Add Video To Playlist
        /// </summary>
        /// <param name="SourceVideoID">video id to add to playlist</param>
        Task<bool> Add(string SourceVideoID);

        /// <summary>
        /// Add Multiple Videos To Playlist
        /// </summary>
        /// <param name="SourceVideoIDs">list of video ids to add to playlist</param>
        Task<bool> AddMultiple(string[] SourceVideoIDs);

        /// <summary>
        /// Remove Video From Playlist
        /// </summary>
        /// <param name="VideoID">video id to remove from playlist</param>
        Task<bool> Remove(string VideoID);




        //Task<JSON_ListPlaylists> Search(string Keyword, Utilitiez.PlaylistSortEnum Sort = Utilitiez.PlaylistSortEnum.recent, int OffSet = 1);

        /// <summary>
        /// List Videos In Playlist
        /// </summary>
        Task<JSON_ListVideos> ListVideos(Utilitiez.VideoSortEnum Sort = Utilitiez.VideoSortEnum.recent, int Limit = 100, int OffSet = 1);
        
        /// <summary>
        /// get playlist metadata
        /// </summary>
        Task<JSON_PlayListMetadata> Metadata();
        /// <summary>
        /// Playlist Videos Direct Urls
        /// </summary>
        Task<List<json_VideoResolutionUrls>> VideosDirectUrls(int Limit = 100, int OffSet = 1, IProgress<ReportStatus> ReportCls = null, System.Threading.CancellationToken token = default);

        /// <summary>
        /// Check if Video Exists In Playlist
        /// </summary>
        Task<bool> VideoExists(string VideoID);
    }
}
