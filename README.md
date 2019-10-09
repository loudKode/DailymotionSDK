## DailymotionSDK


`Download:`[https://github.com/loudKode/DailymotionSDK/releases](https://github.com/loudKode/DailymotionSDK/releases)<br>
`NuGet:`
[![NuGet](https://img.shields.io/nuget/v/DeQmaTech.DailymotionSDK.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/DeQmaTech.DailymotionSDK)<br>

**Features**
* Assemblies for .NET 4.5.2 and .NET Standard 2.0
* Just one external reference (Newtonsoft.Json)
* Easy installation using NuGet
* Upload/Download tracking support
* Proxy Support
* Upload/Download cancellation support


## List of functions
* CheckAccessToken
* RemoteUpload
* UserInfo
* ListFeaturesVideos
* ListVideos
* DeleteVideo
* VideoMetadata
* EditVideo
* APIrateLimits
* UploadLocalFile
* RevokeAccessToken
* GetVideoDirectLink
* EditVideoPrivacy
* AddTag
* AddToFavorites


## code simples
```vb.net
    Async Sub GetTokenByProvidingUserAndPass()
        Dim tkn = Await DailymotionSDK.GetToken.By_UsernameAndPassword("api key", "api secret", "user", "pass")
        DataGridView1.Rows.Add(tkn.access_token, tkn.refresh_token, tkn.expires_in, tkn.uid)
    End Sub
```
```vb.net
    Sub GetTokenByBrowser()
        Dim ScopeItems As New List(Of DailymotionSDK.GetToken.ScopesEnum) From {DailymotionSDK.GetToken.ScopesEnum.manage_videos, DailymotionSDK.GetToken.ScopesEnum.email}
        Dim tkn = DailymotionSDK.GetToken.By_AddressBar("client id", ScopeItems)
        Process.Start(tkn)
    End Sub
```
```vb.net
    Dim MyClient As DailymotionSDK.IClient = New DailymotionSDK.DClient("access token")
```
```vb.net
    Sub SetClient()
        Dim MyClient As DailymotionSDK.IClient = New DailymotionSDK.DClient("access token")
    End Sub
```
```vb.net
    Sub SetClientWithOptions()
        Dim Optians As New DailymotionSDK.ConnectionSettings With {.CloseConnection = True, .TimeOut = TimeSpan.FromMinutes(30), .Proxy = New DailymotionSDK.ProxyConfig With {.ProxyIP = "172.0.0.0", .ProxyPort = 80, .ProxyUsername = "myname", .ProxyPassword = "myPass", .SetProxy = True}}
        Dim MyClient As DailymotionSDK.IClient = New DailymotionSDK.DClient("access token", Optians)
    End Sub
```
```vb.net
    Async Sub ListMyVideos()
        Dim result = Await MyClient.ListVideos("my user id - tkn.uid", 1)
        For Each vid In result.VideoList
            DataGridView1.Rows.Add(vid.title, vid.tiny_url, vid.VideoPreview_480p_url, vid.SingleThumb_x240_url)
        Next
    End Sub
```
```vb.net
    Async Sub AddVideoTags()
        Dim result = Await MyClient.AddTag("video id", New List(Of String) From {"tag1", "tag2"})
    End Sub
```
```vb.net
    Async Sub AddVideoToFavoriteList()
        Dim result = Await MyClient.AddToFavorites("video id")
    End Sub
```
```vb.net
    Async Sub GetCurrentApiLimitsRate()
        Dim result = Await MyClient.APIrateLimits()
        DataGridView1.Rows.Add(result.UserID, result.limits.video_size, result.limits.video_duration)
    End Sub
```
```vb.net
    Async Sub DeleteAVideo()
        Dim result = Await MyClient.DeleteVideo("video id")
    End Sub
```
```vb.net
    Async Sub EditVideoInfo()
        Dim result = Await MyClient.EditVideo("video id", "new title", New List(Of String) From {"tag1", "tag2"}, ChannelsEnum.animals, PrivacyEnum.Public)
    End Sub
```
```vb.net
    Async Sub VideoDirectUrl()
        Dim result = Await MyClient.GetVideoDirectLink("video id")
        DataGridView1.Rows.Add(result.VideoResolutionUrls._720, result.VideoResolutionUrls.MP4_480p, result.VideoResolutionUrls.M3U8_380p)
    End Sub
```
```vb.net
    Async Sub Upload_Remote()
        Dim result = Await MyClient.RemoteUpload("https://www.tube.com/video.mp4", "my title", Nothing, Nothing, PrivacyEnum.Private)
    End Sub
```
```vb.net
    Async Sub Upload_Local_WithProgressTracking()
        Dim UploadCancellationToken As New Threading.CancellationTokenSource()
        Dim _ReportCls As New Progress(Of DailymotionSDK.ReportStatus)(Sub(ReportClass As DailymotionSDK.ReportStatus)
                                                                           Label1.Text = String.Format("{0}/{1}", (ReportClass.BytesTransferred), (ReportClass.TotalBytes))
                                                                           ProgressBar1.Value = CInt(ReportClass.ProgressPercentage)
                                                                           Label2.Text = CStr(ReportClass.TextStatus)
                                                                       End Sub)
        Dim RSLT = Await Clnt.UploadLocalFile("J:\DB\myvideo.mp4", UploadTypes.FilePath, "myvideo.mp4", Nothing, Nothing, PrivacyEnum.Public, _ReportCls, UploadCancellationToken.Token)
    End Sub
```
