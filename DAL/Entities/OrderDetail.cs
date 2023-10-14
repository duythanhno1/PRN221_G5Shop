using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class OrderDetail
    {
        [Key]
        public Guid ID { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Product Product { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Amount { get; set; }
        public bool isDeleted { get;set;}
    }
}
