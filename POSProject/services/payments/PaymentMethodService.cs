using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.payments;

namespace POSProject.services.payments
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _methodRepo;

        public PaymentMethodService(IPaymentMethodRepository methodRepo)
        {
            _methodRepo = methodRepo;
        }

        public DataTable GetMethods() => _methodRepo.GetAll();

        public DataTable GetCurrencies() => _methodRepo.GetActiveCurrencies();

        public ServiceResult Save(PaymentMethodModel method)
        {
            var validation = Validate(method, null);
            if (!validation.Success)
                return validation;

            _methodRepo.Insert(method);

            NotificationService.Create(
                "PAYMENT_METHOD_ADDED",
                "Info",
                "Mënyrë pagese e re",
                method.Pershkrimi,
                "MenyratPageses",
                null,
                Session.UserId
            );

            return ServiceResult.Ok("Mënyra e pagesës u ruajt me sukses.");
        }

        public ServiceResult Update(PaymentMethodModel method)
        {
            if (method.Id <= 0)
                return ServiceResult.Fail("Id e mënyrës së pagesës nuk është valide.");

            var validation = Validate(method, method.Id);
            if (!validation.Success)
                return validation;

            _methodRepo.Update(method);

            NotificationService.Create(
                "PAYMENT_METHOD_UPDATED",
                "Info",
                "Mënyrë pagese e përditësuar",
                method.Pershkrimi,
                "MenyratPageses",
                method.Id,
                Session.UserId
            );

            return ServiceResult.Ok("Mënyra e pagesës u përditësua me sukses.");
        }

        public ServiceResult Deactivate(int id, string pershkrimi)
        {
            if (id <= 0)
                return ServiceResult.Fail("Zgjidh një mënyrë pagese valide.");

            _methodRepo.Deactivate(id);

            NotificationService.Create(
                "PAYMENT_METHOD_DEACTIVATED",
                "Info",
                "Mënyrë pagese e ç'aktivizuar",
                pershkrimi,
                "MenyratPageses",
                id,
                Session.UserId
            );

            return ServiceResult.Ok("Mënyra e pagesës u ç'aktivizua me sukses.");
        }

        private ServiceResult Validate(PaymentMethodModel method, int? excludeId)
        {
            if (method.Rendorja <= 0)
                return ServiceResult.Fail("Rendorja duhet të jetë numër pozitiv.");

            if (string.IsNullOrWhiteSpace(method.Pershkrimi))
                return ServiceResult.Fail("Përshkrimi është i detyrueshëm.");

            if (string.IsNullOrWhiteSpace(method.Shkurtesa))
                return ServiceResult.Fail("Shkurtesa është e detyrueshme.");

            if (string.IsNullOrWhiteSpace(method.Tipi))
                return ServiceResult.Fail("Zgjidh tipin.");

            if (string.IsNullOrWhiteSpace(method.ValutaDefault))
                return ServiceResult.Fail("Zgjidh valutën default.");

            if (_methodRepo.ExistsByPershkrimi(method.Pershkrimi, excludeId))
                return ServiceResult.Fail("Ekziston një mënyrë pagese me të njëjtin përshkrim.");

            if (_methodRepo.ExistsByShkurtesa(method.Shkurtesa, excludeId))
                return ServiceResult.Fail("Ekziston tashmë një mënyrë pagese me të njëjtën shkurtesë.");

            if (_methodRepo.ExistsByRendorja(method.Rendorja, excludeId))
                return ServiceResult.Fail("Ekziston tashmë një mënyrë pagese me të njëjtin numër rendor.");

            return ServiceResult.Ok();
        }
    }
}
