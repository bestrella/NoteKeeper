using LiteDB;
using NoteKeeper.Core.Abstracts;
using NoteKeeper.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Core.Controllers
{
    class NoteController:INoteController
    {
        private const string liteDBPath = @"D:\litedb.db";

        public List<Models.Note> GetAll()
        {
            using (var db = new LiteDatabase(liteDBPath))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<Note>("notes");
                return col.FindAll().ToList<Note>();
            }
        }

        public Note Get(int id)
        {
            using (var db = new LiteDatabase(liteDBPath))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<Note>("notes");
                col.EnsureIndex(x => x.Id);
                return col.FindOne(x => x.Id == id);
            }
            // Index document using document Name property
            //col.EnsureIndex(x => x.Name);

            // Use LINQ to query documents
            //var results = col.Find(x => x.Name.StartsWith("Jo"));
        }

        public bool Add(Models.Note note)
        {
            try
            {
                using (var db = new LiteDatabase(liteDBPath))
                {
                    // Get a collection (or create, if not exits)
                    var col = db.GetCollection<Note>("notes");
                    return col.Insert(note);
                }
            }
            catch (Exception)
            {
                
                //throw;
                return false;
            }
        }

        public bool Update(Models.Note note)
        {
            //var results = col.Find(x => x.Name.StartsWith("Jo"));
            try
            {
                using (var db = new LiteDatabase(liteDBPath))
                {
                    // Get a collection (or create, if not exits)
                    var col = db.GetCollection<Note>("notes");
                    return col.Update(note);
                }
            }
            catch (Exception)
            {

                //throw;
                return false;
            }
        }

        public bool Delete(int id)
        {
            //var results = col.Find(x => x.Name.StartsWith("Jo"));
            try
            {
                using (var db = new LiteDatabase(liteDBPath))
                {
                    // Get a collection (or create, if not exits)
                    var col = db.GetCollection<Note>("notes");
                    var note = col.FindOne(x => x.Id == id);

                    if (note == null)
                        return false;

                    if (col.Delete(x => x.Id == id) <= 0)
                        return false;

                    return true;
                }
            }
            catch (Exception)
            {

                //throw;
                return false;
            }
        }


    }
}
