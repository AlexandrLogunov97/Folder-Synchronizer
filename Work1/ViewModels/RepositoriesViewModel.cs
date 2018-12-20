using S;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Work1.Models;
using Work1.Utils;

namespace Work1.ViewModels
{
    class RepositoriesViewModel : ViewModel, IFtpViewerResult
    {


        public ObservableCollection<Repository> Repositories { get; set; }

        #region Source and target path
        public string SourceFolderPath { get; set; }
        public string TargetFolderPath { get; set; }
        #endregion

        public string RepositoryName { get; set; }

        private TypeDerectory typeDerectory;

        public FtpUser SourceUser { get; set; }
        public FtpUser TargetUser { get; set; }
        #region Ftp uri's
        public string SourceFtpUri { get; set; }
        public string TargetFtpUri { get; set; }
        #endregion
        #region flags
        public bool SourceIsReadOnly { get; set; }
        public bool TargetIsReadOnly { get; set; }

        public bool SourceIsFS { get; set; }
        public bool TargetIsFS { get; set; }

        public bool SourceIsFTP { get; set; }
        public bool TargetIsFTP { get; set; }
        #endregion
        private RepositoryDbContext dbContext;

        private string SourceType;
        private string TargetType;

        #region Select repository
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
                        SourceType = "FS";
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
                        TargetType = "FS";
                        typeDerectory = TypeDerectory.Target;
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
                    SelectSourceFtpDerectory.CanCantinue=ViewModel.Get<FtpViewModel>().Connect(SourceFtpUri, SourceUser,this);
                    SourceType = "FTP";
                    typeDerectory = TypeDerectory.Source;
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
                    SelectTargetFtpDerectory.CanCantinue=ViewModel.Get<FtpViewModel>().Connect(TargetFtpUri, TargetUser,this);
                    TargetType = "FTP";
                    typeDerectory = TypeDerectory.Target;
                }));
            }
        }
        #endregion

        private Repository selectedRepository;
        public Repository SelectedRepository
        {
            get { return selectedRepository; }
            set
            {
                selectedRepository = value;
                if (selectedRepository != null)
                {
                    this.SetSelected();
                    SourceFolderPath = selectedRepository.Source;
                    TargetFolderPath = selectedRepository.Target;
                    RepositoryName = selectedRepository.Name;
                    SourceType = selectedRepository.SourceType;
                    TargetType = selectedRepository.TargetType;
                }
                this.RaisePropertyChanged("SelectedRepository");
            }
        }

        public void SetSelected()
        {
            if (SelectedRepository != null)
            {
                SourceIsFS = SelectedRepository.SourceType == "FS" ? true : false;
                TargetIsFS = SelectedRepository.TargetType == "FS" ? true : false;
                SourceIsFTP = SelectedRepository.SourceType == "FTP" ? true : false;
                TargetIsFTP = SelectedRepository.TargetType == "FTP" ? true : false;
                if (SourceIsFTP)
                    SourceFtpUri = SelectedRepository.Source;
                else
                    SourceFtpUri = string.Empty;
                if (TargetIsFTP)
                    TargetFtpUri = SelectedRepository.Target;
                else
                    TargetFtpUri = string.Empty;
            }
        }

        public string SearchQuery { get; set; }

        private NavigationCommand change;
        public NavigationCommand Change
        {
            get
            {
                return change ?? (change = new NavigationCommand(obj =>
                {
                    if (SelectedRepository != null)
                    {
                        SelectedRepository.Name = RepositoryName;
                        SelectedRepository.Source = SourceFolderPath;
                        SelectedRepository.Target = TargetFolderPath;
                        SelectedRepository.SourceType = SourceType;
                        SelectedRepository.TargetType = TargetType;
                        SelectedRepository.LastSync = SelectedRepository.LastSync;
                        dbContext.Entry(SelectedRepository).State = System.Data.Entity.EntityState.Modified;
                        int res = dbContext.SaveChanges();
                        Repositories = new ObservableCollection<Repository>(dbContext.Repositories.ToList());
                        int index = Repositories.IndexOf(SelectedRepository);
                        SelectedRepository = Repositories[index];
                    }
                },
                obj =>
                {
                        bool unique = false;
                        if (!string.IsNullOrEmpty(RepositoryName) && SelectedRepository!=null)
                        {
                            if (dbContext.Repositories.ToList().Exists(x => x.Name.Trim().ToLower() == RepositoryName.Trim().ToLower() && x.Id==SelectedRepository.Id))
                                unique = false;
                            else
                                unique = true;
                        }
                        bool notEqual = !string.IsNullOrEmpty(SourceFolderPath) && !string.IsNullOrEmpty(TargetFolderPath) ? !(SourceFolderPath == TargetFolderPath) : false;
                        return !string.IsNullOrEmpty(RepositoryName) && unique && notEqual;

                }));
            }
        }

        private List<Repository> repSource;

        private Command search;
        public Command Search
        {
            get
            {
                return search ?? (search = new Command(obj => {
                    var searchQuery = obj as string;
                    searchQuery = searchQuery.Trim();
                    var searchResult = repSource.Where(x => x.Name.Contains(searchQuery) || x.Name.StartsWith(searchQuery) || x.Name.EndsWith(searchQuery));
                    
                    Repositories = new ObservableCollection<Repository>(searchResult);
                    if (Repositories.Count > 0)
                        SelectedRepository = Repositories[0];
                }));
            }
        }

        private Command synchronize;
        public Command Synchronize
        {
            get
            {
                return synchronize ?? (synchronize = new Command(obj =>
                {
                    Sync sync = new Sync()
                    {
                        Source = SelectedRepository.SourceType == "FTP" ? TypeCnnection.Ftp : TypeCnnection.FileSystem,
                        Target = SelectedRepository.TargetType == "FTP" ? TypeCnnection.Ftp : TypeCnnection.FileSystem
                    };
                    string result = sync.Synchronize(SelectedRepository.Source, SelectedRepository.Target);

                    System.Windows.MessageBox.Show(result);
                },obj=> {
                    bool unique = false;
                    if (!string.IsNullOrEmpty(RepositoryName) && SelectedRepository != null)
                    {
                        if (dbContext.Repositories.ToList().Exists(x => x.Name.Trim().ToLower() == RepositoryName.Trim().ToLower() && x.Id == SelectedRepository.Id))
                            unique = false;
                        else
                            unique = true;
                    }
                    bool notEqual = !string.IsNullOrEmpty(SourceFolderPath) && !string.IsNullOrEmpty(TargetFolderPath) ? !(SourceFolderPath == TargetFolderPath) : false;
                    return !string.IsNullOrEmpty(RepositoryName)  && notEqual;
                }));
            }
        }

        private NavigationCommand delete;
        public NavigationCommand Delete
        {
            get
            {
                return delete ?? (delete = new NavigationCommand(obj =>
                {

                    try
                    {
                        dbContext.Entry(SelectedRepository).State = System.Data.Entity.EntityState.Deleted;
                        int status = dbContext.SaveChanges();

                        if (status == 1)
                        {

                            int index = Repositories.IndexOf(SelectedRepository);
                            Repositories.Remove(SelectedRepository);
                            if (index > 0)
                                SelectedRepository = Repositories[index - 1];
                            if (Repositories.Count == 0)
                            {
                                Delete.CanCantinue = true;
                            }
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                },
                obj =>
                {
                    return !(SelectedRepository == null);
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

        public RepositoriesViewModel()
        {
            dbContext = new RepositoryDbContext();
            repSource = dbContext.Repositories.ToList();
            SourceUser = new FtpUser();
            TargetUser = new FtpUser();
            this.Repositories = new ObservableCollection<Repository>(repSource);
        }
    }
}
