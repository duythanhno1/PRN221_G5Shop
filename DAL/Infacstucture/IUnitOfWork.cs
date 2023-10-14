using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Infacstucture
{
    public interface IUnitOfWork
    {
        void Commit();
        Task commitAsync();
    }
}
