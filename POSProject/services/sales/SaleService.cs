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

        public int SaveSale(SaleModel shitja, List<SaleDetailModel> detale, List<PaymentExecutionModel> payments)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        int shitjaId = _saleRepo.InsertSale(shitja, conn, tx);

                        foreach (var det in detale)
                        {
                            det.ShitjaId = shitjaId;
                            _saleRepo.InsertSaleDetail(det, conn, tx);

                            _productRepo.UpdateStock(det.ArtikulliId, det.Sasia, conn, tx);
                        }

                        foreach (var payment in payments)
                        {
                            payment.ShitjaId = shitjaId;

                            int ekzekutimiPagesesId = _paymentRepo.InsertPaymentExecution(payment, conn, tx);

                            if (!string.IsNullOrWhiteSpace(payment.Tipi) &&
                                payment.Tipi.Trim().Equals("GIFTCARD", StringComparison.OrdinalIgnoreCase))
                            {
                                if (string.IsNullOrWhiteSpace(payment.ReferenceNr))
                                    throw new Exception("Gift card payment nuk ka kod reference.");

                                decimal amountToDeduct = payment.ShumaPaguar;

                                _giftCardService.UseGiftCard(
                                    payment.ReferenceNr.Trim(),
                                    amountToDeduct,
                                    shitjaId,
                                    ekzekutimiPagesesId,
                                    payment.PerdoruesiId,
                                    conn,
                                    tx
                                );
                            }
                        }

                        tx.Commit();
                        return shitjaId;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
