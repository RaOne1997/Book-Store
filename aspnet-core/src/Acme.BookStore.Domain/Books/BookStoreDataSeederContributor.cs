using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Acme.BookStore
{
    public class BookStoreDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<IdentityRole, Guid> _rolesRepository;

        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository, IRepository<IdentityRole, Guid> rolesRepository)
        {
            _bookRepository = bookRepository;
            _rolesRepository = rolesRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() <= 0)
            {
                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "1984",
                        Type = BookType.Dystopia,
                        PublishDate = new DateTime(1949, 6, 8),
                        Price = 19.84f
                    },
                    autoSave: true
                );

                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "The Hitchhiker's Guide to the Galaxy",
                        Type = BookType.ScienceFiction,
                        PublishDate = new DateTime(1995, 9, 27),
                        Price = 42.0f
                    },
                    autoSave: true
                );
            }
            if (await _rolesRepository.GetCountAsync() <= 1)
            {
                await _rolesRepository.InsertAsync(
                    new IdentityRole(

                         new Guid(),
                         "user",
                         null
                        )
                    {
                        IsPublic = true,
                        IsStatic = true,
                    },
                    autoSave:true);

                await _rolesRepository.InsertAsync(
                    new IdentityRole(

                         new Guid(),
                         "Accoutent",
                         null
                        )
                    {
                        IsPublic= true,
                        IsStatic= true,
                    },

                    autoSave: true);


            }
        }
    }
}
