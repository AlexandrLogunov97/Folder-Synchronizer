using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Work1.Models
{
    class RepositoryDbContext:DbContext
    {
        public RepositoryDbContext() : base("Repositories")
        {
            MessageBox.Show(this.Database.Exists().ToString());
        }
        public DbSet<Repository> Repositories { get; set; }
    }
}
