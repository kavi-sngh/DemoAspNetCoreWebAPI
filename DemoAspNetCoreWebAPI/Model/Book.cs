using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCoreWebAPI.Model
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        [Required]
        public string Author { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }
    }
}
