using System;

using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Web.Pages.Todos.Todo.ViewModels
{
    public class CreateEditTodoViewModel
    {
        [Display(Name = "TodoContent")]
        public string Content { get; set; }

        [Display(Name = "TodoDueTime")]
        public DateTime? DueTime { get; set; }

        [Display(Name = "TodoIsDone")]
        public bool IsDone { get; set; }
    }
}