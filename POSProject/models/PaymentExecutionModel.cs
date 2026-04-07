using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public class PaymentExecutionModel
    {
        public int MenyraPagesesId { get; set; }
        public string Pershkrimi { get; set; }
        public string Shkurtesa { get; set; }
        public string Tipi { get; set; }
        public decimal ShumaPaguar { get; set; }
        public decimal PaguarMe { get; set; }
        public decimal CashBack { get; set; }
        public string Valuta { get; set; }
        public decimal KursiKembimit { get; set; }
        public decimal ShumaNeValuteBaze { get; set; }
        public string ReferenceNr { get; set; }
        public string Statusi { get; set; }
        public string Koment { get; set; }
        public int PerdoruesiId { get; set; }

    }
}
