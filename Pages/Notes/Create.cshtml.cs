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
    public class CreateModel : CategorySelectModel
    {
        private readonly NoteRepository _repository;
        private readonly NotebookContext _context;

        [BindProperty]
        public Note Note { get; set; }

        public CreateModel(NoteRepository repository, NotebookContext context)
        {
            _repository = repository;
            _context = context;
        }

        public IActionResult OnGet()
        {
            GenerateCategoryDropDown(_context);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyNote = new Note();
            var tryUpdate = await TryUpdateModelAsync<Note>(emptyNote, "note", n => n.Id, n => n.CategoryId, n => n.Title, n => n.Text);

            if (tryUpdate)
            {
                _repository.CreateNote(emptyNote);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
