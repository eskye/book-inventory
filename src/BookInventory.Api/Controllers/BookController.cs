using System.Threading.Tasks;
using BookInventory.Api.ViewModels;
using BookInventory.Logic.Dtos;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _bookService.GetListOfBooks();
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery(Name = "query")] string query, string column)
        {
            var response = await _bookService.SearchBook(query, column);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookViewModel model)
        {
            var createBookDto = new CreateBookDto
            {
                Isbn = model.Isbn,
                Author = model.Author,
                Title = model.Title,
                Year = model.Year,
                Publisher = model.Publisher
            };
             await _bookService.AddBook(createBookDto);
            return Created("", "Book Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CreateBookViewModel model, long id)
        {
            var createBookDto = new CreateBookDto
            {
                Isbn = model.Isbn,
                Author = model.Author,
                Title = model.Title,
                Year = model.Year,
                Publisher = model.Publisher
            };
            await _bookService.UpdateBook(createBookDto, id);
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _bookService.DeleteBook(id);
            return Ok("Deleted successfully");
        }
    }
}