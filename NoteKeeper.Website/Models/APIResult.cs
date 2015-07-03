using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteKeeper.Website.Models
{
    //data returned by API services
    public class APIResult<T>
    {
        public T Data { get; set; }
        public String Message { get; set; }
        public int Count { get; set; }
        public DateTime GeneratedOn { get; set; }
        public Int32 Code { get; set; }
    }
}