using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class LoyaltyTransactionModel
    {
        public int Id { get; set; }
        public int LoyaltyAccountId { get; set; }
        public int? ShitjaId { get; set; }
        public string Tipi { get; set; }
        public decimal Piket { get; set; }
        public decimal? VleraEuro { get; set; }
        public string Pershkrimi { get; set; }
        public string ReferenceNr { get; set; }
        public int PerdoruesiId { get; set; }
        public DateTime Created_At { get; set; }

    }
}
