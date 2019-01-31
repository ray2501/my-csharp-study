using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using pefnotes.Models;

namespace pefnotes.Services
{

    public class NoteContext : DbContext
    {
        private string connectionString;
        public DbSet<Note> Notes { get; set; }

        public NoteContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // For auto generated Created column
            modelBuilder.Entity<Note>()
                .Property(b => b.Created )
                .HasDefaultValueSql("now()");
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }


    public class PGNoteEFService : INoteService
    {
        private NoteContext _context;

        public PGNoteEFService(string connectionString)
        {
            this._context = new NoteContext(connectionString);
        }

        public IEnumerable<Note> GetAll()
        {
            return _context.Notes.ToList();
        }

        public Note GetNoteById(Guid id)
        {
            var note = _context.Notes.FirstOrDefault(t => t.Id == id);
            return note;
        }

        public void AddNote(Note note)
        {
            if (note != null)
            {
                _context.Notes.Add(note);
                _context.SaveChanges();
            }
        }

        public void UpdateNote(Note note)
        {
            if (note != null)
            {
                _context.Notes.Update(note);
                _context.SaveChanges();
            }
        }

        public void DeleteNoteById(Guid id)
        {
            Note note = GetNoteById(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
        }
    }
}
