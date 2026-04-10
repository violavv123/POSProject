using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.repositories.products
{
    public interface ICategoryRepository
    {
        List<CategoryModel> GetActiveCategories();
    }
}
