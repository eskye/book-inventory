using System;

namespace BookInventory.Logic.Dtos
{
    public class BookListDto
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string Year { get; set; }
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}