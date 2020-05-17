using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BookInventory.DataLayer.Repositories;
using BookInventory.DataLayer.RepositoryImplementation.Interfaces;
using BookInventory.Domain.Models;
using BookInventory.Logic.Dtos;
using BookInventory.Logic.Services;
using Moq;
using Xunit;

namespace BookInventory.Test
{
    public class BookServiceTest
    {
        [Fact]
        public async Task BookServiceTest_AddBook_Successful()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock.Setup(b => b.BeginTransaction(It.IsAny<IsolationLevel>()));

            bookRepoMock.Setup(c => c.InsertAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

            unitOfWorkMock.Setup(c => c.Commit()); 

            await sut.AddBook(new CreateBookDto
                {Author = "Author", Isbn = "Isbn", Title = "Title", Year = "2039", Publisher = "Publisher"});

            

        }

        [Fact]
        public async Task BookServiceTest_AddBook_ThrowException_If_Null()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock.Setup(b => b.BeginTransaction(It.IsAny<IsolationLevel>()));

            unitOfWorkMock.Setup(c => c.Commit());

            unitOfWorkMock.Setup(c => c.Rollback());
            
            await Assert.ThrowsAsync<Exception>(() => sut.AddBook(null));



        }

        [Fact]
        public async Task BookServiceTest_UpdateBook_Successful()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock.Setup(b => b.BeginTransaction(It.IsAny<IsolationLevel>()));

            bookRepoMock.Setup(c => c.GetAsync(It.IsAny<long>())).Returns(Task.FromResult(new Book()));
           
            bookRepoMock.Setup(c => c.Update(It.IsAny<Book>()));

            unitOfWorkMock.Setup(c => c.Commit());

             await sut.UpdateBook(new CreateBookDto
                { Author = "Author", Isbn = "Isbn", Title = "Title", Year = "2039", Publisher = "Publisher" }, 1);

        }

        [Fact]
        public async Task BookServiceTest_UpdateBook_ThrowException_If_Null()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock.Setup(b => b.BeginTransaction(It.IsAny<IsolationLevel>()));

            unitOfWorkMock.Setup(c => c.Commit());

            unitOfWorkMock.Setup(c => c.Rollback());

            await Assert.ThrowsAsync<Exception>(() => sut.UpdateBook(null, 0));

        }

        [Fact]
        public async Task BookServiceTest_DeleteBook_Successful()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock.Setup(b => b.BeginTransaction(It.IsAny<IsolationLevel>()));

            bookRepoMock.Setup(c => c.GetAsync(It.IsAny<long>())).Returns(Task.FromResult(new Book()));

            bookRepoMock.Setup(c => c.DeleteAsync(It.IsAny<Book>(), It.IsAny<bool>())).Returns(Task.CompletedTask);

            unitOfWorkMock.Setup(c => c.Commit());

            await sut.DeleteBook(1);

        }


        [Fact]
        public async Task BookServiceTest_DeleteBook_ThrowException_If_Book_Not_found()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock.Setup(b => b.BeginTransaction(It.IsAny<IsolationLevel>()));

            unitOfWorkMock.Setup(c => c.Commit());

            unitOfWorkMock.Setup(c => c.Rollback());

           var response = await Assert.ThrowsAsync<Exception>(() => sut.DeleteBook(0));

           Assert.Equal(response.Message, $"The book you are trying to delete does not exist");
        }

        [Fact]
        public async Task BookServiceTest_Return_List_Of_Books()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            IReadOnlyList<Book> books = new List<Book>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);
            bookRepoMock.Setup(c => c.GetListOfBooks()).Returns(Task.FromResult(books));

            var result = await sut.GetListOfBooks();

            Assert.Equal(result, new List<BookListDto>()); 
        }

        [Fact]
        public async Task BookServiceTest_Return_List_Of_Books_When_Sarch()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bookRepoMock = new Mock<IBookRepository>();

            IReadOnlyList<Book> books = new List<Book>();

            var sut = new BookService(bookRepoMock.Object, unitOfWorkMock.Object);
            bookRepoMock.Setup(c => c.SearchBook(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(books));

            var result = await sut.SearchBook("testQuery", "Author");

            Assert.Equal(result, new List<BookListDto>());
        }
    }
}
