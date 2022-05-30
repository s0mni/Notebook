using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notebook.Areas.Identity.Data;
using Notebook.Model;
using Notebook.Repositories;

namespace Notebook.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly CategoryRepository _repository;
        private readonly NotebookContext _context;

        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(CategoryRepository repository, NotebookContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyCategory = new Category();
            var tryUpdate = await TryUpdateModelAsync<Category>(emptyCategory, "category", c => c.Id, c => c.Title);

            if (tryUpdate)
            {
                _repository.CreateCategory(emptyCategory);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
