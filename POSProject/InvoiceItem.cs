using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public class InvoiceItem
    {
        public int ArtikulliId { get; set; }
        public string Barkodi { get; set; } = "";
        public string Produkti { get; set; } = "";
        public decimal Sasia { get; set; }
        public decimal Cmimi { get; set; }
        public decimal Vlera { get; set; }
        public decimal Zbritja { get; set; }
        public decimal CmimiFinal { get; set; }
    }
}
