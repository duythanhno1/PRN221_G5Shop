using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implements
{
    public class OrderDetailRepository : FRMDbContextBase<OrderDetail>, IOrderDetailRepository
    {
        private readonly FRMDbContext _dbContext;
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void SaveList(List<OrderDetail> orderDetails)
        {
            _dbSet.AddRange(orderDetails);
        }
    }
}
