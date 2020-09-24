using System;
using System.ComponentModel;
namespace Acme.BookStore.Todos.Dtos
{
    [Serializable]
    public class CreateUpdateTodoDto
    {
        public string Content { get; set; }

        public DateTime? DueTime { get; set; }

        public bool IsDone { get; set; }
    }
}