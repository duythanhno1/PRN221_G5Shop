using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key] public Guid ID { get; set; }
        public float Amount { get; set; }
        public string PreviousHash { get; set; }
        public string HashValue { get; set; }
        public decimal  PreviousBalance { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public virtual Orders Order { get; set; }
    }
}
