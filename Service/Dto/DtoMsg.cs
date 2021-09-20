using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class DtoMsg
    {
        public DtoMsg()
        {
            isCheck = false;
        }
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 檢查判斷
        /// </summary>
        public bool isCheck { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string msg { get; set; }
    }
}
