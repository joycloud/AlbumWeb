using System;
using System.Collections.Generic;

#nullable disable

namespace DataSource.Models
{
    public partial class BudgetItem
    {
        public string Items { get; set; }
        public string Remark { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int? Total { get; set; }
        public Guid Puid { get; set; }
        public Guid Uid { get; set; }
        public string Cruser { get; set; }
        public DateTime? Crdate { get; set; }
        public string Eduser { get; set; }
        public DateTime? Eddate { get; set; }
        public string Sctrl { get; set; }
    }
}
