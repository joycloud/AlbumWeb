using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class DtoItem
    {
        /// <summary>
        /// Name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 路徑
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 登入狀態
        /// </summary>
        public string loginType { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string remark { get; set; }
    }
}
