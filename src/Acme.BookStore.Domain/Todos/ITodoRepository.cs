using System;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Todos
{
    public interface ITodoRepository : IRepository<Todo, Guid>
    {
    }
}