using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class POSCheckoutRequestModel
    {
        public List<CartItemModel> Items { get; set; } = new();
        public decimal TotaliPaZbritje { get; set; }
        public decimal ZbritjaTotale { get; set; }
        public decimal TotaliFinal { get; set; }
        public decimal PaguarPos { get; set; }
        public string Koment { get; set; }
        public int PerdoruesiId { get; set; }
        public int ShiftId { get; set; }
        public int? SubjektiId { get; set; }

    }
}
