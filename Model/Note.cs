using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notebook.Model
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Text { get; set; }
        public string NotebookUserId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
