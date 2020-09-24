using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Acme.BookStore.Todos;
using Acme.BookStore.Todos.Dtos;
using Acme.BookStore.Web.Pages.Todos.Todo.ViewModels;

namespace Acme.BookStore.Web.Pages.Todos.Todo
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreateEditTodoViewModel ViewModel { get; set; }

        private readonly ITodoAppService _service;

        public CreateModalModel(ITodoAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTodoViewModel, CreateUpdateTodoDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}