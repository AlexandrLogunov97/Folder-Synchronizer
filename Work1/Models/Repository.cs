using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work1.Models
{
    class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string SourceType { get; set; }
        public string TargetType { get; set; }
        public DateTime LastSync { get; set; }
        static Random random = new Random();
        static string[] names = new string[] { "Name 1", "Name 2", "Name 3", "Name 4", "Name 5", "Name 6", "Name 7", "Name 8" };
        public Repository()
        {
            Name = names[random.Next(0, names.Length - 1)];
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
