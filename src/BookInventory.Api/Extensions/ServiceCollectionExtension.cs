using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookInventory.DataLayer.Repositories;
using BookInventory.DataLayer.RepositoryImplementation.Implementation;
using BookInventory.DataLayer.RepositoryImplementation.Interfaces;
using BookInventory.Logic.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BookInventory.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomServiceExtensions(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();

            SwaggerConfiguration(services);

            return services;
        }

        #region SwaggerContactInfoRegion

        private static void SwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Book Inventory Management System",
                    Description = "API Version 1.0",

                    Contact = new OpenApiContact()
                    {
                        Name = "Sunkanmi Ijatuyi",
                        Email = "sunkanmiijatuyi@gmail.com"
                    }
                });

            });
        }

        #endregion
    }
}
