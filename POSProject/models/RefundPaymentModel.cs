using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public class RefundPaymentModel
    {
        public int Id { get; set; }
        public int ReturnId { get; set; }
        public int MenyraPagesesId { get; set; }
        public decimal Shuma { get; set; }
        public string ReferenceNr { get; set; }
        public DateTime Created_At { get; set; }

        public int PerdoruesiId { get; set; }
    }
}
