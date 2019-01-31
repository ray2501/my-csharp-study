using System;
using System.ComponentModel.DataAnnotations;

namespace NancyApplication.Data
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(255)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }

        public DateTime Created { get; set; }
    }
}

