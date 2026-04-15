using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.services.products
{
    public interface IGiftCardService
    {
        GiftCardModel GetByCode(string kodi);
        GiftCardModel ValidateForRedeem(string kodi, decimal shumaKerkuar);
        int IssueGiftCard(decimal shuma, int shitjaId, int userId, NpgsqlConnection conn, NpgsqlTransaction tx, string kodi = null);
        void RedeemGiftCard(string kodi, decimal shuma, int shitjaId, int ekzekutimiPagesesId, int userId, NpgsqlConnection conn, NpgsqlTransaction tx);
        string GenerateUniqueCode();
        int CreateGiftCard(GiftCardModel model);
        List<GiftCardTransactionModel> GetTransactionsByCode(string kodi);
        GiftCardModel IssueGiftCardFromSale(decimal shuma, int shitjaId, int userId);
        List<GiftCardModel> GetAll(bool onlyAvailable = false);
        void UseGiftCard(string code, decimal amount, int shitjaId, int ekzekutimiPagesesId, int userId, NpgsqlConnection conn, NpgsqlTransaction tx);
    }
}
