using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLBenefits.Core.Domain;
using SLBenefits.Core.Helper;

namespace SLBenefits.Core.Service
{
    public interface ICategoryService
    {
        long Save(Category category);
        List<Category> GetAll();
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public long Save(Category category)
        {
            if (category.Id == 0)
            {
                category.IsActive = true;
                _categoryRepository.Insert(category);
            }
            else
            {
                _categoryRepository.Update(category);
            }
            _categoryRepository.Save();
            return category.Id;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetQuery().Where(q => q.IsActive).ToList();
        }
    }
}
