﻿using S;
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

namespace Work1.Views.FtpViewer
{
    /// <summary>
    /// Логика взаимодействия для CreateDerectory.xaml
    /// </summary>
    public partial class CreateDerectory : Page
    {
        public CreateDerectory()
        {
            InitializeComponent();
            this.DataContext = ViewModel.Get<FtpViewer>();
        }
    }
}
