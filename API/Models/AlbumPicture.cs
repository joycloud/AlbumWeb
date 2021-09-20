using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class AlbumPicture
    {
        public int Sn { get; set; }
        public int Idnum { get; set; }
        public string Sctrl { get; set; }
        public string Path { get; set; }
        public string Picturefile { get; set; }
        public string Remark { get; set; }
        public string Cruser { get; set; }
        public DateTime? Crdate { get; set; }
        public string Eduser { get; set; }
        public DateTime? Eddate { get; set; }
        public string Mtop { get; set; }
    }
}
