using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public class CashierShift
    {
        public int Id { get; set; }
        public int PerdoruesiId { get; set; }
        public DateTime Opened_At { get; set; }
        public DateTime? Closed_At { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal? ClosingBalanceExpected { get; set; }
        public decimal? ClosingBalanceActual { get; set; }
        public decimal? Difference { get; set; }
        public string Status { get; set; } = "OPEN";
        public string? Koment { get; set; }

    }
}
