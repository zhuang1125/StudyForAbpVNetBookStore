using System;
using Acme.BookStore.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.Todos
{
    public class TodoRepository : EfCoreRepository<BookStoreDbContext, Todo, Guid>, ITodoRepository
    {
        public TodoRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}