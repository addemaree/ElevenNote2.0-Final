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
                new Note
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
                            IsStarred = e.IsStarred,
                            CreatedUtc = e.CreatedUtc
                        }
                    );
                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        Content = entity.Content,
                        IsStarred = entity.IsStarred,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.IsStarred = model.IsStarred;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);
                //Mark for deletion
                ctx.Notes.Remove(entity);

                //Only do one change
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
