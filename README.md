# DailymotionSDK
Dailymotion SDK for .NET


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


## code simple
**get token**
```vb.net
Dim tkn = Await DailymotionSDK.GetToken.By_UsernameAndPassword("apikey", "apisec", "User", "Pass")
```
**set client**
```vb.net
Dim Clnt As DailymotionSDK.IClient = New DailymotionSDK.DClient(tkn.access_token)
```
**set client with proxy**
```vb.net
Dim roxy = New DailymotionSDK.ProxyConfig With {.ProxyIP = "172.0.0.0", .ProxyPort = 80, .ProxyUsername = "myname", .ProxyPassword = "myPass", .SetProxy = true}
Dim Clnt As DailymotionSDK.IClient = New DailymotionSDK.DClient(tkn.access_token,roxy)
```
**list videos**
```vb.net
Dim RSLT = Await Clnt.ListVideos("user id", 1)
For Each vid In RSLT.VideoList
    DataGridView1.Rows.Add(vid.id, vid.title, vid.VideoPreview_240p_url, vid.VideoUrl, vid.DurationInSec, vid.MultiThumbUrl, vid.status, vid.published, String.Format("{0}/{1}", vid.width, vid.height))
Next
```
**upload local file with progress tracking**
```vb.net
Dim UploadCancellationToken As New Threading.CancellationTokenSource()
Dim prog_ReportCls As New Progress(Of DailymotionSDK.ReportStatus)(Sub(ReportClass As DailymotionSDK.ReportStatus)
                         Label1.Text = String.Format("{0}/{1}", (ReportClass.BytesTransferred), (ReportClass.TotalBytes))
                         ProgressBar1.Value = CInt(ReportClass.ProgressPercentage)
                         Label2.Text = CStr(ReportClass.TextStatus)
                         End Sub)
Dim RSLT = Await Clnt.UploadLocalFile("C:\myvideo.mp4", UploadTypes.FilePath, "filename_myvideo", Nothing, Nothing, PrivacyEnum.Public, prog_ReportCls, UploadCancellationToken.Token)
DataGridView1.Rows.Add(RSLT.name, RSLT.id, RSLT.dimension,(RSLT.duration), RSLT.acodec,(RSLT.size), RSLT.vcodec)
```
**upload local file (without progress tracking)**
```vb.net
Dim UploadCancellationToken As New Threading.CancellationTokenSource()
Dim RSLT = Await Clnt.UploadLocalFile("C:\myvideo.mp4", UploadTypes.FilePath, "filename_myvideo", Nothing, Nothing, PrivacyEnum.Public, Nothing, UploadCancellationToken.Token)
DataGridView1.Rows.Add(RSLT.name, RSLT.id, RSLT.dimension,(RSLT.duration), RSLT.acodec,(RSLT.size), RSLT.vcodec)
```
