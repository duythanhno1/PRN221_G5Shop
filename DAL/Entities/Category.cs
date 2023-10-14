using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Category
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool isDeleted { get; set; }
    }
}
