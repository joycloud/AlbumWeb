using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class BudgetBugdum
    {
        public string Bugda { get; set; }
        public int Total { get; set; }
        public Guid Puid { get; set; }
        public Guid Uid { get; set; }
    }
}
