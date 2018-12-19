using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Work1.ViewModels;

namespace Work1.Utils
{
    class Sync
    {
        string pathToFileBuffer;
        private TypeCnnection source;
        private TypeCnnection target;
        public TypeCnnection Source
        {
            get { return source; }
            set { source = value; }
        }
        public TypeCnnection Target
        {
            get { return target; }
            set { target = value; }
        }
        public Sync()
        {
            pathToFileBuffer = String.Format("{0}/{1}", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName, "FileBuffer");
            pathToFileBuffer = pathToFileBuffer.Replace('\\', '/');
        }
        public Sync(TypeCnnection source, TypeCnnection target)
        {
            this.source = source;
            this.target = target;
            pathToFileBuffer = String.Format("{0}/{1}", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName, "FileBuffer");
            pathToFileBuffer = pathToFileBuffer.Replace('\\', '/');
        }

        private void CopyFromFSToFtp(string source, string destination)
        {
            source = source.Replace('\\', '/');
            DirectoryInfo sourceDir = new DirectoryInfo(source);
            FtpClient ftpDestination = new FtpClient(destination, new FtpUser());

            FileInfo[] files = sourceDir.GetFiles();
            foreach (FileInfo file in files)
            {

                if (!ftpDestination.IsExist(Path.Combine(destination, file.Name)))
                {
                    ftpDestination.UploadFile(String.Format("{0}/{1}",source,file.Name), file.Name);
                }
                else
                {
                    ftpDestination.DeleteFile( file.Name);
                    ftpDestination.UploadFile(source, file.Name);
                }
            }
            DirectoryInfo[] dirs = sourceDir.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if (!ftpDestination.IsExist(dir.Name))
                {
                    ftpDestination.MakeDirectory(dir.Name);
                }
                else
                {
                    ftpDestination.RemoveDirectory(dir.Name);
                    ftpDestination.MakeDirectory(dir.Name);
                }
                string destinationDir = Path.Combine(destination, dir.Name);
                CopyFromFSToFtp(Path.Combine(source, dir.Name), destinationDir);
            }
        }
        private void CopyFromFtpToFtp(string source, string destination)
        {
            FtpClient ftpSource = new FtpClient(source, new FtpUser());
            FtpClient ftpDestination = new FtpClient(destination, new FtpUser());

            List<FileDirectoryInfo> files = ftpSource.GetFiles();
            foreach (FileDirectoryInfo file in files)
            {
                if (!ftpSource.IsExist(Path.Combine(destination, file.Name)))
                {
                    ftpSource.DownloadFile(file.Name, string.Format("{1}/{0}", file.Name, pathToFileBuffer));
                    ftpDestination.UploadFile(string.Format("{1}/{0}", file.Name, pathToFileBuffer), file.Name);
                }
            }

            List<FileDirectoryInfo> dirs = ftpSource.GetFolders();
            foreach (FileDirectoryInfo dir in dirs)
            {
                if (!ftpDestination.IsExist(dir.Name))
                {
                    ftpDestination.MakeDirectory(dir.Name);
                }
                else
                {
                    ftpDestination.RemoveDirectory(dir.Name);
                    ftpDestination.MakeDirectory(dir.Name);
                }
                string destinationDir = Path.Combine(destination, dir.Name);
                CopyFromFtpToFtp(Path.Combine(source, dir.Name), destinationDir);
            }
        }
        private void CopyFromFSToFS(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!File.Exists(Path.Combine(destination.FullName,
                    file.Name)))
                    file.CopyTo(Path.Combine(destination.FullName,
                        file.Name));
            }

            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                string destinationDir = Path.Combine(destination.FullName, dir.Name);
                CopyFromFSToFS(dir, new DirectoryInfo(destinationDir));
            }
        }
        private void CopyFromFtpToFS(string source, string destination)
        {
            destination = destination.Replace('\\', '/');
            FtpClient ftpSource = new FtpClient(source, new FtpUser());
            DirectoryInfo destDir = new DirectoryInfo(destination);

            List<FileDirectoryInfo> files = ftpSource.GetFiles();
            foreach (FileDirectoryInfo file in files)
            {
                
                string filePath= Path.Combine(destination, file.Name);
                filePath = filePath.Replace('\\', '/');
                if (!File.Exists(filePath))
                {
                    ftpSource.DownloadFile(file.Name, filePath);
                }
            }
            List<FileDirectoryInfo> dirs = ftpSource.GetFolders();
            foreach (FileDirectoryInfo dir in dirs)
            {
                string destinationDir = Path.Combine(destination, dir.Name);
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }
                else
                {
                    Directory.Delete(destinationDir);
                    Directory.CreateDirectory(destinationDir);
                }
                
                CopyFromFtpToFS(Path.Combine(source, dir.Name), destinationDir);
            }
        }
        public string Synchronize(string sourcePath, string targetPath)
        {
            try
            {
                if (source == TypeCnnection.FileSystem && target == TypeCnnection.FileSystem)
                {
                    CopyFromFSToFS(new DirectoryInfo(sourcePath), new DirectoryInfo(targetPath));
                }
                else if (source == TypeCnnection.Ftp && target == TypeCnnection.Ftp)
                {
                    CopyFromFtpToFtp(sourcePath, targetPath);
                }
                else if (source == TypeCnnection.FileSystem && target == TypeCnnection.Ftp)
                {
                    CopyFromFSToFtp(sourcePath,targetPath);
                }
                else if (source == TypeCnnection.Ftp && target == TypeCnnection.FileSystem)
                {
                    CopyFromFtpToFS(sourcePath, targetPath);
                }
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
