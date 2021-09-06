using coreProject.Entities;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Repositories
{
    public class DapperProductRepository
    {
        public List<Product> GetProducts()
        {
            using var connection = new SqlConnection("server=(localdb)\\mssqllocaldb; database=youtubeNetCore; integrated security=true");
            return connection.GetAll<Product>().ToList();
        }
    }
}
