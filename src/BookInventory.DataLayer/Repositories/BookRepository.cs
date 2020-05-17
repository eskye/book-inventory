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
        Task<IReadOnlyList<Book>> SearchBook(string searchQuery, string clomun);
        Task<IReadOnlyList<Book>> GetListOfBooks();
    }
    public class BookRepository : Repository<Book>,IBookRepository
    {
        public BookRepository(BookInventoryDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<IReadOnlyList<Book>> SearchBook(string searchQuery, string column)
        {
            var result = new List<Book>();
            if (column == "Author")
            {
                var response = await WhereAsync(x => x.Author == searchQuery);
                result = response.ToList();
            }
            else if(column == "Title")
            {
                var response = await WhereAsync(x => x.Title == searchQuery);
                result = response.ToList();
            }else if (column == "Isbn")
            {
                var response = await WhereAsync(x => x.Isbn == searchQuery);
                result = response.ToList();
            }
            else
            {
                var response = await WhereAsync(x => x.Title.StartsWith(searchQuery));
                result = response.ToList();
            }

            return result;
        }

        public async Task<IReadOnlyList<Book>> GetListOfBooks()
        {
            var books = await GetAllAsync();
            return books.ToList();
        }
    }
}
