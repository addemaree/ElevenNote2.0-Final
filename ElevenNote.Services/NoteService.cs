using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Web.Data;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        //This makes the user Id available to us in all methods.
        //When we create a new instance of the NoteService, we need to give it a userId.
        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.UtcNow
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                //Returns an integer. Number of rows updated in DB.
                //This will add 1 new row in DB.
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            //Context to get data out of the database
            //Keep it constrained to method body so things get cleaned up/disposed.
            using (var ctx = new ApplicationDbContext())
            {
                //Use LINQ to ask a question of the db.
                //ctx.Notes represent all the notes in the system by user
                //.Where takes all of the notes and filters them down to the ones that are just me.

                var query =
                    ctx
                        .Notes
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                        e => new NoteListItem
                        {
                            NoteId = e.NoteId,
                            Title = e.Title,
                            CreatedUTC = e.CreatedUtc
                        }
                    );
                return query.ToArray();
            }
        }
    }
}
