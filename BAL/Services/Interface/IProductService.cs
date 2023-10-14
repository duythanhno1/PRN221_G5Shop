using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interface
{
    public interface IProductService
    {
        List<Product> Search(string searchItems);
        List<Product> Filter(int id);
        Product? GetProductById(long id);
        List<Product> sortPro(int sortId, List<Product> product);
    }
}
