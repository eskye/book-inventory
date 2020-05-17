using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookInventory.DataLayer.RepositoryImplementation.Implementation;
using BookInventory.DataLayer.RepositoryImplementation.Interfaces;
using BookInventory.Domain.Models;

namespace BookInventory.DataLayer.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IReadOnlyList<Book>> SearchBook(string searchQuery);
        Task<IReadOnlyList<Book>> GetListOfBooks();
    }
    public class BookRepository : Repository<Book>,IBookRepository
    {
        public BookRepository(BookInventoryDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<IReadOnlyList<Book>> SearchBook(string searchQuery)
        {
            var result = await WhereAsync(x => x.Author.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
                                               || x.Title.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
                                               || x.Isbn.Equals(searchQuery, StringComparison.OrdinalIgnoreCase));
            return result.ToList();
        }

        public async Task<IReadOnlyList<Book>> GetListOfBooks()
        {
            var books = await GetAllAsync();
            return books.ToList();
        }
    }
}
