using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBUFirstApi.Models
{
    public class NoteDto:BaseDto
    {
        public List<OneNoteDto> Notlar { get; set; }
        public int OgrId { get; set; }
    }

    public class OneNoteDto:BaseDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

    }
}
