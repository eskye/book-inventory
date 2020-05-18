using System.ComponentModel.DataAnnotations;

namespace BookInventory.Api.ViewModels
{
    public class CreateBookViewModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public string Year { get; set; }
    }
}
