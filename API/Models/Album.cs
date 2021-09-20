using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class Album
    {
        public int Sn { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? Permission { get; set; }
        public string Path { get; set; }
        public string Download { get; set; }
        public string Sctrl { get; set; }
        public string Remark { get; set; }
        public string Cruser { get; set; }
        public DateTime? Crdate { get; set; }
        public string Eduser { get; set; }
        public DateTime? Eddate { get; set; }
    }
}
