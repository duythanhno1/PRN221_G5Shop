using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Product> Products { get; set; }

      
    }
}
