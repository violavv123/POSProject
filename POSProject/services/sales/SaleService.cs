using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.payments;
using POSProject.repositories.products;
using POSProject.repositories.sales;
using POSProject.services.products;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace POSProject.services.sales
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepo;
        private readonly IProductRepository _productRepo;
        private readonly IPaymentExecutionRepository _paymentRepo;
        private readonly GiftCardService _giftCardService;

        public SaleService(ISaleRepository saleRepo, IProductRepository productRepo, IPaymentExecutionRepository paymentRepo)
        {
            _saleRepo = saleRepo;
            _productRepo = productRepo;
            _paymentRepo = paymentRepo;
            _giftCardService = new GiftCardService(new GiftCardRepository());
        }

        public int SaveSale(SaleModel sale, List<SaleDetailModel> detale, List<PaymentExecutionModel> payments)
        {
            using var connection = Db.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                int newShitjaId = _saleRepo.InsertSale(sale, connection, transaction);

                foreach (var item in detale)
                {
                    decimal sasiaNeStokAktuale = _productRepo.GetStockById(
                        item.ArtikulliId
                        );

                    if (sasiaNeStokAktuale <= 0)
                    {
                        throw new Exception($"Produkti me Id: {item.ArtikulliId} nuk u gjet në databazë.");
                    }

                    if (item.Sasia > sasiaNeStokAktuale)
                    {
                        throw new Exception(
                            $"Nuk ka stok të mjaftueshëm për produktin me Id: {item.ArtikulliId}. " +
                            $"Në stok: {sasiaNeStokAktuale}, kërkohet: {item.Sasia}");
                    }

                    item.ShitjaId = newShitjaId;
                    _saleRepo.InsertSaleDetail(item, connection, transaction);
                    _productRepo.UpdateStock(item.ArtikulliId, item.Sasia, connection, transaction);
                }

                foreach (var payment in payments)
                {
                    payment.ShitjaId = newShitjaId;
                    _paymentRepo.InsertPaymentExecution(payment, connection, transaction);
                    bool isGiftCard = !string.IsNullOrWhiteSpace(payment.Tipi) &&
                      payment.Tipi.Trim().Equals("GIFTCARD", StringComparison.OrdinalIgnoreCase);

                    if (isGiftCard)
                    {
                        if (string.IsNullOrWhiteSpace(payment.ReferenceNr))
                            throw new Exception("Kodi i gift card mungon gjatë ruajtjes.");

                        _giftCardService.RedeemGiftCard(
                            payment.ReferenceNr.Trim(),
                            payment.ShumaPaguar,
                            newShitjaId,
                            payment.Id,
                            payment.PerdoruesiId,
                            connection,
                            transaction
                        );
                    }
                }

                transaction.Commit();
                return newShitjaId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
