using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public class ReturnModel
    {
        public int Id { get; set; }
        public int ShitjaId { get; set; }
        public string NumriKthimit { get; set; }
        public decimal TotaliKthimit { get; set;}
        public string Arsyeja { get; set; } 
        public int PerdoruesiId { get; set; }
        public DateTime Created_At { get; set; }

        public int ShiftId { get; set; }

        public List<ReturnDetailModel> Details { get; set; } = new();
        public List<RefundPaymentModel> RefundPayments { get; set; } = new();
    }
}
