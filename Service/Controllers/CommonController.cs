using DataSource.Models;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class CommonController : Controller
    {
        private MISContext db = new MISContext();

        #region 取Menu、Logo資料
        /// <summary>
        /// 取Menu、Logo資料
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="userAccount"></param>
        [HttpGet]
        [Route("api/Common/GetAppMenuList")]
        [Obsolete]
        //[ValidateAntiForgeryToken] //避免XSS、CSRF攻擊
        public DtoAppMenu GetAppMenuList(string systemName, string userAccount)
        {
            //取相簿Menu資料
            List<AppMenu> appMenu = (from c in db.SystemLists
                                     join o in db.AppMenu on c.Id equals o.SystemListId
                                     where c.SystemName == systemName && c.IsEnable == true && o.IsEnable == true
                                     select o).ToList();

            // #1# 搜尋條件為 = True AND 產品名稱 AND 生產工廠
            var menuWhere = PredicateBuilder.True<AppMenu>();

            //未登入
            if (userAccount == "Login")
            {
                menuWhere.And(o => o.AppMenuName.Contains("Album"));
            }
            //已登入
            else
            {

            }

            //IQueryable<AppMenu> products = db.AppMenu;
            //var aa = products.AsExpandable().Where(menuWhere).ToList();
            //取logo資料
            AppMenu logoItem = appMenu.Where(x => x.Type == "logo").First();
            //取logIn資料
            AppMenu logIn = appMenu.Where(x => x.Type == "logIn").First();
            //Home路徑
            string pathHome = "/Home/";

            DtoAppMenu dtoAppMenu = new DtoAppMenu
            {
                logo = logoItem.AppMenuName,
                logoPath = pathHome + logoItem.Path,
                logInType = new DtoItem
                {
                    name = logIn.AppMenuName,
                    path = pathHome + logIn.Path
                },
            };

            //整理Menu List
            var menuList = appMenu.Where(x => x.Type == "list");
            foreach (AppMenu item in menuList)
            {
                DtoItem dtoItem = new DtoItem
                {
                    name = item.AppMenuName,
                    path = pathHome + item.Path,
                };
                dtoAppMenu.list.Add(dtoItem);
            }
            return dtoAppMenu;
        }
        #endregion

        #region 取Index資料
        /// <summary>
        /// 取Index資料
        /// </summary>
        /// <param name="systemName"></param>
        [HttpGet]
        [Route("api/Common/GetIndexData")]
        //[ValidateAntiForgeryToken] //避免XSS、CSRF攻擊
        public DtoItem GetIndexData(string systemName)
        {
            //取Index資料
            AppMenu indexData = (from c in db.SystemLists
                                 join o in db.AppMenu on c.Id equals o.SystemListId
                                 where c.SystemName == systemName && c.IsEnable == true && o.IsEnable == true
                                 && o.Type == "logo"
                                 select o).FirstOrDefault();

            DtoItem dtoItem = new DtoItem()
            {
                name = indexData.AppMenuName,
                path = indexData.Path,
                remark = "photo studio"
            };
            return dtoItem;
        }
        #endregion

        #region 檢查帳號
        /// <summary>
        /// 檢查帳號
        /// </summary>
        /// <param name="userInfo"></param>
        [HttpPost]
        [Route("api/Common/CheckUser")]
        //[ValidateAntiForgeryToken] //避免XSS、CSRF攻擊
        public DtoUserInfo CheckUser([FromBody] DtoUserInfo userInfo)
        {
            string password = SHA256.Encryption(userInfo.password);     //SHA256加密
            //檢查帳號、密碼
            Users userData = (from c in db.SystemLists
                              join o in db.Users on c.Id equals o.SystemListId
                              where c.SystemName == "AlbumWeb" && c.IsEnable == true &&
                              o.Account == userInfo.account &&
                              o.Password == password &&
                              o.IsEnable == true
                              select o).FirstOrDefault();

            DtoUserInfo UserRrequest = new DtoUserInfo();
            if (userData == null)
            {
                UserRrequest.remark = "帳號密碼錯誤!";
            }
            else
            {
                UserRrequest.id = userData.Id;
                UserRrequest.account = userInfo.account;
                UserRrequest.lev = userData.Lev;
            };
            return UserRrequest;
        }
        #endregion

        #region 檢查相簿是否重複
        /// <summary>
        /// 檢查相簿是否重複
        /// </summary>
        /// <param name="albumName"></param>
        [HttpPost]
        [Route("api/Common/CheckAlbumName")]
        //[ValidateAntiForgeryToken] //避免XSS、CSRF攻擊
        public DtoMsg CheckAlbumName([FromBody] string apiAlbumName)
        {
            DtoMsg dtoMsg = new DtoMsg();
            //取相簿
            Albums getAlbum = db.Albums.Where(x => x.Name == apiAlbumName).FirstOrDefault();

            //有重複
            if (getAlbum != null)
            {
                dtoMsg.msg = "相簿名稱重複!";
            }
            else
            {
                dtoMsg.isCheck = true;
            }
            return dtoMsg;
        }
        #endregion

        #region 建立相簿
        /// <summary>
        /// 建立相簿
        /// </summary>
        /// <param name="albumName"></param>
        [HttpPost]
        [Route("api/Common/CreateAlbum")]
        //[ValidateAntiForgeryToken] //避免XSS、CSRF攻擊
        public DtoMsg CreateAlbum([FromBody] DtoItem dtoItem)
        {
            DtoMsg dtoMsg = new DtoMsg();
            Albums albums = new Albums();
            albums.Name = dtoItem.name;
            albums.Path = dtoItem.path;
            albums.IsEnable = true;
            albums.CreateDate = DateTime.Now;

            // Create Album into Datatable
            try
            {
                using (MISContext db = new MISContext())
                {
                    db.Albums.Add(albums);
                    db.SaveChanges();
                    dtoMsg.isCheck = true;
                    //取出id
                    dtoMsg.id = db.Albums.Where(o => o.Name == dtoItem.name).Select(o => o.Id).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                dtoMsg.isCheck = false;
                dtoMsg.msg = "儲存相簿" + dtoItem.name + "失敗!";
            }

            return dtoMsg;
        }
        #endregion

        #region 建立相片
        /// <summary>
        /// 建立相片
        /// </summary>
        /// <param name="albumName"></param>
        [HttpPost]
        [Route("api/Common/CreatePic")]
        //[ValidateAntiForgeryToken] //避免XSS、CSRF攻擊
        public DtoMsg CreatePic([FromBody] DtoAlbum dtpAlbum)
        {
            DtoMsg dtoMsg = new DtoMsg();

            //取相簿
            //Pictures getPictures = db.Pictures.Where(x => x.AlbumsId == dtpAlbum.id).OrderByDescending(o => o.Id).FirstOrDefault();

            Pictures picture = new Pictures();
            picture.AlbumsId = dtpAlbum.id;
            picture.Path = dtpAlbum.bigPicPath;
            picture.Name = dtpAlbum.picName;
            picture.CreateDate = DateTime.Now;
            picture.IsEnable = true;

            //try
            //{
            using (MISContext db = new MISContext())
            {
                db.Pictures.Add(picture);
                db.SaveChanges();
                dtoMsg.isCheck = true;
                //取出id
                dtoMsg.id = dtpAlbum.id;
            }
            //}
            //catch (Exception)
            //{
            //    dtoMsg.isCheck = false;
            //    dtoMsg.msg = "儲存相片" + dtpAlbum.picName + "失敗!";
            //}
            return dtoMsg;
        }
        #endregion

        #region 取相簿清單
        /// <summary>
        /// 取相簿清單
        /// </summary>
        /// <param name="albumName"></param>
        [HttpGet]
        [Route("/api/Common/GetAlbumList")]
        //[ValidateAntiForgeryToken] //避免XSS、CSRF攻擊
        public DtoAlbum GetAlbumList(string userAccount)
        {
            DtoAlbum dtoAlbum = new DtoAlbum();



            //取相簿
            //List<DtoAlbum> getPictures = db.Albums.Where(x => x.AlbumsId == dtpAlbum.id).OrderByDescending(o => o.Id).FirstOrDefault();

            //Pictures picture = new Pictures();
            //picture.AlbumsId = dtpAlbum.id;
            //picture.Path = dtpAlbum.bigPicPath;
            //picture.Name = dtpAlbum.picName;
            //picture.CreateDate = DateTime.Now;
            //picture.IsEnable = true;

            ////try
            ////{
            //using (MISContext db = new MISContext())
            //{
            //    db.Pictures.Add(picture);
            //    db.SaveChanges();
            //    dtoMsg.isCheck = true;
            //    //取出id
            //    dtoMsg.id = dtpAlbum.id;
            //}
            //}
            //catch (Exception)
            //{
            //    dtoMsg.isCheck = false;
            //    dtoMsg.msg = "儲存相片" + dtpAlbum.picName + "失敗!";
            //}
            return dtoAlbum;
        }
        #endregion
    }
}
