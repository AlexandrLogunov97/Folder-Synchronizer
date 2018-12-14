﻿using S;
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

        private object toCopy;

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
        public byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        private void CopyFromFSToFtp(string fsPath= "c:/Users/acer/Desktop/SourceFolder/", string ftpPath= "ftp://192.168.1.34:3721/Truba/")
        {
            FtpClient ftp = new FtpClient(ftpPath, new FtpUser());
            DirectoryInfo directory = new DirectoryInfo(fsPath);
            directory.GetDirectories().ToList().ForEach(x =>
            {
                if (!ftp.IsExist(x.Name))
                {
                    ftp.MakeDirectory(x.Name);
                }
                else
                {
                    ftp.Rename(x.Name, String.Format("{0}-copy({1})", x.Name, DateTime.Now));
                    ftp.MakeDirectory(x.Name);
                }
            });
        }
        //private void CopyFromFtpToFtp(string source, string destination)
        //{
        //    FtpClient ftpSource = new FtpClient(source, new FtpUser());
        //    FtpClient ftpDestination = new FtpClient(destination, new FtpUser());
        //    //if (!destination.Is)
        //    //{
        //    //    destination.();
        //    //}

        //    // Copy all files.
            
        //    List<FileDirectoryInfo> files = ftpSource.GetFiles();
        //    foreach (FileDirectoryInfo file in files)
        //    {
        //        if (!File.Exists(Path.Combine(destination.FullName,
        //            file.Name)))
        //            file.CopyTo(Path.Combine(destination.FullName,
        //                file.Name));
        //    }

        //    // Process subdirectories.
        //    DirectoryInfo[] dirs = source.GetDirectories();
        //    foreach (DirectoryInfo dir in dirs)
        //    {
        //        // Get destination directory.
        //        string destinationDir = Path.Combine(destination.FullName, dir.Name);

        //        // Call CopyDirectory() recursively.
        //        CopyFromFSToFS(dir, new DirectoryInfo(destinationDir));
        //    }
        //}
        private void CopyFromFSToFS(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!File.Exists(Path.Combine(destination.FullName,
                    file.Name)))
                file.CopyTo(Path.Combine(destination.FullName,
                    file.Name));
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyFromFSToFS(dir, new DirectoryInfo(destinationDir));
            }
            //DirectoryInfo directory = new DirectoryInfo(source);
            //directory.GetDirectories().ToList().ForEach(x =>
            //{
            //    MessageBox.Show(source + " " + target);
            //    if (!Directory.Exists(String.Format("{0}/{1}", target, x.Name)))
            //    {
            //        Directory.CreateDirectory(String.Format("{0}/{1}", target, x.Name));
            //    }
            //    directory.GetFiles().ToList().ForEach(y =>
            //    {
            //        if (!File.Exists(String.Format("{0}/{1}", target, y.Name)))
            //        {
            //            File.Copy(String.Format("{0}/{1}", source, y.Name), String.Format("{0}/{1}", target, y.Name));
            //        }
            //    });
            //    CopyFromFSToFS(String.Format("{0}/{1}", source, x.Name), String.Format("{0}/{1}", target, x.Name));
            //});
        }
        private void CopyFromFtpToFS()
        {

        }
        private Command create;
        public Command Create
        {
            get
            {
                return create ?? (create = new Command(obj =>
                {

                    //CopyFromFSToFtp();
                    CopyFromFSToFS(new DirectoryInfo(SourceFolderPath),new DirectoryInfo(TargetFolderPath));
                    //FtpClient ftp = new FtpClient("ftp://192.168.1.34:3721/Truba/", new FtpUser());
                    //ftp.UploadFile("c:/Users/acer/Desktop/SourceFolder/Test.txt", "Test.txt");

                    //sc.Add(SourceFolderPath);
                    //Clipboard.SetFileDropList(sc);
                    //byte[] buffer=new byte[1024];
                    ////buffer = ObjectToByteArray(Clipboard.GetData(n));
                    //using (FileStream fs = new FileStream(TargetFolderPath, FileMode.OpenOrCreate))
                    //{
                    //    fs.WriteAsync(buffer, 0, buffer.Length);
                    //}
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
            this.SourceFtpUri= "ftp://192.168.1.100:3721/";
            this.TargetFtpUri= "ftp://192.168.1.100:3721/";
            this.SourceUser = new FtpUser() { };
            this.TargetUser = new FtpUser() { };
        }
    }
}
