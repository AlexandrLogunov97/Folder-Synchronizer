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
        public RepositoryDbContext()
        {
            //this.Database.Connection. = "System.Data.SQLite";
           // var connection = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            this.Database.Connection.ConnectionString = "Data Source=E:/Projects/CSharp/Works/Folder-Synchronizer/Work1/App_data/Repositories.db;";
        }
        public DbSet<Repository> Repositories { get; set; }
    }
}
