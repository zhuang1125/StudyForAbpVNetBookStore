using System;
using Acme.BookStore.Todos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Todos
{
    public interface ITodoAppService :
        ICrudAppService< 
            TodoDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateTodoDto,
            CreateUpdateTodoDto>
    {

    }
}