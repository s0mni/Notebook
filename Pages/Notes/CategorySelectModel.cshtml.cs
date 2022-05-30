using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notebook.Areas.Identity.Data;

namespace Notebook.Pages.Notes
{
    public class CategorySelectModel : PageModel
    {
        public SelectList CategoryList { get; set; }

        public void GenerateCategoryDropDown(NotebookContext context, object selectedCategory = null)
        {
            var categoryQuery = context.Categories.OrderBy(c => c.Title);
            CategoryList = new SelectList(categoryQuery, "Id", "Title", selectedCategory);
        }
    }
}
