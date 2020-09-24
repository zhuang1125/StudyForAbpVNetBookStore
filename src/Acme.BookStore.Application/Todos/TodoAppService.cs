using System;
using Acme.BookStore.Permissions;
using Acme.BookStore.Todos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Todos
{
    public class TodoAppService : CrudAppService<Todo, TodoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTodoDto, CreateUpdateTodoDto>,
        ITodoAppService
    {
        protected override string GetPolicyName { get; set; } = BookStorePermissions.Todo.Default;
        protected override string GetListPolicyName { get; set; } = BookStorePermissions.Todo.Default;
        protected override string CreatePolicyName { get; set; } = BookStorePermissions.Todo.Create;
        protected override string UpdatePolicyName { get; set; } = BookStorePermissions.Todo.Update;
        protected override string DeletePolicyName { get; set; } = BookStorePermissions.Todo.Delete;

        private readonly ITodoRepository _repository;
        
        public TodoAppService(ITodoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
