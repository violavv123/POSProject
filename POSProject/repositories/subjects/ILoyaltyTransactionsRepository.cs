using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.subjects
{
    public interface ILoyaltyTransactionsRepository
    {
        void Insert(LoyaltyTransactionModel model, NpgsqlConnection conn, NpgsqlTransaction tx);
        List<LoyaltyTransactionModel> GetByAccountId(int loyaltyAccountId);
        List<LoyaltyTransactionModel> GetBySaleId(int shitjaId);
        LoyaltyTransactionModel GetById(int id);
    }
}
