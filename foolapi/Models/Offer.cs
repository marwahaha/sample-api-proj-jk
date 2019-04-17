using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foolapi.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public DateTime DateCreated  { get; set; }
        public DateTime DateModified  { get; set; }
        public int NumberOfTerms { get; set; }
        public string Description { get; set; }
    }
}