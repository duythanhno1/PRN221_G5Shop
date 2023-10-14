using DAL.Entities;
using DAL.Infacstucture;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements {
    public class TransactionRepository : FRMDbContextBase<Transaction>, ITransactionRepository {
        private readonly FRMDbContext _dbContext;
        public TransactionRepository(IDbFactory dbFactory) : base(dbFactory) {
            _dbContext = dbFactory.Init();
        }

        public Task<Transaction?> GetLatestTransaction()
        {
            return _dbSet.OrderByDescending(tr => tr.CreatedDate).Include(tr => tr.Order).FirstOrDefaultAsync();
        }

        public void Save(Transaction transaction)
        {
            _dbSet.AddAsync(transaction);
        }
    }
}
