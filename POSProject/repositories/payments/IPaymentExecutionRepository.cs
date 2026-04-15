using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POSProject.repositories.payments
{
    public interface IPaymentExecutionRepository
    {
        int InsertPaymentExecution(PaymentExecutionModel payment, NpgsqlConnection conn, NpgsqlTransaction tx);
    }
}
