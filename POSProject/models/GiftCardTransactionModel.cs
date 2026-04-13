using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class GiftCardTransactionModel
    {
        public int Id { get; set; }
        public int GiftCardId { get; set; }
        public string Type { get; set; }
        public decimal Shuma { get; set; }
        public int? ShitjaId { get; set; }
        public int? EkzekutimiPagesesId { get; set; }
        public string Koment { get; set; }
        public DateTime Created_At { get; set; }
        public int? Created_By { get; set; }

    }
}
