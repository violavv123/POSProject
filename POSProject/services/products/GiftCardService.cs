using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.repositories.products;
using POSProject.models;
using Npgsql;

namespace POSProject.services.products
{
    public class GiftCardService : IGiftCardService
    {
        private readonly IGiftCardRepository _giftCardRepo;

        public GiftCardService(GiftCardRepository giftCardRepo)
        {
            _giftCardRepo = giftCardRepo;
        }
        public GiftCardModel GetByCode(string kodi)
        {
            if (string.IsNullOrWhiteSpace(kodi))
                return null;
            return _giftCardRepo.GetByCode(kodi.Trim());
        }

        public GiftCardModel ValidateForRedeem(string kodi, decimal shumaKerkuar)
        {
            if (string.IsNullOrWhiteSpace(kodi))
                throw new Exception("Kodi i gift cardit mungon.");

            var card = _giftCardRepo.GetByCode(kodi.Trim());
            if (card == null)
                throw new Exception("Gift card nuk u gjet.");

            if (!string.Equals(card.Statusi, "Aktiv", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Gift card nuk është aktiv.");

            if (card.Expires_At.HasValue && card.Expires_At.Value < DateTime.Now)
                throw new Exception("Gift card ka skaduar.");

            if (card.BilanciAktual <= 0)
                throw new Exception("Gift card nuk ka bilanc.");

            if (shumaKerkuar > card.BilanciAktual)
                throw new Exception($"Bilanci në gift card është i pamjaftueshëm. Bilanci aktual: {card.BilanciAktual:0.00} €");

            return card;
        }

        public int CreateGiftCard(GiftCardModel model)
        {
            if (model == null)
                throw new Exception("Modeli është null.");

            if (string.IsNullOrWhiteSpace(model.Kodi))
                throw new Exception("Kodi i gift card është i detyrueshëm.");

            if (model.ShumaFillestare <= 0)
                throw new Exception("Shuma fillestare duhet të jetë më e madhe se zero.");

            if (_giftCardRepo.ExistsByCode(model.Kodi))
                throw new Exception("Ekziston një gift card me këtë kod.");

            model.BilanciAktual = model.ShumaFillestare;

            if (string.IsNullOrWhiteSpace(model.Statusi))
                model.Statusi = "ACTIVE";

            if (model.Created_At == default)
                model.Created_At = DateTime.Now;

            if (model.Activated_At == null)
                model.Activated_At = DateTime.Now;

            using var conn = Db.GetConnection();
            conn.Open();

            using var tx = conn.BeginTransaction();

            try
            {
                int giftCardId = _giftCardRepo.InsertGiftCard(model, conn, tx);

                _giftCardRepo.InsertTransaction(new GiftCardTransactionModel
                {
                    GiftCardId = giftCardId,
                    Type = "ISSUE",
                    Shuma = model.ShumaFillestare,
                    ShitjaId = model.ShitjaIdIssued,
                    EkzekutimiPagesesId = null,
                    Koment = "Gift card u krijua.",
                    Created_At = DateTime.Now,
                    Created_By = model.Created_By
                }, conn, tx);

                tx.Commit();

                return giftCardId;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
        public int IssueGiftCard(decimal shuma, int shitjaId, int userId, NpgsqlConnection conn, NpgsqlTransaction tx, string kodi = null)
        {
            if (shuma <= 0)
                throw new Exception("Vlera e gift card duhet të jetë më e madhe se zero.");

            string finalCode = string.IsNullOrWhiteSpace(kodi) ? _giftCardRepo.GenerateUniqueCode() : kodi.Trim();
            var card = new GiftCardModel
            {
                Kodi = finalCode,
                Barkodi = finalCode,
                ShumaFillestare = shuma,
                BilanciAktual = shuma,
                Statusi = "Aktiv",
                ShitjaIdIssued = shitjaId,
                Activated_At = DateTime.Now,
                Expires_At = null,
                Created_At = DateTime.Now,
                Created_By = userId
            };

            int giftCardId = _giftCardRepo.InsertGiftCard(card, conn, tx);
            _giftCardRepo.InsertTransaction(new GiftCardTransactionModel
            {
                GiftCardId = giftCardId,
                Type = "ISSUE",
                Shuma = shuma,
                ShitjaId = shitjaId,
                EkzekutimiPagesesId = null,
                Koment = "Gift card u krijua nga shitja.",
                Created_At = DateTime.Now,
                Created_By = userId
            }
             ,conn, tx);
            return giftCardId;
        }

        public void RedeemGiftCard(string kodi, decimal shuma, int shitjaId, int ekzekutimiPagesesId, int userId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            if (shuma <= 0)
                throw new Exception("Shuma për redeem duhet të jetë më e madhe se zero.");
            var card = _giftCardRepo.GetByCode(kodi.Trim(), conn, tx);
            if (card == null)
                throw new Exception("Gift card nuk u gjet.");

            if (!string.Equals(card.Statusi, "Aktiv", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Gift card nuk është aktiv.");

            if (card.Expires_At.HasValue && card.Expires_At.Value < DateTime.Now)
                throw new Exception("Gift card ka skaduar.");

            if (card.BilanciAktual < shuma)
                throw new Exception($"Gift card nuk ka bilanc të mjaftueshëm. Bilanci aktual: {card.BilanciAktual:0.00} €");

            decimal newBalance = card.BilanciAktual - shuma;
            string newStatus = newBalance <= 0 ? "Redeemed" : "Aktiv";
            _giftCardRepo.UpdateBalance(card.Id, newBalance, newStatus, conn, tx);
            _giftCardRepo.InsertTransaction(new GiftCardTransactionModel
            {
                GiftCardId = card.Id,
                Type = "Redeem",
                Shuma = shuma,
                ShitjaId = shitjaId,
                EkzekutimiPagesesId = ekzekutimiPagesesId,
                Koment = $"Gift card u përdor në shitje. Kodi: {card.Kodi}.",
                Created_At = DateTime.Now,
                Created_By = userId
            }, conn, tx);

        }

        public string GenerateUniqueCode() => _giftCardRepo.GenerateUniqueCode();

        public List<GiftCardTransactionModel> GetTransactionsByCode(string kodi)
        {
            if (string.IsNullOrWhiteSpace(kodi))
                throw new Exception("Kodi i gift card është i detyrueshëm.");

            var card = _giftCardRepo.GetByCode(kodi.Trim());
            if (card == null)
                throw new Exception("Gift card nuk u gjet.");

            return _giftCardRepo.GetTransactionsByGiftCardId(card.Id);
        }

        public GiftCardModel IssueGiftCardFromSale(decimal shuma, int shitjaId, int userId)
        {
            if (shuma <= 0)
                throw new Exception("Vlera e gift card duhet të jetë më e madhe se zero.");

            string kodi = GenerateUniqueCode();

            var model = new GiftCardModel
            {
                Kodi = kodi,
                Barkodi = kodi,
                ShumaFillestare = shuma,
                BilanciAktual = shuma,
                Statusi = "Aktiv",
                Activated_At = DateTime.Now,
                Expires_At = DateTime.Now.AddYears(1),
                Created_At = DateTime.Now,
                Created_By = userId,
                ShitjaIdIssued = shitjaId
            };

            CreateGiftCard(model);
            return model;
        }

        public List<GiftCardModel> GetAll(bool onlyAvailable = false)
        {
            return _giftCardRepo.GetAll(onlyAvailable);
        }

        public void UseGiftCard(
            string code,
            decimal amount,
            int shitjaId,
            int ekzekutimiPagesesId,
            int userId,
            NpgsqlConnection conn,
            NpgsqlTransaction tx)
        {
            _giftCardRepo.UseGiftCard(code, amount, shitjaId, ekzekutimiPagesesId, userId, conn, tx);
        }
    }
}
