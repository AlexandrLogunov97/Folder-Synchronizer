using S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Work1.ViewModels
{
    class FileSystemViewModel:ViewModel
    {
        public string FolderPath { get; set; }
        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }
        private Command selectDirectory;
        public Command SelectDirectory
        {
            get
            {
                return selectDirectory ?? (selectDirectory=new Command(obj=> {
                    FolderBrowserDialog dialog=new FolderBrowserDialog();
                    if(dialog.ShowDialog()==DialogResult.OK)
                    {
                        FolderPath = dialog.SelectedPath;
                    }
                    MessageBox.Show((GetDirectorySize(FolderPath)/1000).ToString());
                }));
            }
        }
        public FileSystemViewModel()
        {

        }
    }
}
