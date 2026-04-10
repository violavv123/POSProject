using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public string Pershkrimi { get; set; }
        public string Shkurtesa { get; set; }
        public string Tipi { get; set; }
        public string ValutaDefault { get; set; }
        public bool KerkonReference { get; set; }
        public bool Aktiv { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Rendorja { get; set; }

    }
}
