using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace bnotes.Data
{
    public class Note
    {
        [Key]
        [XmlElement]
        public Guid Id { get; set; }

        [Required, StringLength(255)]
        [XmlElement]
        public string Title { get; set; }

        [Required]
        [XmlElement]
        public string Body { get; set; }

        [XmlElement]
        public DateTime Created { get; set; }
    }
}
