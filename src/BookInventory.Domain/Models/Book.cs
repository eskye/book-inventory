using System;
using System.Collections.Generic;
using System.Text;

namespace BookInventory.Domain.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string Year { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}
