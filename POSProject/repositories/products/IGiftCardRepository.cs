using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.products
{
    public interface IGiftCardRepository
    {
        GiftCardModel GetByCode(string kodi);
        GiftCardModel GetByCode(string kodi, NpgsqlConnection conn, NpgsqlTransaction tx);
        int InsertGiftCard(GiftCardModel card, NpgsqlConnection conn, NpgsqlTransaction tx);
        void UpdateBalance(int giftCardId, decimal newBalance, string newStatus, NpgsqlConnection conn, NpgsqlTransaction tx);
        int InsertTransaction(GiftCardTransactionModel transaction, NpgsqlConnection conn, NpgsqlTransaction tx);
        bool ExistsByCode(string kodi);
        string GenerateUniqueCode();

        List<GiftCardTransactionModel> GetTransactionsByGiftCardId(int giftCardId);

    }
}
