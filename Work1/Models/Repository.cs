using S;
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

        public override string ToString()
        {
            return String.Format("{0} {1}",Name,Id);
        }
    }
}
