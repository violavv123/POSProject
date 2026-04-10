using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.services.payments
{
    public interface IPaymentMethodService
    {
        DataTable GetMethods();
        DataTable GetCurrencies();
        ServiceResult Save(PaymentMethodModel method);
        ServiceResult Update(PaymentMethodModel method);
        ServiceResult Deactivate(int id, string pershkrimi);
    }
}
