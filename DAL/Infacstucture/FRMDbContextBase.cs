using Microsoft.EntityFrameworkCore;

namespace DAL.Infacstucture
{
    public class FRMDbContextBase<T> where T : class
    {
        private FRMDbContext _dbContext;
        protected DbSet<T> _dbSet { get; private set; }
        private IDbFactory DbFactory { get; }
        private FRMDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = DbFactory.Init();
                }
                return _dbContext;
            }
        }

        protected FRMDbContextBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;

            _dbSet = DbContext.Set<T>();
        }
    }
}