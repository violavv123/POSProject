using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.repositories.payments
{
    public interface IPaymentMethodRepository
    {
        List<PaymentMethodModel> GetActiveMethods();
        DataTable GetAll();
        DataTable GetActiveCurrencies();
        bool ExistsByPershkrimi(string pershkrimi, int? excludeId = null);
        bool ExistsByShkurtesa(string shkurtesa, int? excludeId = null);
        bool ExistsByRendorja(int rendorja, int? excludeId = null);
        void Insert(PaymentMethodModel method);
        void Update(PaymentMethodModel method);
        void Deactivate(int id);
    }
}
