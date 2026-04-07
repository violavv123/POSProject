using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POSProject.repositories.returns
{
    public interface IReturnRepository
    {
        DataTable GetRefundMethods();
        DataRow GetSaleHeaderByInvoiceNumber(string nrFatures);
        DataTable GetSaleLinesForReturn(int shitjaId);
        int InsertReturn(ReturnModel model, NpgsqlConnection conn, NpgsqlTransaction tx);
        void InsertReturnDetail(ReturnDetailModel detail, int returnId, NpgsqlConnection conn, NpgsqlTransaction tx);
        void UpdateStockForReturn(int artikulliId, decimal sasia, NpgsqlConnection conn, NpgsqlTransaction tx);
        void InsertRefundPayment(RefundPaymentModel payment, int returnId, NpgsqlConnection conn, NpgsqlTransaction tx);
        decimal GetSoldQuantity(int shitjaDetaliId, NpgsqlConnection conn, NpgsqlTransaction tx);
        decimal GetAlreadyReturnedQuantity(int shitjaDetaliId, NpgsqlConnection conn, NpgsqlTransaction tx);
        string GetPaymentMethodType(int menyraPagesesId, NpgsqlConnection conn, NpgsqlTransaction tx);
        void InsertCashMovement(int shiftId, string tipi, decimal shuma, string arsyeja, string koment, int perdoruesiId, NpgsqlConnection conn, NpgsqlTransaction tx);
    }
}
