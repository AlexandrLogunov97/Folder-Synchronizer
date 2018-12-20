using S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Work1.Utils;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using Work1.Models;

namespace Work1.ViewModels
{
    enum TypeDerectory
    {
        Source,
        Target
    }
    enum TypeCnnection
    {
        Ftp=1,
        FileSystem=2
    }
    class SelectDerectoryViewModel : ViewModel,IFtpViewerResult
    {
        public string SourceFolderPath { get; set; }
        public string TargetFolderPath { get; set; }
        
        public string RepositoryName { get; set; }

        public FtpUser SourceUser { get; set; }
        public FtpUser TargetUser { get; set; }

        public string SourceFtpUri { get; set; }
        public string TargetFtpUri { get; set; }

        private TypeCnnection source;
        private TypeCnnection target;

        public bool SynchronizeImmediately { get; set; }

        RepositoryDbContext dbContext;

        private TypeDerectory typeDirectory;

        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }
        #region Select directory
        private Command selectSourceDirectory;
        public Command SelectSourceDirectory
        {
            get
            {
                return selectSourceDirectory ?? (selectSourceDirectory = new Command(obj =>
                {
                    try
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            SourceFolderPath = dialog.SelectedPath;
                            source = TypeCnnection.FileSystem;
                            typeDirectory = TypeDerectory.Source;
                        }
                    }
                    catch(Exception)
                    {

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
                    try
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            TargetFolderPath = dialog.SelectedPath;
                            target = TypeCnnection.FileSystem;
                            typeDirectory = TypeDerectory.Target;
                        }
                    }
                    catch(Exception ex)
                    {
                    }
                }));
            }
        }

        private NavigationCommand selectSourceFtpDirectory;
        public NavigationCommand SelectSourceFtpDerectory
        {
            get
            {
                return selectSourceFtpDirectory ?? (selectSourceFtpDirectory = new NavigationCommand(obj =>
                {
                    try
                    {
                        
                        source = TypeCnnection.Ftp;
                        typeDirectory = TypeDerectory.Source;
                        SelectSourceFtpDerectory.CanCantinue = ViewModel.Get<FtpViewModel>().Connect(SourceFtpUri, SourceUser, this);
 
                    }
                    catch(Exception ex)
                    {
                        SelectSourceFtpDerectory.CanCantinue = false;
                    }
                }));
            }
        }

        private NavigationCommand selectTargetFtpDerectory;
        public NavigationCommand SelectTargetFtpDerectory
        {
            get
            {
                return selectTargetFtpDerectory ?? (selectTargetFtpDerectory = new NavigationCommand(obj =>
                {
                    try
                    {
                        
                        target = TypeCnnection.Ftp;
                        typeDirectory = TypeDerectory.Target;
                        SelectTargetFtpDerectory.CanCantinue = ViewModel.Get<FtpViewModel>().Connect(TargetFtpUri, TargetUser, this);
                    }
                    catch(Exception)
                    {
                        SelectTargetFtpDerectory.CanCantinue = false;
                    }
                }));
            }
        }
        #endregion

        private Command create;
        public Command Create
        {
            get
            {
                return create ?? (create = new Command(obj =>
                {
                    var createdRepository = new Models.Repository() {
                        Name = RepositoryName,
                        Source = SourceFolderPath,
                        Target = TargetFolderPath,
                        SourceType =source==TypeCnnection.Ftp?"FTP":"FS",
                        TargetType = target == TypeCnnection.Ftp ? "FTP" : "FS",
                        LastSync = DateTime.Now };
                    dbContext.Repositories.Add(createdRepository);
                    
                    ViewModel.Get<RepositoriesViewModel>().Repositories.Add(createdRepository);
                    if(SynchronizeImmediately)
                    {
                        Sync sync = new Sync(this.source, this.target);
                        sync.Synchronize(SourceFolderPath, TargetFolderPath);
                    }
                    dbContext.SaveChanges();
                },
                obj =>
                {
                    bool unique=false;
                    if (!string.IsNullOrEmpty(RepositoryName))
                    {
                        if (dbContext.Repositories.ToList().Exists(x => x.Name.Trim().ToLower() == RepositoryName.Trim().ToLower()))
                            unique = false;
                        else
                            unique = true;
                    }
                    bool notEqual=!string.IsNullOrEmpty(SourceFolderPath) && !string.IsNullOrEmpty(TargetFolderPath)? !(SourceFolderPath == TargetFolderPath) : false;
                    return !string.IsNullOrEmpty(RepositoryName) && unique && notEqual;
                }
                ));
            }
        }

        public void SetSelectedPath(string path)
        {

            switch (typeDirectory)
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
            this.RaisePropertyChanged("SourceFolderPath");
            this.RaisePropertyChanged("TargetFolderPath");
        }
        public SelectDerectoryViewModel()
        {
            dbContext = new RepositoryDbContext();

            this.SourceFtpUri = "ftp://192.168.1.34:3721/";
            this.TargetFtpUri= "ftp://192.168.1.34:3721/";
            this.SourceUser = new FtpUser() { User="user",Password="password"};
            this.TargetUser = new FtpUser() { User = "user", Password = "password" };
        }
    }
}
