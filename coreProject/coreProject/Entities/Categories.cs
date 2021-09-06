using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace coreProject.Entities
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }
    }
}
