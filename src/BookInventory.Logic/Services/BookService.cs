using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookInventory.DataLayer.Repositories;
using BookInventory.DataLayer.RepositoryImplementation.Interfaces;
using BookInventory.Domain.Models;
using BookInventory.Logic.Dtos;

namespace BookInventory.Logic.Services
{
    public interface IBookService
    {
        Task AddBook(CreateBookDto model);
        Task UpdateBook(CreateBookDto model, long bookId);
        Task DeleteBook(long bookId);
        Task<IReadOnlyList<BookListDto>> GetListOfBooks();
        Task<IReadOnlyList<BookListDto>> SearchBook(string query);
    }
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddBook(CreateBookDto model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var bookEntity = new Book
                {
                    Isbn = model.Isbn,
                    Author = model.Author,
                    Title = model.Title,
                    Year = model.Year,
                    Publisher = model.Publisher,
                    CreationTime = DateTime.UtcNow,
                    LastModifiedTime = DateTime.UtcNow
                };
                await _bookRepository.InsertAsync(bookEntity);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateBook(CreateBookDto model, long bookId)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var bookEntity = await _bookRepository.GetAsync(bookId) ??
                                 throw new Exception("The book you are looking for does not exist");
                bookEntity.Author = model.Author;
                bookEntity.Isbn = model.Isbn;
                bookEntity.Publisher = model.Publisher;
                bookEntity.Title = model.Title;
                bookEntity.Year = model.Year;
                bookEntity.LastModifiedTime = DateTime.UtcNow;
                _bookRepository.Update(bookEntity);
                 _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteBook(long bookId)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var bookEntity = await _bookRepository.GetAsync(bookId) ??
                                 throw new Exception("The book you are trying to delete does not exist");
                await _bookRepository.DeleteAsync(bookEntity);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<BookListDto>> GetListOfBooks()
        {
            var entities = await _bookRepository.GetListOfBooks();
            return ProcessQuery(entities);
        }

        
        public async Task<IReadOnlyList<BookListDto>> SearchBook(string query)
        {
            var entities = await _bookRepository.SearchBook(query);
            return ProcessQuery(entities);
        }

        private IReadOnlyList<BookListDto> ProcessQuery(IEnumerable<Book> entities)
        {
            return entities.Select(x =>
            {
                var item = new BookListDto
                {
                    Isbn = x.Isbn,
                    Author = x.Author,
                    Title = x.Title,
                    Year = x.Year,
                    Publisher = x.Publisher,
                    CreationTime = x.CreationTime,
                    Id = x.Id
                };
                return item;

            }).ToList();
        }

    }
}
