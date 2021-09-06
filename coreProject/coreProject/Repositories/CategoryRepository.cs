using coreProject.Contexts;
using coreProject.Entities;
using coreProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Repositories
{
    //Kategori ile ilgili CRUD işlemleri burada yapılacak
    public class CategoryRepository:GenericRepository<Categories>,ICategoriesRepository
    {
        
    }
}
