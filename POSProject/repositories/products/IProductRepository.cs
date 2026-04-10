using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.products
{
    public interface IProductRepository
    {
        ProductModel GetByBarcode(string barkodi);
        ProductModel GetById(int id);
        decimal GetStockById(int id);
        void UpdateStock(int artikulliId, decimal sasia, NpgsqlConnection conn, NpgsqlTransaction tx);
    }
}
