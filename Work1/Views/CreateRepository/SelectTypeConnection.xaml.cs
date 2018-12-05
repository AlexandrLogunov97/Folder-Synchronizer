using S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Work1.ViewModels;

namespace Work1.Views.CreateRepository
{
    /// <summary>
    /// Логика взаимодействия для SelectTypeConnection.xaml
    /// </summary>
    public partial class SelectTypeConnection : Page
    {
        public SelectTypeConnection()
        {
            InitializeComponent();
            this.FSPanel.DataContext= ViewModel.Get<FileSystemViewModel>();
            this.FtpPanel.DataContext = ViewModel.Get<FtpViewModel>();
        }
    }
}
