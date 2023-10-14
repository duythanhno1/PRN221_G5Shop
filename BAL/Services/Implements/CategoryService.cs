using System;
using System.Linq;
using System.Text;
using DAL.Infacstucture;
using System.Threading.Tasks;
using DAL.Repositories.Interface;
using System.Collections.Generic;

namespace BAL.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
