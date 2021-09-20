using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class Albums
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lev { get; set; }
        public string Password { get; set; }
        public int Permission { get; set; }
        public string Path { get; set; }
        public bool? IsEnable { get; set; }
        public string Remark { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? EditUser { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
