Imports DailymotionSDK.Utilitiez


Public Class Form1


    Private Async Sub Button1_ClickAsync(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim tkn = Await DailymotionSDK.Authentication.By_UsernameAndPassword("2b418cbe1497e5f65b06", "2e0ef5fc8dd5bfe9670f2f15aa7b8633f77be87c", "xxxxxxxx", "xxxxxxx")
        'FlowLayoutPanel1.Controls.Add(New PropertyGrid With {.SelectedObject = tkn, .Width = 250, .Height = 350, .HelpVisible = False})

        'Dim dcnt As DailymotionSDK.IClient = New DailymotionSDK.DClient(tkn.access_token, Nothing)

        Dim dcnt As DailymotionSDK.IClient = New DailymotionSDK.DClient("djdXAFwPC1xQF1pYRE5EEgQbVxgMREBbGgxDDwYNEgZD", Nothing)

        'Dim rslt = Await dcnt.Mine.Account.UserInfo()
        'FlowLayoutPanel1.Controls.Add(New PropertyGridEx(rslt))

        'Dim rslt = Await dcnt.Mine.ListFollowers(UsersSortEnum.recent, 30, 1)
        'rslt.UsersList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        'Dim rslt = Await dcnt.Mine.ListVideos(UsersSortEnum.recent, 30, 1)
        'rslt.VideoList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        'Dim rslt = Await dcnt.Mine.ListVideosInSubscriptedChannels(ChannelsEnum.tv)
        'rslt.VideoList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        'Dim rslt = Await dcnt.Channel(ChannelsEnum.tv).Videos
        'rslt.VideoList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        'Dim rslt = Await dcnt.Mine.ListPlaylists(UsersSortEnum.recent, 30, 1)
        'rslt.PlaylistsList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        'Dim rslt = Await dcnt.Users("x1ge289").ListPlaylists(UsersSortEnum.recent, 30, 1)
        'rslt.PlaylistsList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        'Dim rslt = Await dcnt.Mine.Playlist(Nothing).Create("testplay", "new one", False)
        'FlowLayoutPanel1.Controls.Add(New PropertyGridEx(rslt))

        'Dim rslt = Await dcnt.Mine.Playlist("x6nwex").Metadata()
        'FlowLayoutPanel1.Controls.Add(New PropertyGridEx(rslt))

        'Dim rslt = Await dcnt.Mine.UploadLocal("C:\Users\PointNine\Downloads\dvezd-lb3fs.mkv", UploadTypes.FilePath, "final_5d4c61017573800013f9c0e8_849505", Nothing, ChannelsEnum.shortfilms, PrivacyEnum.Private)
        'FlowLayoutPanel1.Controls.Add(New PropertyGridEx(rslt))

        'Dim rslt = Await dcnt.Mine.ListVideos()
        'rslt.VideoList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        'Dim rslt = Await dcnt.Mine.SearchVideos("final_5d4c61017", SearchTypesEnum.Contains)
        'rslt.VideoList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))

        Dim rslt = Await dcnt.Users("").ListLikes
        rslt.VideoList.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGridEx(f)))




        'rslt.ForEach(Sub(f) FlowLayoutPanel1.Controls.Add(New PropertyGrid With {.SelectedObject = f, .Width = 250, .Height = 350}))

    End Sub

    Private Async Sub Button2_ClickAsync(sender As Object, e As EventArgs) Handles Button2.Click
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


        'Await dcnt.Users("user_id").SearchByUsername
        'Await dcnt.Users("user_id").SearchForUser
        'Await dcnt.Mine.SearchVideos("cat", SearchTypesEnum.Contains, New DailymotionSDK.MineClient.SearchOption With {.Channel = ChannelsEnum.animals})

    End Sub
End Class

Public Class PropertyGridEx
    Inherits PropertyGrid

    Public Sub New(obj As Object)
        MyBase.HelpVisible = False
        MyBase.Width = 250
        MyBase.Height = 350
        MyBase.SelectedObject = obj
    End Sub
End Class