using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foolapi.Models
{
    public class Brand
    {
        public Int16 BrandId { get; set; }
        public string BrandCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}