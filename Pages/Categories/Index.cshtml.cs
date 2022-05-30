using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notebook.Model;
using Notebook.Repositories;

namespace Notebook.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CategoryRepository _repository;
        public List<Category> Categories { get; set; }

        public IndexModel(CategoryRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
            Categories = _repository.GetCategories();
        }

        public IActionResult OnPostDelete(int id)
        {
            var category = _repository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            _repository.DeleteCategory(id);
            return RedirectToPage("Index");
        }
    }
}
