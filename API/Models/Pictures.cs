using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class Pictures
    {
        public int Id { get; set; }
        public int AlbumsId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? EditUser { get; set; }
        public DateTime? EditDate { get; set; }
        public bool? IsEnable { get; set; }

    }
}
