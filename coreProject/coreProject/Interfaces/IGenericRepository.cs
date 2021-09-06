using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Interfaces
{
    public interface IGenericRepository<T> where T:class,new()
    {
        public void Add(T t);
        public void Delete(T t);
        public void Update(T t);
        public List<T> GetAlll();
        public T GetId(int id);

    }
}
