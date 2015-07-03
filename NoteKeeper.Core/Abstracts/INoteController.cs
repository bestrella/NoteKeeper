using NoteKeeper.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Core.Abstracts
{
    public interface INoteController
    {
        Note Get(int id);
        List<Note> GetAll();
        bool Add(Note note);
        bool Update(Note note);
        bool Delete(int id);
    }
}
