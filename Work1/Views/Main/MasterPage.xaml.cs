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

namespace Work1.Views.Main
{
    /// <summary>
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        public MasterPage()
        {
            InitializeComponent();
            this.Repositories.DataContext = ViewModel.Get<RepositoriesViewModel>();
            Button a=new Button();
            UIElement b = (UIElement)a;
        }

        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("vasv");
        }
    }
}
