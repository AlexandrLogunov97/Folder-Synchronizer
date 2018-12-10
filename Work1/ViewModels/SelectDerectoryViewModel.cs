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
    class SelectDerectoryViewModel : ViewModel
    {
        public string SourceFolderPath { get; set; }
        public string TargetFolderPath { get; set; }
        private string type { get; set; }
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
                return selectDirectory ?? (selectDirectory = new Command(obj =>
                {
                    string folder = obj as string;
                    FolderBrowserDialog dialog = new FolderBrowserDialog();

                    if (folder.Equals("source"))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            SourceFolderPath = dialog.SelectedPath;
                            type = "source";
                        }
                    }
                    else if (folder.Equals("target"))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            TargetFolderPath = dialog.SelectedPath;
                            type = "target";
                        }
                    }
                }));
            }
        }


        private Command selectFtpDerectory;
        public Command SelectFtpDerectory
        {
            get
            {
                return selectFtpDerectory ?? (selectFtpDerectory = new Command(obj =>
                {
                    string folder = obj as string;
                    ViewModel.Get<FtpViewModel>().Connect();
                    if (folder.Equals("source"))
                    {
                        type = "source";
                    }
                    else if (folder.Equals("target"))
                    {
                        type = "target";
                    }

                }));
            }
        }
        public void SetSelectedPath(string path)
        {
            switch (type)
            {
                case "source":
                    {
                        SourceFolderPath = path;
                        break;
                    }
                case "target":
                    {
                        TargetFolderPath = path;
                        break;
                    }
            }
        }
        public SelectDerectoryViewModel()
        {

        }
    }
}
