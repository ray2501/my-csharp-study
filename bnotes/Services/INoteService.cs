using System;
using System.Collections.Generic;
using bnotes.Data;

namespace bnotes.Services
{
    public interface INoteService  
    {          
        IEnumerable<Note> GetAll();
        Note GetNoteById(Guid id);
        void AddNote(Note note);  
        void UpdateNote(Note note);  
        void DeleteNoteById(Guid id);
    }
}
