using DailymotionSDK.JSON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public interface IVideos
    {


        Task<bool> Delete();
        Task<JSON_VideoMetadata> Metadata();


        Task<bool> Edit(string VideoTitle = null, string[] VideoTags = null, ChannelsEnum? VideoChannel = null, PrivacyEnum? Privacy = null);

        /// <summary>
        /// edit video channel
        /// </summary>
        Task<ChannelsEnum> EditChannel(ChannelsEnum VideoChannel);

        /// <summary>
        /// edit video title
        /// </summary>
        /// <returns>video name</returns>
        Task<string> EditName(string VideoTitle);

        Task<bool> EditTag(string[] VideoTags);
        Task<bool> SetThumbnail(Uri ThumbnailUrl);
        Task<bool> RemovePassword();
        Task<bool> SetPassword(string Password);
        Task<bool> ChangePrivacy(PrivacyEnum Privacy);
        Task<bool> SchedulingPublicity(DateTime FromDate, DateTime ToDate);
        Task<bool> SchedulingLiveStream(DateTime StartDate, DateTime EndDate);
        Task<JSON_GetVideoDirectLink> DirectLink();


    }
}
