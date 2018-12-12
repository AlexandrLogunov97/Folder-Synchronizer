using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work1.Utils
{
    class FileDirectoryInfo
    {
        public string FileSize { get; set; }
        public string Name { get;}
        public string DisplayName { get; set; }
        public string Date { get; set; }
        public DirectoryType Type { get; set; }
        private string adress;
        public string Adress { get; set; }
        public Uri Icon { get; set; }

        public FileDirectoryInfo() { }

        public FileDirectoryInfo(string fileSize, DirectoryType type, string name, string date, string adress)
        {
            FileSize = fileSize;
            Type = type;
            Name = name;
            Date = date;
            DisplayName = Name;
            this.Adress = adress;
        }
        public FileDirectoryInfo(string fileSize, DirectoryType type, string name, string date, string adress, string displayName)
        {
            FileSize = fileSize;
            Type = type;
            Name = name;
            Date = date;
            DisplayName = displayName;
            this.Adress = adress;
        }
        public override string ToString()
        {
            return "";
        }
    }
}
