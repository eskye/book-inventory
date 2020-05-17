using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookInventory.Api.ViewModels
{
    public class CreateBookViewModel
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string Year { get; set; }
    }
}
