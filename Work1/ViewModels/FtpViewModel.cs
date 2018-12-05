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
        public string FtpUri { get; set; } = "ftp://192.168.1.34:3721/";
        public string PrevAdress { get; set; }
        public FileDirectoryInfo SelectedDirectory { get; set; }
        
        string preveousDirectory { get; set; }
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
                    
                    MessageBox.Show(SelectedDirectory.ToString());
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
                    if (fdi.Type != DirectoryType.File)
                    {
                        string currentUri;
                        if (fdi.Type!=DirectoryType.Back)
                        {
                            currentUri = SelectedDirectory.Adress;
                        }
                        else
                        {
                            currentUri = SelectedDirectory.Adress;
                        }
                        FtpUri = currentUri;
                        sourceDerictories = FtpClient.LoadDerictory(currentUri, "", "");
                        Derictories = new ObservableCollection<FileDirectoryInfo>(sourceDerictories);
                    }
                    
                }));
            }
        }
        private NavigationCommand createConnection;
        public NavigationCommand CreateConnection
        {
            get
            {
                return createConnection ?? (createConnection = new NavigationCommand(obj =>
                {
                    try
                    {
                        ftpClient = new FtpClient(FtpUri, "", "");
                        sourceDerictories = FtpClient.LoadDerictory(FtpUri, "", "");
                        Derictories = new ObservableCollection<FileDirectoryInfo>(sourceDerictories);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error of ftp server", "Error");
                        return;
                    }
                    CreateConnection.CanCantinue = true;
                },
                obj =>
                {
                    return !string.IsNullOrEmpty(this.FtpUri);
                }));
            }
        }
        public string SearchQuery { get; set; }
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
        }
    }
}
