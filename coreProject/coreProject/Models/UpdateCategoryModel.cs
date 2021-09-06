using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Models
{
    public class UpdateCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(40,ErrorMessage ="Max 40 char")]
        [MinLength(1,ErrorMessage ="Min 1 char")]
        public string Name { get; set; }

    }
}
