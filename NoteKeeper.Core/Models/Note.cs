using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Core.Models
{
    public class Note
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public bool IsHidden { get; set; }
        //public DateTime LastUpdate { get; set; }

        //public int AuthorId { get; set; }
    }
}
