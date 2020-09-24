using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Todos
{
    public class Todo : AggregateRoot<Guid>
    {
        public virtual string Content { get; private set; }
        public virtual DateTime? DueTime { get; private set; }
        public virtual bool IsDone{ get; private set; }

        protected Todo() { }
        public Todo(Guid id, string content, DateTime? dueTime,bool isDone) : base(id)
        {
            Content = content;
            DueTime = dueTime;
            IsDone = isDone;
        }
    }
}
