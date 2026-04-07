using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public class ReturnDetailModel
    {
        public int Id { get; set; }
        public int ReturnId { get; set; }
        public int ShitjaDetaliId { get; set; }
        public int ArtikulliId { get; set; }
        public decimal Sasia { get; set; }
        public decimal Cmimi { get; set; }
        public decimal Vlera { get; set; }

    }
}
