using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notebook.Areas.Identity.Data;
using Notebook.Model;

namespace Notebook.Repositories
{
    public class NoteRepository
    {
        private readonly NotebookContext _context;

        public NoteRepository(NotebookContext context)
        {
            _context = context;
        }

        public List<Note> GetNotes()
        {
            return _context.Notes.Include(n => n.Category).ToList();
        }

        public Note GetNote(int id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }

        public List<Note> GetNotesByUserId(string id)
        {
            return _context.Notes.Include(n => n.Category).Where(note => note.NotebookUserId == id).ToList();
        }

        public List<Note> GetNotes(string text = null, string categoryTitle = null)
        {
            var notes = _context.Notes.Include(n => n.Category).Select(n => n);
            if (!string.IsNullOrEmpty(text))
            {
                notes = notes.Where(n => n.Title.Contains(text) || n.Text.Contains(text));
            }

            if (!string.IsNullOrEmpty(categoryTitle))
            {
                notes = notes.Where(n => n.Category.Title.Contains(categoryTitle));
            }
            return notes.ToList();
        }

        public void CreateNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void DeleteNote(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
        }

        public void UpdateNote(Note note)
        {
            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges(); 
        }

        public List<string> GetNotesCategory()
        {
            return _context.Notes.Include(n => n.Category).Select(note => note.Category.Title).Distinct().ToList();
        }
    }
}
