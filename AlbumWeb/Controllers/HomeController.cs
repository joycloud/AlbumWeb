using AlbumWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Service.Tools;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Routing;
using Service.Dto;

namespace AlbumWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostEnvironment _hostingEnvironment;
        private readonly IConfiguration config;
        public HomeController(IHostEnvironment hostingEnvironment, IConfiguration config)
        {
            this.config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        /// <summary>
        /// index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region 會員登入
        /// <summary>
        /// Login Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 呼叫API檢查會員，並回傳資料
        /// </summary>
        /// <param name="apiUserInfo"></param>
        /// <returns></returns>
        private static async Task<DtoUserInfo> CheckUserLogin(DtoUserInfo apiUserInfo)
        {
            DtoUserInfo apiUserResult = new DtoUserInfo();
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient client = new HttpClient(handler))
                    {
                        #region 呼叫遠端 Web API
                        string FooUrl = $"https://localhost:44372/api/Common/CheckUser";

                        //string FooUrl = $"~/api/Common/CheckUser";

                        #region  設定相關網址內容
                        var fooFullUrl = $"{FooUrl}";

                        // Accept 用於宣告客戶端要求服務端回應的文件型態 (底下兩種方法皆可任選其一來使用)
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        // Content-Type 用於宣告遞送給對方的文件型態
                        //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                        var fooJSON = JsonConvert.SerializeObject(apiUserInfo);
                        HttpResponseMessage response = null;
                        using (var fooContent = new StringContent(fooJSON, Encoding.UTF8, "application/json"))
                        {
                            response = await client.PostAsync(fooFullUrl, fooContent);
                        }
                        #endregion
                        #endregion

                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode == true)
                            {
                                // 取得呼叫完成 API 後的回報內容
                                string strResult = await response.Content.ReadAsStringAsync();
                                //轉成obect
                                apiUserResult = JsonConvert.DeserializeObject<DtoUserInfo>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                            }
                            else
                            {
                                apiUserResult.remark = "呼叫API異常。";
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                apiUserResult.remark = "呼叫API異常。";
                //throw;
            }
            return apiUserResult;
        }
        /// <summary>
        /// 會員登入Post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(IFormCollection post)
        {
            string account = post["account"];
            string password = post["password"];

            //判斷帳號
            if (!String.IsNullOrEmpty(account) && !String.IsNullOrEmpty(password))
            {
                //檢查會員，並傳回會員Level
                DtoUserInfo apiUserInfo = new DtoUserInfo()
                {
                    account = account,
                    password = password
                };
                DtoUserInfo userResult = CheckUserLogin(apiUserInfo).Result;
                // Session預設有效時間是20分鐘
                HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(userResult));

                //登入成功
                if (string.IsNullOrEmpty(userResult.remark))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        /// <summary>
        /// 取Session
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Home/GetSession")]
        public DtoUserInfo GetSession()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            DtoUserInfo userInfo = new DtoUserInfo();
            //轉成物件
            if (!string.IsNullOrEmpty(userSession))
                userInfo = JsonConvert.DeserializeObject<DtoUserInfo>(userSession);
            return userInfo;
        }

        #endregion

        #region 新增相簿
        /// <summary>
        /// 新增相簿頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateAlbum()
        {
            return View();
        }

        /// <summary>
        /// 新增相簿，呼叫API
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DtoMsg> CreateAlbumApi(string albumName)
        {
            DtoMsg dtoMsg = new DtoMsg();
            try
            {
                // 建立相簿名稱
                string datename = DateTime.Now.ToString("yyyyMMdd") + "_";
                albumName = datename + albumName;

                //呼叫API判斷是否有重複相簿名稱
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient client = new HttpClient(handler))
                    {
                        #region 呼叫遠端 Web API
                        string FooUrl = $"https://localhost:44372/api/Common/CheckAlbumName";

                        #region  設定相關網址內容
                        var fooFullUrl = $"{FooUrl}";

                        // Accept 用於宣告客戶端要求服務端回應的文件型態 (底下兩種方法皆可任選其一來使用)
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var apiAlbumName = JsonConvert.SerializeObject(albumName);
                        HttpResponseMessage response = null;
                        using (var fooContent = new StringContent(apiAlbumName, Encoding.UTF8, "application/json"))
                        {
                            response = await client.PostAsync(fooFullUrl, fooContent);
                        }
                        #endregion

                        #region 呼叫後結果
                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode == true)
                            {
                                // 取得呼叫完成 API 後的回報內容
                                string strResult = await response.Content.ReadAsStringAsync();
                                //轉成obect
                                dtoMsg = JsonConvert.DeserializeObject<DtoMsg>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                            }
                            else
                            {
                                dtoMsg.isCheck = false;
                                dtoMsg.msg = "呼叫API異常。";
                            }
                        }
                        #endregion
                        #endregion

                        #region 產生資料夾
                        //判斷資料夾是否重複
                        if (dtoMsg.isCheck)
                        {
                            string albumPath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\album\\" + albumName;
                            if (Directory.Exists(albumPath))
                            {
                                dtoMsg.isCheck = false;
                                dtoMsg.msg = "本機資料夾重複!";
                            }
                            else
                            {
                                //建立資料夾
                                Directory.CreateDirectory(albumPath);
                                //大小圖路徑
                                string Albumname_big = albumPath + "//big";
                                string Albumname_smail = albumPath + "//smail";
                                // 建大小圖資料夾
                                Directory.CreateDirectory(Albumname_big);
                                Directory.CreateDirectory(Albumname_smail);

                                //寫入資料庫
                                FooUrl = $"https://localhost:44372/api/Common/CreateAlbum";
                                fooFullUrl = $"{FooUrl}";
                                DtoItem dtoItem = new DtoItem()
                                {
                                    name = albumName,
                                    path = albumPath
                                };

                                apiAlbumName = JsonConvert.SerializeObject(dtoItem);
                                response = null;
                                using (var fooContent = new StringContent(apiAlbumName, Encoding.UTF8, "application/json"))
                                {
                                    response = await client.PostAsync(fooFullUrl, fooContent);
                                }

                                if (response != null)
                                {
                                    if (response.IsSuccessStatusCode == true)
                                    {
                                        // 取得呼叫完成 API 後的回報內容
                                        string strResult = await response.Content.ReadAsStringAsync();
                                        //轉成obect
                                        dtoMsg = JsonConvert.DeserializeObject<DtoMsg>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                                    }
                                    else
                                    {
                                        dtoMsg.isCheck = false;
                                        dtoMsg.msg = "建立相簿呼叫API異常。";
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
                dtoMsg.isCheck = false;
                dtoMsg.msg = "呼叫建立相簿API異常。";
                //throw;
            }
            return dtoMsg;
        }

        /// <summary>
        /// 新增照片，呼叫API
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public async Task<DtoMsg> CreatePicApi(string albumName, int id)
        {
            DtoMsg dtoMsg = new DtoMsg();

            //try
            //{
            //照片資料
            var picFile = Request.Form.Files;
            //路徑資料整理
            string datename = DateTime.Now.ToString("yyyyMMdd") + "_";
            albumName = datename + albumName;
            string albumPath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\album\\" + albumName;
            string bigAlbum = albumPath + "\\big";
            string smailAlbum = albumPath + "\\smail";
            string bigPath = bigAlbum + "\\" + picFile[0].Name;
            string smailPath = smailAlbum + "\\" + picFile[0].Name;
            DtoAlbum dtoPostApi = new DtoAlbum()
            {
                id = id,
                albumName = albumName,
                bigPicPath = bigPath,
                picName = picFile[0].Name
            };

            //呼叫API判斷是否有重複相簿名稱
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    #region 呼叫遠端 Web API
                    string FooUrl = $"https://localhost:44372/api/Common/CreatePic";

                    #region  設定相關網址內容
                    var fooFullUrl = $"{FooUrl}";

                    // Accept 用於宣告客戶端要求服務端回應的文件型態 (底下兩種方法皆可任選其一來使用)
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var postApi = JsonConvert.SerializeObject(dtoPostApi);
                    HttpResponseMessage response = null;
                    using (var fooContent = new StringContent(postApi, Encoding.UTF8, "application/json"))
                    {
                        response = await client.PostAsync(fooFullUrl, fooContent);
                    }
                    #endregion
                    #endregion

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode == true)
                        {
                            // 取得呼叫完成 API 後的回報內容
                            string strResult = await response.Content.ReadAsStringAsync();
                            //轉成obect
                            dtoMsg = JsonConvert.DeserializeObject<DtoMsg>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                        }
                        else
                        {
                            dtoMsg.isCheck = false;
                            dtoMsg.msg = "呼叫建立相片API異常。";
                        }
                    }
                    //大圖丟入資料夾
                    using (var stream = new FileStream(bigPath, FileMode.Create))
                    {
                        await Request.Form.Files[0].CopyToAsync(stream);
                    }

                    //壓縮成小圖丟入資料夾
                    imageuse(bigPath, smailPath, picFile[0].Name);
                }
            }
            //}
            //catch (Exception)
            //{
            //    dtoMsg.isCheck = false;
            //    dtoMsg.msg = "呼叫建立相片API異常。";
            //    //throw;
            //}
            return dtoMsg;
        }

        /// <summary>
        /// 壓縮圖片
        /// </summary>
        /// <param name="bigPath"></param>
        /// <param name="smailPath"></param>
        /// <param name="picFile"></param>
        private void imageuse(string bigPath, string smailPath, string picFile)
        {
            // 不要宣告Bitmap，因為屬性Server.MapPath無法存外部
            Image img = Image.FromFile(bigPath);
            // 長寬
            int width = 0;
            int height = 0;

            if (img.Width < 400 && img.Height < 400)
            {
                width = img.Width;
                height = img.Height;
            }
            else
            {
                if (img.Width > img.Height)
                {
                    width = 400;
                    height = img.Height / (img.Width / width);
                }
                else if (img.Width < img.Height)
                {
                    height = 400;
                    width = img.Width / (img.Height / height);
                }
                else
                {
                    width = 300;
                    height = 300;
                }
            }

            Image imgNew = new Bitmap(width, height);
            // 宣告繪圖UI
            Graphics g = Graphics.FromImage(imgNew);
            // 壓縮
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height),
            new System.Drawing.Rectangle(0, 0, img.Width, img.Height),
            System.Drawing.GraphicsUnit.Pixel);

            // Image可存外部
            imgNew.Save(smailPath, ImageFormat.Jpeg);
        }

        #endregion

        /// <summary>
        /// 相簿檢視
        /// </summary>
        /// <returns></returns>
        public IActionResult AlbumView()
        {

            return View();
        }

        /// <summary>
        /// 照片清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PicView(string name)
        {
            return View();
        }
    }
}
