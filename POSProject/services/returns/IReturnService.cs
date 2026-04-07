using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.services
{
    public interface IReturnService
    {
        DataTable GetRefundMethods();
        DataRow GetSaleHeaderByInvoiceNumber(string nrFatures);
        DataTable GetSaleLinesForReturn(int shitjaId);
        int SaveReturn(ReturnModel model);
    }
}
