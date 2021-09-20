using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class SystemList
    {
        public SystemList()
        {
            AppMenus = new HashSet<AppMenu>();
        }

        public int Id { get; set; }
        public string SystemName { get; set; }
        public bool? IsEnable { get; set; }

        public virtual ICollection<AppMenu> AppMenus { get; set; }
    }
}
