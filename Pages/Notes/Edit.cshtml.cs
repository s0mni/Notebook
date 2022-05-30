using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notebook.Areas.Identity.Data;
using Notebook.Model;
using Notebook.Repositories;

namespace Notebook.Pages.Notes
{
    public class EditModel : CategorySelectModel
    {
        private readonly NoteRepository _repository;
        private readonly NotebookContext _context;


        public EditModel(NoteRepository repository, NotebookContext context)
        {
            _repository = repository;
            _context = context;
        }
        [BindProperty]
        public Note Note { get; set; }

        public IActionResult OnGet(int id)
        {
            Note = _repository.GetNote(id);
            GenerateCategoryDropDown(_context);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var noteFromDb = _repository.GetNote(Note.Id);

            var tryUpdate = await TryUpdateModelAsync<Note>(noteFromDb, "note", n => n.Id, n => n.CategoryId, n => n.Title, n => n.Text);

            if (tryUpdate)
            {
                _repository.UpdateNote(noteFromDb);
                return RedirectToPage("Index");
            }
            GenerateCategoryDropDown(_context, noteFromDb.CategoryId);
            return Page();
        }
    }
}
