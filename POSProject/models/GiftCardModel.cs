using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class GiftCardModel
    {
        public int Id { get; set; }
        public string Kodi { get; set; }
        public string Barkodi { get; set; }
        public decimal ShumaFillestare { get; set; }
        public decimal BilanciAktual { get; set; }
        public string Statusi { get; set; }
        public int? ShitjaIdIssued { get; set; }
        public DateTime? Activated_At { get; set; }
        public DateTime? Expires_At { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Created_By { get; set; }


    }
}
