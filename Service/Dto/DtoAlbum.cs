using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class DtoAlbum
    {
        /// <summary>
        /// 相簿id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 相簿名稱
        /// </summary>
        public string albumName { get; set; }
        /// <summary>
        /// 相片名稱
        /// </summary>
        public string picName { get; set; }
        /// <summary>
        /// 大圖路徑
        /// </summary>
        public string bigPicPath { get; set; }
        /// <summary>
        /// 小圖路徑
        /// </summary>
        public string smallPicPath { get; set; }
        /// <summary>
        /// href路徑
        /// </summary>
        public string hrefPath { get; set; }
    }
}
