using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Barkodi { get; set; }

        public int KategoriaId { get; set; }
        public decimal CmimiShitjes { get; set; }
        public decimal SasiaNeStok { get; set; }
        public bool Aktiv { get; set; }
        public decimal MinimumStok { get; set; }

    }
}
