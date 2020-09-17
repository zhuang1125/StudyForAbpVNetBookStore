using System;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        public Guid AuthorId { get; set; }
    }


    public static class BookExtensions
    {
        private const string TitlePropertyName = "Title";

        public static void SetTitle(this Book user, string title)
        {
            user.SetProperty(TitlePropertyName, title);
        }

        public static string GetTitle(this Book user)
        {
            return user.GetProperty<string>(TitlePropertyName);
        }
    }
}
