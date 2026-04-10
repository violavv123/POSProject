using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class CartSummaryModel
    {
        public decimal TotaliPaZbritje { get; set; }
        public decimal ZbritjaTotale { get; set; }
        public decimal TotaliFinal { get; set; }
        public decimal Paguar { get; set; }
        public decimal Kusuri { get; set; }
        public decimal InvoiceDiscountAmount { get; set; }

    }
}
