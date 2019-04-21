using System;
using System.Collections.Generic;

namespace NoteWebApplication.Models
{
    public partial class Notes
    {
        public Guid NoteId { get; set; }
        public string NoteDetail { get; set; }
        public DateTime? NotedDate { get; set; }
    }
}
