using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.subjects
{
    public interface ILoyaltyAccountsRepository
    {
        LoyaltyAccountsModel GetById(int id);
        LoyaltyAccountsModel GetBySubjektiId(int subjektiId);
        LoyaltyAccountsModel GetByCardNumber(string cardNr);
        List<LoyaltyAccountsModel> GetAll();
        int Insert(LoyaltyAccountsModel model, NpgsqlConnection conn, NpgsqlTransaction tx);
        void Update(LoyaltyAccountsModel model, NpgsqlConnection conn, NpgsqlTransaction tx);
        void UpdatePoints(int loyaltyAccountId, decimal piketAktuale, decimal piketFituara, decimal piketShfrytezuara, NpgsqlConnection conn, NpgsqlTransaction tx);
        void SetActiveStatus(int loyaltyAccountId, bool aktiv, NpgsqlConnection conn, NpgsqlTransaction tx);
    }
}
