using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class DtoAppMenu
    {
        public DtoAppMenu()
        {
            list = new List<DtoItem>();
        }

        /// <summary>
        /// Logo
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// Logo路徑
        /// </summary>
        public string logoPath { get; set; }
        /// <summary>
        /// Menu清單
        /// </summary>
        public List<DtoItem> list { get; set; }
        /// <summary>
        /// 登入狀態、資訊
        /// </summary>
        public DtoItem logInType { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string remark { get; set; }
    }
}
