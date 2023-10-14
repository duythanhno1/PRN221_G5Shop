using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interface
{
    public interface IOrderDetailRepository
    {
        public void SaveList(List<OrderDetail> orderDetails);
    }
}
