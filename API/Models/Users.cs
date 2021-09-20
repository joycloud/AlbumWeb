using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public int? SystemListId { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Lev { get; set; }
        public bool IsEnable { get; set; }
        public string Third { get; set; }
        public string ThirdId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public DateTime? LoginDate { get; set; }
    }
}
