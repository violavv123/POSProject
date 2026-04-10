using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.sales
{
    public interface ISaleRepository
    {
        int InsertSale(SaleModel sale, NpgsqlConnection conn, NpgsqlTransaction tx);
        void InsertSaleDetail(SaleDetailModel saleDetail, NpgsqlConnection conn, NpgsqlTransaction tx);
    }
}
