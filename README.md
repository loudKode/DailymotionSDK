## DailymotionSDK


`Download:`[https://github.com/loudKode/DailymotionSDK/releases](https://github.com/loudKode/DailymotionSDK/releases)<br>
`NuGet:`
[![NuGet](https://img.shields.io/nuget/v/DeQmaTech.DailymotionSDK.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/DeQmaTech.DailymotionSDK)<br>

**Features**
* Assemblies for .NET 4.5.2 and .NET Standard 2.0 and .NET Core 2.1
* Just one external reference (Newtonsoft.Json)
* Easy installation using NuGet
* Upload/Download tracking support
* Proxy Support
* Upload/Download cancellation support


## List of functions
**Account**
> * CheckInUseAccessToken
> * RevokeAccessToken
> * UserInfo
> * APIrateLimits

**Authentication**
> * By_AddressBar
> * By_VerificationCode
> * By_UsernameAndPassword
> * ExchangingVerificationCode_For_Token
> * RenewExpiredToken

**Channels**
> * Metadata
> * Subscribers
> * Videos

**Favorites**
> * Add
> * Remove
> * Exists

**Features**
> * Add
> * Remove
> * Exists

**General**
> * GetChannelsList
> * GetVimeoDownloadUrls
> * GetYoutubeDownloadUrls
> * AdvancedVideoSearch
> * SearchForAUser
> * SearchForAUserByUsername
> * VideoMetadata
> * VideoDirectLink
> * VideoDirectLinkMultiple

**History**
> * Exists
> * Add
> * Remove

**Likes**
> * Exists
> * LikeVideo
> * UnLikeVideo

**Playlist**
> * Edit
> * Delete
> * Add
> * AddMultiple
> * Remove
> * ListVideos
> * Metadata
> * VideosDirectUrls
> * VideoExists

**User**
> * Metadata
> * ListLikes
> * ListFeatures
> * ListFollowers
> * ListFollowing
> * ListVideos
> * ListPlaylists
> * ListPlaylistVideos
> * SearchPlaylists
> * SearchVideos

**Videos**
> * Delete
> * Metadata
> * Edit
> * EditChannel
> * EditName
> * EditTag
> * SetThumbnail
> * RemovePassword
> * SetPassword
> * ChangePrivacy
> * SchedulingPublicity
> * SchedulingLiveStream
> * DirectLink

**WatchLater**
> * Add
> * Remove
> * Exists


## code map
[HTML](https://dl3.pushbulletusercontent.com/bNk3fMxCWYW8SGChrrIOto0hQrg22pde/adc41-7xwbq.html)

![https://res.cloudinary.com/dqo5lh7cs/image/upload/v1576051453/Github/d001.png](https://res.cloudinary.com/dqo5lh7cs/image/upload/v1576051453/Github/d001.png)

## code simples
```vb.net
Dim tkn = Await DailymotionSDK.Authentication.By_UsernameAndPassword("api_key", "api_secret", "username", "password")

Dim dcnt As DailymotionSDK.IClient = New DailymotionSDK.DClient(tkn.access_token, New DailymotionSDK.ConnectionSettings With {.CloseConnection = True, .TimeOut = TimeSpan.FromMinutes(80), .Proxy = New DailymotionSDK.ProxyConfig With {.SetProxy = True, .ProxyIP = "127.0.0.1", .ProxyPort = 80, .ProxyUsername = "user", .ProxyPassword = "123456"}})

''Channel
Await dcnt.Channel(ChannelsEnum.animals).Metadata
Await dcnt.Channel(ChannelsEnum.animals).Videos(VideoSortEnum.recent, 50, 1)
Await dcnt.Channel(ChannelsEnum.animals).Subscribers(UsersSortEnum.recent, 50, 1)

''Mine.Account
Await dcnt.Mine.Account.CheckInUseAccessToken
Await dcnt.Mine.Account.RevokeAccessToken
Await dcnt.Mine.Account.UserInfo
Await dcnt.Mine.Account.APIrateLimits

''Mine.Favorites
Await dcnt.Mine.Favorites.Add("video_id")
Await dcnt.Mine.Favorites.Remove("video_id")
Await dcnt.Mine.Favorites.Exists("video_id")

''Mine.Features
Await dcnt.Mine.Features.Add("video_id")
Await dcnt.Mine.Features.Remove("video_id")
Await dcnt.Mine.Features.Exists("video_id")

''Mine.History
Await dcnt.Mine.History.Add("video_id")
Await dcnt.Mine.History.Remove("video_id")
Await dcnt.Mine.History.Exists("video_id")

''Mine.Playlist
Await dcnt.Mine.Playlist("playlist_id").Add("video_id")
Await dcnt.Mine.Playlist("playlist_id").Remove("video_id")
Await dcnt.Mine.Playlist("playlist_id").Delete
Await dcnt.Mine.Playlist("playlist_id").AddMultiple(New String() {"video_id", "video_id"})
Await dcnt.Mine.Playlist("playlist_id").Edit(Nothing, Nothing, True)
Await dcnt.Mine.Playlist("playlist_id").ListVideos(VideoSortEnum.recent, 30, 1)
Await dcnt.Mine.Playlist("playlist_id").Metadata
Await dcnt.Mine.Playlist("playlist_id").VideoExists("video_id")
Await dcnt.Mine.Playlist("playlist_id").VideosDirectUrls(30, 1, Nothing, Nothing)

''Mine.Likes
Await dcnt.Mine.Likes.LikeVideo("video_id")
Await dcnt.Mine.Likes.UnLikeVideo("video_id")
Await dcnt.Mine.Likes.Exists("video_id")

''Mine.WatchLater
Await dcnt.Mine.WatchLater.Add("video_id")
Await dcnt.Mine.WatchLater.Remove("video_id")
Await dcnt.Mine.WatchLater.Exists("video_id")

''Mine.Video
Await dcnt.Mine.Video("video_id").Delete
Await dcnt.Mine.Video("video_id").EditTag(New String() {"tag1", "tag2"})
Await dcnt.Mine.Video("video_id").EditName("new video name")
Await dcnt.Mine.Video("video_id").EditChannel(ChannelsEnum.animals)
Await dcnt.Mine.Video("video_id").ChangePrivacy(PrivacyEnum.Private)
Await dcnt.Mine.Video("video_id").DirectLink
Await dcnt.Mine.Video("video_id").Edit("new video name", New String() {"tag1", "tag2"}, ChannelsEnum.animals, PrivacyEnum.Private)
Await dcnt.Mine.Video("video_id").Metadata
Await dcnt.Mine.Video("video_id").RemovePassword
Await dcnt.Mine.Video("video_id").SchedulingLiveStream(New Date, New Date)
Await dcnt.Mine.Video("video_id").SchedulingPublicity(New Date, New Date)
Await dcnt.Mine.Video("video_id").SetPassword("2020")
Await dcnt.Mine.Video("video_id").SetThumbnail(New Uri("https://www.domain.com/wat.jpg"))


''Mine
Await dcnt.Mine.ListFavorites
Await dcnt.Mine.ListFeatures
Await dcnt.Mine.ListFollowers
Await dcnt.Mine.ListFollowing
Await dcnt.Mine.ListHistories
Await dcnt.Mine.ListLikes
Await dcnt.Mine.ListPlaylists
Await dcnt.Mine.ListVideos
Await dcnt.Mine.ListWatchLater
Await dcnt.Mine.ListVideosInSubscriptedChannels(ChannelsEnum.animals)
Await dcnt.Mine.Follow("user_id")
Await dcnt.Mine.UnFollow("user_id")
Await dcnt.Mine.CreatePlaylist("home", "my home videos", False)
Await dcnt.Mine.SearchPlaylists("home")
Await dcnt.Mine.SearchVideos("cat", SearchTypesEnum.Contains)
Dim CancelToken As New Threading.CancellationTokenSource()
Dim _ReportCls As New Progress(Of DailymotionSDK.ReportStatus)(Sub(r) Button1.Text = $"{r.BytesTransferred}/{r.TotalBytes} - {r.ProgressPercentage} <{If(r.TextStatus, "Downloading...")}>")
Await dcnt.Mine.UploadLocal("C:\Users\fle.rar", UploadTypes.FilePath, "my new video", Nothing, ChannelsEnum.kids, PrivacyEnum.Private, _ReportCls, CancelToken.Token)
Await dcnt.Mine.UploadRemote("https://www.doman.com/mymov.mp4", "my new video")
Await dcnt.Mine.Download("https://www.dailymotion.com/video/xxxxx", "C:\downloads", "mymov.mp4", _ReportCls, CancelToken.Token)
Await dcnt.Mine.DownloadLarge("https://www.dailymotion.com/video/xxxxx", "C:\downloads", "mymov.mp4", _ReportCls, CancelToken.Token)

''Users
Await dcnt.Users("user_id").ListFeatures
Await dcnt.Users("user_id").ListLikes
Await dcnt.Users("user_id").ListFollowers
Await dcnt.Users("user_id").ListFollowing
Await dcnt.Users("user_id").ListPlaylists
Await dcnt.Users("user_id").ListPlaylistVideos("playlist_id")
Await dcnt.Users("user_id").ListVideos
Await dcnt.Users("user_id").Metadata
Await dcnt.Users("user_id").SearchPlaylists("myplay")
Await dcnt.Users("user_id").SearchVideos("myvid", SearchTypesEnum.Contains)

''General
Await dcnt.General.AdvancedVideoSearch("emy", SearchTypesEnum.Contains, New DailymotionSDK.GeneralClient.SearchOption With {.Channel = ChannelsEnum.shortfilms, .ShorterThan_Mins = TimeSpan.FromMinutes(2)}, VideoSortEnum.recent, 40, 1)
Await dcnt.General.GetChannelsList
Await dcnt.General.GetVimeoDownloadUrls("http_vimeo")
Await dcnt.General.GetYoutubeDownloadUrls("http_youtube")
Await dcnt.General.SearchForAUser("james", SearchTypesEnum.Contains, UsersSortEnum.popular, 40, 1)
Await dcnt.General.SearchForAUserByUsername("jamesheck")
Await dcnt.General.VideoDirectLink("https://www.dailymotion.com/video/xxxxx")
Await dcnt.General.VideoDirectLinkMultiple(New List(Of String) From {{"https://www.dailymotion.com/video/xxxxx"}, {"https://www.dailymotion.com/video/xxxxx"}})
Await dcnt.General.VideoMetadata("video_id")
```
