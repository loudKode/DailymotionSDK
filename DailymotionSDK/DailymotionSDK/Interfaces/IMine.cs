using DailymotionSDK.JSON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public interface IMine
    {
        IAccount Account { get; }
        IFavorites Favorites { get; }
        IFeatures Features { get; }
        IHistory History { get; }
        IWatchLater WatchLater { get; }
        IPlaylist Playlist(string PlaylistID);
        ILikes Likes { get; }
        IVideos Video(string VideoID);


        Task<bool> Follow(string UserID);
        Task<bool> UnFollow(string UserID);

        Task<JSON_UploadLocalFile> UploadLocal(object FileToUpload, UploadTypes UploadType, string VideoTitle = null, List<string> VideoTags = null, ChannelsEnum? VideoChannel = null, PrivacyEnum? Privacy = null, IProgress<ReportStatus> ReportCls = null, System.Threading.CancellationToken token = default);
        Task<JSON_RemoteUpload> UploadRemote(string VideoUrl, string VideoTitle, List<string> VideoTags = null, ChannelsEnum? VideoChannel = null, PrivacyEnum? Privacy = null);

        Task Download(string VideoUrl, string FileSaveDir, string FileName, IProgress<ReportStatus> ReportCls = null, System.Threading.CancellationToken token = default);
        Task DownloadLarge(string VideoUrl, string FileSaveDir, string FileName, IProgress<ReportStatus> ReportCls = null, System.Threading.CancellationToken token = default);

        Task<JSON_ListVideos> ListVideos(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1);
        Task<JSON_ListUsers> ListFollowers(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1);
        Task<JSON_ListUsers> ListFollowing(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1);
        Task<JSON_ListPlaylists> ListPlaylists(PlaylistSortEnum Sort = PlaylistSortEnum.recent, int Limit = 100, int OffSet = 1);
        Task<JSON_ListVideos> ListFavorites(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1);

        /// <summary>
        /// list feature videos
        /// </summary>
        Task<JSON_ListVideos> ListFeatures(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1);

        /// <summary>
        /// list videos in history
        /// </summary>
        Task<JSON_ListVideos> ListHistories(Utilitiez.VideoSortEnum Sort = Utilitiez.VideoSortEnum.recent, int Limit = 10, int OffSet = 1);

        /// <summary>
        /// List of videos liked by me
        /// </summary>
        Task<JSON_ListVideos> ListLikes(Utilitiez.VideoSortEnum Sort = Utilitiez.VideoSortEnum.recent, int Limit = 10, int OffSet = 1);

        /// <summary>
        /// list videos in watch later
        /// </summary>
        Task<JSON_ListVideos> ListWatchLater(Utilitiez.VideoSortEnum Sort = Utilitiez.VideoSortEnum.recent, int Limit = 10, int OffSet = 1);



        /// <summary>
        /// create new playlist
        /// </summary>
        Task<JSON_PlayListMetadata> CreatePlaylist(string Name, string Description, bool SetPrivate);


        /// <summary>
        /// search my playlists
        /// </summary>
        Task<JSON_ListPlaylists> SearchPlaylists(string Keyword, PlaylistSortEnum Sort = PlaylistSortEnum.recent, int OffSet = 1);
        Task<JSON_ListVideos> SearchVideos(string ExactKeyword, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1);

        Task<JSON_ListVideos> ListVideosInSubscriptedChannels(ChannelsEnum Channel, int? DurationShorterOrEqual_inMins = null, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1);


    }
}
