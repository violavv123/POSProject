using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public string NrFatures { get; set; }
        public DateTime DataShitjes { get; set; }
        public decimal Totali { get; set; }
        public string Koment { get; set; }
        public int PerdoruesiId { get; set; }
        public int? SubjektiId { get; set; }
        public int ShiftId { get; set; }
        public decimal TotaliPaZbritje { get; set; }
        public decimal ZbritjaTotale { get; set; }
        public decimal TotaliFinal { get; set; }

    }
}
