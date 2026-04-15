using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class LoyaltyAccountsModel
    {
        public int Id { get; set; }
        public int SubjektiId { get; set; }
        public string LoyaltyCardNr { get; set; }
        public decimal PiketAktuale { get; set; }
        public decimal PiketTotaleFituara { get; set; }
        public decimal PiketTotaleShfrytezuara { get; set; }
        public bool Aktiv { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
