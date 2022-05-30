using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notebook.Areas.Identity.Data;
using Notebook.Model;

namespace Notebook.Repositories
{
    public class CategoryRepository
    {
        private readonly NotebookContext _context;

        public CategoryRepository(NotebookContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.Include(c => c.Notes).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
