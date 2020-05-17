using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookInventory.Api.Controllers;
using BookInventory.Api.ViewModels;
using BookInventory.Logic.Dtos;
using BookInventory.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookInventory.Test
{
     
    public class BookControllerTest
    {
        [Fact]
        public async Task Get_ReturnsOkListOfBooks()
        {
            //Arrange
            var bookServiceMock = new Mock<IBookService>();

            var sut = new BookController(bookServiceMock.Object);

            IReadOnlyList<BookListDto> books = new List<BookListDto>();
            bookServiceMock.Setup(c => c.GetListOfBooks())
                .Returns(Task.FromResult(books));

            //Act
            var result = await sut.Get();
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result);

        }

        [Fact]
        public async Task Get_ReturnsOkListOfBooks_SearchQueryIsFound()
        {
            //Arrange
            var bookServiceMock = new Mock<IBookService>();

            var sut = new BookController(bookServiceMock.Object);

            IReadOnlyList<BookListDto> books = new List<BookListDto>();
            bookServiceMock.Setup(c => c.SearchBook(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(books));

            //Act
            var result = await sut.Search("testquery","Author");
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result);

        }


        [Fact]
        public async Task Create_ReturnsCreated_If_successful()
        {
            //Arrange
            var bookServiceMock = new Mock<IBookService>();

            var sut = new BookController(bookServiceMock.Object);
            bookServiceMock.Setup(c => c.AddBook(It.IsAny<CreateBookDto>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await sut.Post(new CreateBookViewModel(){Isbn = "Isbn", Title = "Title", Author = "Author",Year = "2013",Publisher = "Publisher"});

            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedResult>(result);

        }


        [Fact]
        public async Task Update_ReturnsUpdated_If_successful()
        {
            //Arrange
            var bookServiceMock = new Mock<IBookService>();

            var sut = new BookController(bookServiceMock.Object);
            bookServiceMock.Setup(c => c.UpdateBook(It.IsAny<CreateBookDto>(), It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await sut.Put(new CreateBookViewModel() { Isbn = "Isbn", Title = "Title", Author = "Author", Year = "2013", Publisher = "Publisher" }, 1);

            Assert.NotNull(result);
          var response =  Assert.IsAssignableFrom<OkObjectResult>(result);
          Assert.Equal(response.Value, $"Updated successfully");

        }

        [Fact]
        public async Task Delete_ReturnsDeleted_If_successful()
        {
            //Arrange
            var bookServiceMock = new Mock<IBookService>();

            var sut = new BookController(bookServiceMock.Object);
            bookServiceMock.Setup(c => c.DeleteBook(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await sut.Delete( 1);

            Assert.NotNull(result);
            var response = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(response.Value, $"Deleted successfully");

        }
    }
}
