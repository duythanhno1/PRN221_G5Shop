using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BAL.Model
{
    public class CateAndProductViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
