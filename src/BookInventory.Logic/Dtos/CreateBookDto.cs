namespace BookInventory.Logic.Dtos
{
   public class CreateBookDto
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string Year { get; set; } 
    }
}
