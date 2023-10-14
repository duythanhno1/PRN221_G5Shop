using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BAL.Model
{
    public class UserViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public bool isDeleted { get; set; }
    }
}
