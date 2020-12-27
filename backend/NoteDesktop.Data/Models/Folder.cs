using System;

namespace NoteDesktop.Data.Models
{
    public class Folder
    {
        public int id { get; set; }
        public Guid userid { get; set; }
        public int parentid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createddate { get; set; }
        public DateTime modifieddate { get; set; }
        //public int Order { get; set; }
    }
}
