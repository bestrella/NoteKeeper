using NoteKeeper.Core.Abstracts;
using NoteKeeper.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Core
{
    public class NoteKeeperFactory
    {
        public static INoteController CreateNoteController()
        {
            return new NoteController();
        }
    }
}
