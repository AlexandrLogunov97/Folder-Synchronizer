using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using S;
using Work1.Models;

namespace Work1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = String.Format("{0}\\{1}", System.IO.Directory.GetParent(executable).Parent.Parent.FullName,"App_data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }
    }
}
