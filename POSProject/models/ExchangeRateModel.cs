using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class ExchangeRateModel
    {
        public int Id { get; set; }
        public string Valuta { get; set; }
        public decimal KursiNeEuro { get; set; }
        public bool Aktiv { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
