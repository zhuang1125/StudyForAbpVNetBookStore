using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Todos.Dtos
{
    [Serializable]
    public class TodoDto : EntityDto<Guid>
    {
        public string Content { get; set; }

        public DateTime? DueTime { get; set; }

        public bool IsDone { get; set; }
    }
}