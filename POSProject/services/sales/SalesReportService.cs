using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.sales;

namespace POSProject.services.sales
{
    public class SalesReportService
    {
        private readonly ISalesReportRepository _salesReportRepo;

        public SalesReportService(ISalesReportRepository salesReportRepo)
        {
            _salesReportRepo = salesReportRepo;
        }

        public DataTable GetSales(SalesFilterModel filter)
        {
            return _salesReportRepo.GetSales(filter);
        }

        public DataTable GetTopSoldProducts(SalesFilterModel filter)
        {
            return _salesReportRepo.GetTopSoldProducts(filter);
        }
    }
}
