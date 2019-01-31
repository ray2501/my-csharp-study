using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using pnotes.Data;

namespace pnotes.Services
{
    public interface INoteService  
    {          
        Task<IEnumerable<Note>> GetAllAsync();
        Task<Note> GetNoteByIdAsync(Guid id);
        Task AddNoteAsync(Note note);  
        Task UpdateNoteAsync(Note note);  
        Task DeleteNoteByIdAsync(Guid id);
    }
}
