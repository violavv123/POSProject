using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.repositories.returns;

namespace POSProject.services
{
    public class ReturnService : IReturnService
    {
        private readonly IReturnRepository _returnRepository;

        public ReturnService(IReturnRepository returnRepository)
        {
            _returnRepository = returnRepository;
        }
        public DataTable GetRefundMethods()
        {
            return _returnRepository.GetRefundMethods();
        }

        public DataRow GetSaleHeaderByInvoiceNumber(string nrFatures)
        {
            if (string.IsNullOrWhiteSpace(nrFatures))
            {
                throw new ArgumentException("Numri i faturës është i detyrueshëm.");
            }
            return _returnRepository.GetSaleHeaderByInvoiceNumber(nrFatures.Trim());
        }

        public DataTable GetSaleLinesForReturn(int shitjaId)
        {
            if(shitjaId <= 0)
            {
                throw new ArgumentException("Shitja nuk është valide.");
            }
            return _returnRepository.GetSaleLinesForReturn(shitjaId);
        }

        public int SaveReturn(ReturnModel model)
        {
            var shiftService = new ShiftService();
            var activeShift = shiftService.GetOpenShift();

            if (activeShift == null)
                throw new Exception("Nuk ka ndërrim aktiv. Kthimi nuk mund të ruhet.");

            if (activeShift.PerdoruesiId != Session.UserId)
                throw new Exception("Ndërrimi aktiv nuk i përket përdoruesit aktual.");

            model.ShiftId = activeShift.Id;

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.ShitjaId <= 0)
                throw new ArgumentNullException("Shitja është e pavlefshme.");

            if (model.Details == null || model.Details.Count == 0)
                throw new ArgumentException("Kthimi duhet të ketë së paku një artikull");

            if (model.RefundPayments == null || model.RefundPayments.Count == 0)
                throw new ArgumentException("Kthimi duhet të ketë një pagesë rimbursimi");

            using var conn = Db.GetConnection();
            conn.Open();

            using var tx = conn.BeginTransaction();
            try
            {
                int returnId = _returnRepository.InsertReturn(model, conn, tx);
                foreach(var detail in model.Details)
                {
                    if (detail.Sasia <= 0)
                        throw new Exception("Sasia e kthimit duhet të jetë më e madhe se zero.");

                    decimal soldQuantity = _returnRepository.GetSoldQuantity(detail.ShitjaDetaliId, conn, tx);
                    decimal alreadyReturnedQuantity = _returnRepository.GetAlreadyReturnedQuantity(detail.ShitjaDetaliId, conn, tx);

                    if (alreadyReturnedQuantity + detail.Sasia > soldQuantity)
                        throw new Exception("Sasia e kthyer nuk mund të jetë më e madhe se sasia e shitur");

                    _returnRepository.InsertReturnDetail(detail, returnId, conn, tx);

                    _returnRepository.UpdateStockForReturn(detail.ArtikulliId, detail.Sasia, conn, tx);
                }

                decimal totalRefundPayments = 0m;
                foreach (var payment in model.RefundPayments)
                {
                    if (payment.Shuma <= 0)
                        throw new Exception("Shuma e rimbursimit duhet të jetë më e madhe se zero.");

                    totalRefundPayments += payment.Shuma;
                    _returnRepository.InsertRefundPayment(payment, returnId, conn, tx);

                    string paymentType = _returnRepository.GetPaymentMethodType(payment.MenyraPagesesId, conn, tx);

                    if (paymentType.ToUpper() == "CASH")
                    {
                        _returnRepository.InsertCashMovement(
                            model.ShiftId,
                            "OUT",
                            payment.Shuma,
                            $"Refund për kthimin nr. {model.NumriKthimit}",
                            $"Kthim i shitjes ID {model.ShitjaId}",
                            model.PerdoruesiId,
                            conn,
                            tx
                        );
                    }
                }

                if (totalRefundPayments != model.TotaliKthimit)
                    throw new Exception("Totali i rimbursimit nuk përputhet me totalin e kthimit.");

                tx.Commit();
                return returnId;
            }catch(Exception ex)
            {
                tx.Rollback();
                throw new Exception("Nuk arriti të ruhet kthimi" + ex.Message);
            }
        }
    }
}
