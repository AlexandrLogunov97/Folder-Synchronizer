using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace Work1.Models
{
    class RepositoryDbContext:DbContext
    {
        public RepositoryDbContext():base("Repositories")
        {
            //this.Database.Connection. = "System.Data.SQLite";
            // var connection = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            //var con = ConfigurationManager.ConnectionStrings["Repositories"].;


        }
        public DbSet<Repository> Repositories { get; set; }
    }
}
