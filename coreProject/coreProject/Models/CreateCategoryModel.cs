using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Models
{
    public class CreateCategoryModel
    {
        [Required(ErrorMessage ="Bu alan boş geçilmez !")]
        public string Name { get; set; }

    }
}
