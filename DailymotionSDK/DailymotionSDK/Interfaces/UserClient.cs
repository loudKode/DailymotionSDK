using DailymotionSDK.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static DailymotionSDK.Basic;
using static DailymotionSDK.Utilitiez;

namespace DailymotionSDK
{
    public class UserClient : IUser
    {
        private string UserID { get; set; }
        public UserClient(string UserID) => this.UserID = UserID;


        public async Task<JSON_UserMetadata> Metadata(Uri UserPage)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers))) },
                { "usernames", UserPage.ToString().Split('/').Last() }
            };

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri("/users", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();

                if (ResPonse.IsSuccessStatusCode)
                {
                    var fin = JsonConvert.DeserializeObject<JSON_ListUsers>(result, JSONhandler);
                    return fin.total.Equals(0) ? null : fin.UsersList[0];
                }
                else
                {
                    ShowError(result);
                    return null;
                }
            }
        }
        public async Task<JSON_UserMetadata> Metadata()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("fields", string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers))));

            using (HtpClient localHttpClient = new HtpClient(new HCHandler()))
            {
                HttpResponseMessage ResPonse = await localHttpClient.GetAsync(new pUri($"/user/{UserID}", parameters)).ConfigureAwait(false);
                string result = await ResPonse.Content.ReadAsStringAsync();
                return ResPonse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<JSON_UserMetadata>(result, JSONhandler) : throw ShowError(result);
            }
        }

        public async Task<JSON_ListVideos> ListLikes(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_")).ToList());
            return await Core.SDK.ListLikes("me", flds, Sort, Limit, OffSet);
        }
        public async Task<JSON_ListVideos> ListFeatures(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 10, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList());
            return await Core.SDK.ListFeatures("me", flds, Sort, Limit, OffSet);
        }
        public async Task<JSON_ListUsers> ListFollowers(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList());  // parameters.Add("fields", String.Join(",", (GetStringsFromClassConstants(GetType(FieldsVideo))).ToList().Where(Function(u) Not u.StartsWith("preview_")).ToList()))
            return await Core.SDK.ListFollowers(UserID, flds, Sort, Limit, OffSet);
        }
        public async Task<JSON_ListUsers> ListFollowing(UsersSortEnum Sort = UsersSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsUsers)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList());  // parameters.Add("fields", String.Join(",", (GetStringsFromClassConstants(GetType(FieldsVideo))).ToList().Where(Function(u) Not u.StartsWith("preview_")).ToList()))
            return await Core.SDK.ListFollowing(UserID, flds, Sort, Limit, OffSet);
        }
        public async Task<JSON_ListVideos> ListVideos(VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList());  // parameters.Add("fields", String.Join(",", (GetStringsFromClassConstants(GetType(FieldsVideo))).ToList().Where(Function(u) Not u.StartsWith("preview_")).ToList()))
            return await Core.SDK.ListVideos(UserID, flds, Sort, Limit, OffSet);
        }
        public async Task<JSON_ListPlaylists> ListPlaylists(PlaylistSortEnum Sort = PlaylistSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsPlaylist)));
            return await Core.SDK.ListPlaylists(UserID, flds, Sort, Limit, OffSet);
        }
        public async Task<JSON_ListVideos> ListPlaylistVideos(string PlaylistID, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsVideo)).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList());
            return await Core.SDK.ListPlaylistVideos(PlaylistID, flds, Sort, Limit, OffSet);
        }
        public async Task<JSON_ListPlaylists> SearchPlaylists(string Keyword, PlaylistSortEnum Sort = PlaylistSortEnum.recent, int OffSet = 1)
        {
            string flds = string.Join(",", GetStringsFromClassConstants(typeof(FieldsPlaylist)));
            return await Core.SDK.SearchForAPlaylist(UserID, flds, Keyword, Sort, OffSet);
        }
        public async Task<JSON_ListVideos> SearchVideos(string ExactKeyword, VideoSortEnum Sort = VideoSortEnum.recent, int Limit = 100, int OffSet = 1)
        {
            string flds = string.Join(",", (GetStringsFromClassConstants(typeof(FieldsVideo))).ToList().Where(u => !u.StartsWith("preview_") && !u.StartsWith("publish_date") && !u.StartsWith("expiry_date_deletion") && !u.StartsWith("expiry_date")).ToList());
            return await Core.SDK.SearchForAVideo(UserID, flds, ExactKeyword, Sort, Limit, OffSet);
        }


    }
}
