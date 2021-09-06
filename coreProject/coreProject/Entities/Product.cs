using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Entities
{
    public class Product
    {
        [Key]
        //[Dapper.Contrib.Extensions.ExplicitKey]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }

        //public bool Equals([AllowNull] Product other)
        //{
        //    return Id == other.Id && Name = other.Name && Photo == other.Photo && Price = other.Price;
        //}
    }
}
