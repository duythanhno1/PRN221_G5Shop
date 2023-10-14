using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Threading.Tasks;
using DAL.Repositories.Interface;
using System.Collections.Generic;

namespace DAL.Repositories.Implements
{
    public class CategoryRepository : FRMDbContextBase<Category>, ICategoryRepository
    {
        private readonly FRMDbContext _dbContext;
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }
    }
}
