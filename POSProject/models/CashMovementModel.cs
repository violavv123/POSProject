using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class CashMovementModel
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public string Tipi { get; set; }
        public decimal Shuma { get; set; }
        public string Arsyeja { get; set; }
        public string Koment { get; set; }
        public int PerdoruesiId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
