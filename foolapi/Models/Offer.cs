using System;

namespace foolapi.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public int ProductId { get; set; }
        public Decimal Price { get; set; }
        public DateTime DateCreated  { get; set; }
        public DateTime DateModified  { get; set; }
        public int NumberOfTerms { get; set; }
        public string Description { get; set; }
    }
}