using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class Cart
    {
        [Key]
        public Guid ID { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Quantity { get; set; }
    }
}
