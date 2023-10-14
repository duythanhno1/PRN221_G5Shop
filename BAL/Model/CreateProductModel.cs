using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BAL.Model
{
    public class CreateProductModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string? imgPath { get; set; }
        public bool isAvailable { get; set; }
        public long CategoryID { get; set; }
        public bool isDeleted { get; set; }
    }
}
