using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.products;

namespace POSProject.services.products
{ 
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public List<ProductModel> GetAllForManagement()
        {
            return _repo.GetAllForManagement();
        }

        public List<string> GetProductNames()
        {
            return _repo.GetProductNames();
        }

        public bool ExistsByBarcodeOrName(string barcode, string emri)
        {
            return _repo.ExistsByBarcodeOrName(barcode, emri);
        }

        public void Add(ProductModel model)
        {
            _repo.Insert(model);
        }

        public void Update(ProductModel model)
        {
            _repo.Update(model);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
       
        public ProductModel GetByBarcode(string barkodi) => _repo.GetByBarcode(barkodi);
        public ProductModel GetById(int id) => _repo.GetById(id);
        public bool HasEnoughStock(int artikulliId, decimal sasiaKerkuar, decimal sasiaNeFature)
        {
            decimal stock = _repo.GetStockById(artikulliId);
            return sasiaKerkuar + sasiaNeFature <= stock;
        }

        public decimal GetQuantityInStock(int artikulliId) => _repo.GetStockById(artikulliId);

    }
}
