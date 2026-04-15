using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.services.products
{
    public interface IProductService 
    {
        List<ProductModel> GetAllForManagement();
        List<string> GetProductNames();
        bool ExistsByBarcodeOrName(string barcode, string emri);
        void Add(ProductModel model);
        void Update(ProductModel model);
        void Delete(int id);
        ProductModel GetByBarcode(string barkodi);
        ProductModel GetById(int id);
    }
}
