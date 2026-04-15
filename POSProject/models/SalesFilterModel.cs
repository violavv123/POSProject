using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class SalesFilterModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Cashier { get; set; }

    }
}
