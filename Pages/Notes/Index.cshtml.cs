using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notebook.Areas.Identity.Data;
using Notebook.Model;
using Notebook.Repositories;
using Notebook.Services;

namespace Notebook.Pages.Notes
{
    
    public class IndexModel : PageModel
    {
        private readonly NoteRepository _repository;
        private readonly CategoryRepository _catRepository;
        private readonly UserService _userService;

        public List<Note> Notes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchInputTitle { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }
        [BindProperty]
        public Category Category { get; set; }

        public IndexModel(NoteRepository repository, UserService userService, CategoryRepository catRepository)
        {
            _repository = repository;
            _userService = userService;
            _catRepository = catRepository;
        }

        public void OnGet(int id)
        {
            var categories = _repository.GetNotesCategory();
            Categories = new SelectList(categories);

            if (id != 0)
            {
                var category = _catRepository.GetCategory(id);
                SelectedCategory = category.Title;
            }
            
                Notes = _repository.GetNotes(SearchInputTitle, SelectedCategory);
                //var userId = _userService.GetUserId();
                //Notes = _repository.GetNotesByUserId(userId);
            
        }

        public IActionResult OnPostDelete(int id)
        {
            var note = _repository.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }

            _repository.DeleteNote(id);
            return RedirectToPage("Index");
        }
    }
}
