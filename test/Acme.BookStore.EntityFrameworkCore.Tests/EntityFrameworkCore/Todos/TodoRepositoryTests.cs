using System;
using System.Threading.Tasks;
using Acme.BookStore.Todos;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Acme.BookStore.EntityFrameworkCore.Todos
{
    public class TodoRepositoryTests : BookStoreEntityFrameworkCoreTestBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoRepositoryTests()
        {
            _todoRepository = GetRequiredService<ITodoRepository>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
        */
    }
}
