using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Data;

namespace Acme.BookStore.Books
{
    public class CreateUpdateBookDto: IHasExtraProperties
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }

        public Guid AuthorId { get; set; }

        public Dictionary<string, object> ExtraProperties { get; set; } = new Dictionary<string, object>();
    }
}
