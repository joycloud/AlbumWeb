using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class BudgetStep
    {
        public string Bno { get; set; }
        public string Month { get; set; }
        public int? Total { get; set; }
        public Guid? Uid { get; set; }
        public string Dept { get; set; }
        public string Sctrl { get; set; }
        public int? Lev { get; set; }
    }
}
