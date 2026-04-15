using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.products;

namespace POSProject.services.products
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryModel> GetAll()
        {
            return _categoryRepository.GetActiveCategories();
        }
    }
}
