using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class DtoUserInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        public int lev { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string remark { get; set; }
    }
}
