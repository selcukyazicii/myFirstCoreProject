using coreProject.Contexts;
using coreProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Repositories
{
    public class GenericRepository<Ta> where Ta:class,new()
    {
        public void Add(Ta t)
        {
            using var context = new YoutubeContext();
            context.Set<Ta>().Add(t);
            context.SaveChanges();
        }
        public void Delete(Ta t)
        {
            using var context = new YoutubeContext();
            context.Set<Ta>().Remove(t);
            context.SaveChanges();
        }
        public void Update(Ta t)
        {
            using var context = new YoutubeContext();
            context.Set<Ta>().Update(t);
            context.SaveChanges();
        }
        public List<Ta> GetAlll()
        {
            using var context = new YoutubeContext();
            return context.Set<Ta>().ToList(); //order by descending eklenecek
        }
        public Ta GetId(int id)
        {
            using var context = new YoutubeContext();
            return context.Set<Ta>().Find(id);
        }
    }
}
