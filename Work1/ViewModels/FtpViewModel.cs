using S;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Work1.Utils;

namespace Work1.ViewModels
{
    class FtpViewModel : ViewModel
    {
        public string FtpUri { get; set; }
        public string PrevAdress { get; set; }
        public string NewDirectory { get; set; } = "new directory";
        public FileDirectoryInfo SelectedDirectory { get; set; }


        public string SearchQuery { get; set; }

        FtpClient ftpClient;
        public ObservableCollection<FileDirectoryInfo> Derictories { get; set; }
        private List<FileDirectoryInfo> sourceDerictories;

        private Command showSelected;
        public Command ShowSelected
        {
            get
            {
                return showSelected ?? (showSelected = new Command(obj =>
                {
                    
                    MessageBox.Show(SelectedDirectory.FileSize.ToString());
                }));
            }
        }
        private Command toDirectory;
        public Command ToDirectory
        {
            get
            {
                return toDirectory ?? (toDirectory = new Command(obj =>
                {
                    FileDirectoryInfo fdi = SelectedDirectory;
                    ftpClient.ToDerectory(SelectedDirectory);
                    sourceDerictories = ftpClient.LoadDerectory();
                    Derictories = new ObservableCollection<FileDirectoryInfo>(sourceDerictories);   
                }));
            }
        }
        private void reload()
        {
            sourceDerictories = ftpClient.LoadDerectory();
            Derictories = new ObservableCollection<FileDirectoryInfo>(sourceDerictories);
        }
        private Command refresh;
        public Command Refresh
        {
            get
            {
                return refresh ?? (refresh = new Command(obj =>
                {
                    try
                    {
                        reload();
                    }
                    catch (Exception) { }
                }));
            }
        }
        private Command select;
        public Command Select
        {
            get
            {
                return select ?? (select = new Command(obj =>
                {
                    ViewModel.Get<SelectDerectoryViewModel>().SetSelectedPath(SelectedDirectory.Adress);
                },obj=> {
                    return SelectedDirectory!=null && SelectedDirectory?.Type==DirectoryType.Folder;
                }));
            }
        }
        public bool Connect(string ftpUri ,FtpUser user)
        {
            try
            {
                
                ftpClient = new FtpClient(ftpUri, user);
                FtpUri = ftpUri;
                sourceDerictories = ftpClient.LoadDerectory();
                Derictories = new ObservableCollection<FileDirectoryInfo>(sourceDerictories);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private NavigationCommand create;
        public NavigationCommand Create
        {
            get
            {
                return create ?? (create = new NavigationCommand(obj =>
                {
                    try
                    {
                        ftpClient.MakeDirectory(NewDirectory);
                        reload();
                        Create.CanCantinue = true;
                    }
                    catch(Exception ex)
                    {
                        Create.CanCantinue = false;
                    }
                },
                obj =>
                {
                    return !string.IsNullOrEmpty(this.FtpUri);
                }));
            }
        }

        private Command selectRenamedDerectory;
        public Command SelectRenamedDerectory
        {
            get
            {
                return selectRenamedDerectory ?? (selectRenamedDerectory = new Command(obj =>
                {
                    NewDirectory = SelectedDirectory.Name;
                },
                obj =>
                {
                    return SelectedDirectory!= null && SelectedDirectory?.Type==DirectoryType.Folder;
                }));
            }
        }

        private NavigationCommand rename;
        public NavigationCommand Rename
        {
            get
            {
                return rename ?? (rename = new NavigationCommand(obj =>
                {
                    try
                    {
                        ftpClient.Rename(SelectedDirectory.Name,NewDirectory);
                        reload();
                        Rename.CanCantinue = true;
                    }
                    catch (Exception ex)
                    {
                        Rename.CanCantinue = false;
                    }
                },
                obj =>
                {
                    return !string.IsNullOrEmpty(this.FtpUri);
                }));
            }
        }
        private Command search;
        public Command Search
        {
            get
            {
                return search ?? (search=new Command(obj=> {
                    var searchQuery = obj as string;
                    searchQuery = searchQuery.Trim();
                    SearchQuery = searchQuery;
                    var searchResult = sourceDerictories.Where(x => x.Name.Contains(searchQuery) || x.Name.StartsWith(searchQuery) || x.Name.EndsWith(searchQuery) || x.Type==DirectoryType.Back);
                    Derictories = new ObservableCollection<FileDirectoryInfo>(searchResult);
                }));
            }
        }
        public FtpViewModel()
        {
            FtpUri = "ftp://192.168.1.34:3721/";
        }
    }
}
