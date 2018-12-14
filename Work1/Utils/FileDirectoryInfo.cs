using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Work1.Utils
{
    class FileDirectoryInfo
    {
        public string FileSize { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Date { get; set; }
        public string FileType { get; set; }
        public DirectoryType Type { get; set; }
        private string adress;
        public string Adress { get; set; }
        public Uri Icon { get; set; }

        public FileDirectoryInfo() { }
        private string getFileType(string file)
        {

            var type = (new StringBuilder(new string(file.Reverse().ToArray()))).ToString();
            int length = type.IndexOf('.', 1);
            var result = new StringBuilder(type);
            //MessageBox.Show(length.ToString()+" "+ file+" "+type);
            if (length > 0)
            {
                result.Remove(0, length);
                return new string(result.ToString().Reverse().ToArray());
            }
            return string.Empty;
        }
        public FileDirectoryInfo(string fileSize, DirectoryType type, string name, string date, string adress)
        {
            FileSize = fileSize;
            Type = type;
            Name = name;
            Date = date;
            DisplayName = Name;
            //if (type == DirectoryType.File)
            //{
                
            //    DisplayName +=" "+ getFileType(name);
            //    MessageBox.Show(DisplayName);
            //    //MessageBox.Show(getFileType(name));
            //}
            this.Adress = adress;
        }
        public FileDirectoryInfo(string fileSize, DirectoryType type, string name, string date, string adress, string displayName)
        {
            FileSize = fileSize;
            Type = type;
            Name = name;
            Date = date;
            DisplayName = displayName;
            //MessageBox.Show(getFileType(name));
            //if (type == DirectoryType.File)
            //{
            //    FileType = getFileType(name);
            //    MessageBox.Show(getFileType(name));
            //}
            this.Adress = adress;
        }
        public override string ToString()
        {
            return "";
        }
    }
}
