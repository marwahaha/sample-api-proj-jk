using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foolapi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
        public string Brand { get; set; }
        public string Term { get; set; }
        public Int16 BrandId { get; set; }
    }
} 