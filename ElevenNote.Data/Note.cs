using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        
        [Required]
        //DateTimeOffset carries timezone info with it
        public DateTimeOffset CreatedUtc { get; set; }
        
        
        //Why do we care? To tell if something has been edited. Capture a timestamp
        public DateTimeOffset? ModifiedUtc { get; set; }


        //Notes:


        //GUIDS:

        //GUID - Globally Unique Identifier
        //Global, as in the world
        //Computer that this is running on is capable of generating
        //Identity Framework identifies users by GUID. It's a hexadecimal character
        //Once user is created, it creates a GUID. Tied to the account.
        //Performance of the database. Inserting a new row, we want something that's incremental.
        //It's faster to add something to the end of the list. Not the middle

        //VALUE TYPES VS. REFERENCE TYPES
        //DateTimeOffset is a value type. They CAN'T be null.
        //Objects are a reference type. They can be null.
        //Adding the question mark to ModifiedUtc allows a value type to be null.
    }
}
