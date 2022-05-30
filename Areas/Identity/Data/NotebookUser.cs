using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Notebook.Model;

namespace Notebook.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the NotebookUser class
    public class NotebookUser : IdentityUser
    {
        public List<Note> Notes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
