using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Infacstucture
{
    public class DbFactory : IDbFactory
    {
        private FRMDbContext _dbContext;

        // Team 1 addin Dependency injection
        public DbFactory(FRMDbContext dbContext)
        {
            _dbContext = dbContext;
        }
      

        public FRMDbContext Init()
        {
            if (_dbContext == null)
            {
                _dbContext = new FRMDbContext();
            }
            return _dbContext;
        }
    
    }
}
