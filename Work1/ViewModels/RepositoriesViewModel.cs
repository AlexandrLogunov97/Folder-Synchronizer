using S;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Work1.Models;

namespace Work1.ViewModels
{
    class RepositoriesViewModel:ViewModel
    {
        public ObservableCollection<Repository> Repositories { get; set; }
        public Repository SelectedRepository { get; set; }
        private Command show;
        public Command Show
        {
            get {
                return show ?? (show = new Command(obj =>
                {
                    MessageBox.Show(SelectedRepository.Name);
                }));
            }
        }
        public RepositoriesViewModel()
        {
            this.Repositories = new ObservableCollection<Repository>(new List<Repository>()
            {
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/Target/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now },
                new Repository() { Source="c:/folder/",Target="c:/folder/",SourceType="FS", TargetType="FS",LastSync=DateTime.Now }
            });
        }
    }
}
