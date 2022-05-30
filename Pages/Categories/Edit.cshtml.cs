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
    public class EditModel : PageModel
    {
        private readonly CategoryRepository _repository;
        private readonly NotebookContext _context;


        public EditModel(CategoryRepository repository, NotebookContext context)
        {
            _repository = repository;
            _context = context;
        }
        [BindProperty]
        public Category Category { get; set; }

        public void OnGet(int id)
        {
            Category = _repository.GetCategory(id);

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var categoryFromDb = _repository.GetCategory(Category.Id);

            var tryUpdate = await TryUpdateModelAsync<Category>(categoryFromDb, "category", c => c.Id, c => c.Title);

            if (tryUpdate)
            {
                _repository.UpdateCategory(categoryFromDb);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
