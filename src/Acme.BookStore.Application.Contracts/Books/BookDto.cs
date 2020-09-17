using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Acme.BookStore.Books
{
    public class BookDto : ExtensibleAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
    }


    public static class BookDtoExtensions
    {
        private const string TitlePropertyName = "Title";

        public static void SetTitle(this BookDto user, string title)
        {
            user.SetProperty(TitlePropertyName, title);
        }

        public static string GetTitle(this BookDto user)
        {
            return user.GetProperty<string>(TitlePropertyName);
        }
    }
}
