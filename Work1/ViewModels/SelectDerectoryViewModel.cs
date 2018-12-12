using S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Work1.Utils;

namespace Work1.ViewModels
{
    enum TypeDerectory
    {
        Source,
        Target
    }
    class SelectDerectoryViewModel : ViewModel
    {
        public string SourceFolderPath { get; set; }
        public string TargetFolderPath { get; set; }
        
        public FtpUser SourceUser { get; set; }
        public FtpUser TargetUser { get; set; }

        public string SourceFtpUri { get; set; }
        public string TargetFtpUri { get; set; }

        private TypeDerectory typeDerectory;

        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        private Command selectSourceDirectory;
        public Command SelectSourceDirectory
        {
            get
            {
                return selectSourceDirectory ?? (selectSourceDirectory = new Command(obj =>
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        SourceFolderPath = dialog.SelectedPath;
                        typeDerectory = TypeDerectory.Source;
                    }
                }));
            }
        }

        private Command selectTargetDirectory;
        public Command SelectTargetDirectory
        {
            get
            {
                return selectTargetDirectory ?? (selectTargetDirectory = new Command(obj =>
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        TargetFolderPath = dialog.SelectedPath;
                        typeDerectory = TypeDerectory.Target;
                    }
                }));
            }
        }

        private Command selectSourceFtpDerectory;
        public Command SelectSourceFtpDerectory
        {
            get
            {
                return selectSourceFtpDerectory ?? (selectSourceFtpDerectory = new Command(obj =>
                {
                    ViewModel.Get<FtpViewModel>().Connect(SourceFtpUri,SourceUser);
                    typeDerectory = TypeDerectory.Source;
                }));
            }
        }
        private Command selectTargetFtpDerectory;
        public Command SelectTargetFtpDerectory
        {
            get
            {
                return selectTargetFtpDerectory ?? (selectTargetFtpDerectory = new Command(obj =>
                {
                    ViewModel.Get<FtpViewModel>().Connect(TargetFtpUri,TargetUser);
                    typeDerectory = TypeDerectory.Target;
                }));
            }
        }
        public void SetSelectedPath(string path)
        {
            switch (typeDerectory)
            {
                case TypeDerectory.Source:
                    {
                        SourceFolderPath = path;
                        break;
                    }
                case TypeDerectory.Target:
                    {
                        TargetFolderPath = path;
                        break;
                    }
            }
        }
        public SelectDerectoryViewModel()
        {
            this.SourceFtpUri= "ftp://192.168.1.34:3721/";
            this.TargetFtpUri= "ftp://192.168.1.34:3721/";
            this.SourceUser = new FtpUser() { };
            this.TargetUser = new FtpUser() { };
        }
    }
}
