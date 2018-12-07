using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work1.Utils
{
    class FileDirectoryInfo
    {
        string fileSize;
        string type;
        string name;
        string date;
        string adress;
        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = String.Format("{0}{1}/",value,name);
            }
        }
        public Uri Icon { get; set; }

        public string FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        public DirectoryType Type { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public FileDirectoryInfo() { }

        public FileDirectoryInfo(string fileSize, DirectoryType type, string name, string date, string adress)
        {
            FileSize = fileSize;
            Type = type;
            Name = name;
            Date = date;
            this.Adress = adress;
        }
        public override string ToString()
        {
            return "";
        }
    }
}
