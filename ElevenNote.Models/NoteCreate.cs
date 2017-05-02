using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(128, ErrorMessage = "Title is too long.")]
        public string Title { get; set; }


        [Required]
        [MaxLength(8000, ErrorMessage = "We don't want to read your short novel!")]
        public string Content { get; set; }

        public override string ToString() => Title;

    }
}
