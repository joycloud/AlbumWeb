using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class AppMenu
    {
        public int Id { get; set; }
        public int? SystemListId { get; set; }
        public string AppMenuName { get; set; }
        public string Path { get; set; }
        public bool? IsEnable { get; set; }
        public string Type { get; set; }

        public virtual SystemList SystemList { get; set; }
    }
}
