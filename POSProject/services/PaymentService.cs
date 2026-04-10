using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.repositories.payments;
using POSProject.models;

namespace POSProject.services
{
    public class PaymentService
    {
        private readonly IPaymentMethodRepository _methodRepo;
        private readonly IExchangeRateRepository _exchangeRateRepo;

        public PaymentService(IPaymentMethodRepository methodRepo, IExchangeRateRepository exchangeRateRepo)
        {
            _methodRepo = methodRepo;
            _exchangeRateRepo = exchangeRateRepo;
        }
        public List<PaymentMethodModel> GetActiveMethods() => _methodRepo.GetActiveMethods();
        public decimal GetExchangeRate(string valuta) => _exchangeRateRepo.GetLatestRate(valuta);
    }
}
