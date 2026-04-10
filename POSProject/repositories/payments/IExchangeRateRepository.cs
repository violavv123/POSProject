using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.repositories.payments
{
    public interface IExchangeRateRepository
    {
        decimal GetLatestRate(string valuta);
    }
}
