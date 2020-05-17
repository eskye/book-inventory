using System.Threading.Tasks;
using BookInventory.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookInventory.Api.Controllers
{

    
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var response = await _bookService.GetListOfBooks();
            return Ok(response);
        }
    }
}