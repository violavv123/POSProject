using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.repositories.sales
{
    public interface ISalesReportRepository
    {
        DataTable GetSales(SalesFilterModel filter);
        DataTable GetTopSoldProducts(SalesFilterModel filter);
    }
}
