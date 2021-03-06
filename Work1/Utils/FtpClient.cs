﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Work1.Utils
{
    enum DirectoryType
    {
        File=1,
        Folder=2,
        Back=3
    }
    public interface IFtpViewerResult
    {
        void SetSelectedPath(string path);
    }
    class FtpClient
    {
        private string password;
        private string userName;
        public string Uri;
        private string prevUri;
        private readonly string hostUri;
        private int bufferSize = 1024;

        public bool Passive = true;
        public bool Binary = true;
        public bool EnableSsl = false;
        public bool Hash = false;

        public FtpClient(string uri, string userName, string password)
        {
            this.Uri = uri;
            this.hostUri = uri;
            this.prevUri = uri;
            this.userName = userName;
            this.password = password;
        }
        public FtpClient(string uri, FtpUser user)
        {
            this.Uri = uri;
            this.hostUri = uri;
            this.prevUri = uri;
            this.userName = user.User;
            this.password = user.Password;
        }
        public string ChangeWorkingDirectory(string path)
        {
            Uri = combine(Uri, path);

            return PrintWorkingDirectory();
        }

        public string DeleteFile(string fileName)
        {
            var request = createRequest(combine(Uri, fileName), WebRequestMethods.Ftp.DeleteFile);

            return getStatusDescription(request);
        }

        public string DownloadFile(string source, string dest)
        {
            var request = createRequest(combine(Uri, source), WebRequestMethods.Ftp.DownloadFile);

            byte[] buffer = new byte[bufferSize];

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var fs = new FileStream(dest, FileMode.OpenOrCreate))
                    {
                        
                        int readCount = stream.Read(buffer, 0, bufferSize);

                        while (readCount > 0)
                        {
                            if (Hash)
                                Console.Write("#");

                            fs.Write(buffer, 0, readCount);
                            readCount = stream.Read(buffer, 0, bufferSize);
                        }
                    }
                }

                return response.StatusDescription;
            }
        }

        public DateTime GetDateTimestamp(string fileName)
        {
            var request = createRequest(combine(Uri, fileName), WebRequestMethods.Ftp.GetDateTimestamp);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.LastModified;
            }
        }

        public long GetFileSize(string fileName)
        {
            var request = createRequest(combine(Uri, fileName), WebRequestMethods.Ftp.GetFileSize);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.ContentLength;
            }
        }

        public string[] ListDirectory()
        {
            var list = new List<string>();

            var request = createRequest(WebRequestMethods.Ftp.ListDirectory);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public string[] ListDirectoryDetails()
        {
            var list = new List<string>();

            var request = createRequest(WebRequestMethods.Ftp.ListDirectoryDetails);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public string MakeDirectory(string directoryName)
        {
            var request = createRequest(combine(Uri, directoryName), WebRequestMethods.Ftp.MakeDirectory);
            return getStatusDescription(request);
        }

        public string PrintWorkingDirectory()
        {
            var request = createRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);

            return getStatusDescription(request);
        }

        public string RemoveDirectory(string directoryName)
        {
            var request = createRequest(combine(Uri, directoryName), WebRequestMethods.Ftp.RemoveDirectory);

            return getStatusDescription(request);
        }

        public string Rename(string currentName, string newName)
        {
            var request = createRequest(combine(Uri, currentName), WebRequestMethods.Ftp.Rename);

            request.RenameTo = newName;

            return getStatusDescription(request);
        }
        public bool IsExist(string name)
        {
            var request = createRequest(combine(Uri, name), WebRequestMethods.Ftp.ListDirectory);            
            var result = this.LoadDerectory().Exists(x => x.Name == name);
            return result;
        }
        public string UploadFile(string source, string destination)
        {
            var request = createRequest(combine(Uri,destination), WebRequestMethods.Ftp.UploadFile);

            using (var stream = request.GetRequestStream())
            {
                using (var fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                            Console.Write("#");

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            return getStatusDescription(request);
        }

        public string UploadFileWithUniqueName(string source)
        {
            var request = createRequest(WebRequestMethods.Ftp.UploadFileWithUniqueName);

            using (var stream = request.GetRequestStream())
            {
                using (var fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                            Console.Write("#");

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return Path.GetFileName(response.ResponseUri.ToString());
            }
        }

        private FtpWebRequest createRequest(string method)
        {
            return createRequest(Uri, method);
        }

        private FtpWebRequest createRequest(string uri, string method)
        {
            var ftpRequest = (FtpWebRequest)WebRequest.Create(uri);

            ftpRequest.Credentials = new NetworkCredential(userName, password);
            ftpRequest.Method = method;
            ftpRequest.UseBinary = Binary;
            ftpRequest.EnableSsl = EnableSsl;
            ftpRequest.UsePassive = Passive;
            return ftpRequest;
        }

        private string getStatusDescription(FtpWebRequest request)
        {
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
            }
        }

        private string combine(string path1, string path2)
        {
            return Path.Combine(path1, path2).Replace("\\", "/");
        }
        public void ToDerectory(FileDirectoryInfo derectory)
        {
            if(derectory!=null)
            {
                this.Uri = derectory.Adress;
            }
        }

        
        private string derectoryUp(string path)
        {
            if (path != hostUri)
            {
                var adress = (new StringBuilder(new string(path.Reverse().ToArray()))).ToString();
                int length = adress.IndexOf('/', 1);
                var result = new StringBuilder(adress);
                result.Remove(0, length);

                return new string(result.ToString().Reverse().ToArray());
            }
            else return hostUri;
        }
        public List<FileDirectoryInfo> GetFiles()
        {
            return LoadDerectory().Where(x => x.Type == DirectoryType.File).ToList();
        }
        public List<FileDirectoryInfo> GetFolders()
        {
            return LoadDerectory().Where(x => x.Type == DirectoryType.Folder).ToList();
        }
        public List<FileDirectoryInfo> LoadDerectory()
        {
            Regex regex = new Regex(@"^([d-])([rwxt-]{3}){3}\s+\d{1,}\s+.*?(\d{1,})\s+(\w+\s+\d{1,2}\s+(?:\d{4})?)(\d{1,2}:\d{2})?\s+(.+?)\s?$",
    RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            List<FileDirectoryInfo> list = this.ListDirectoryDetails()
                                                 .Select(s =>
                                                 {
                                                     Match match = regex.Match(s);
                                                     if (match.Length > 5)
                                                     {
                                                         DirectoryType type = match.Groups[1].Value == "d" ? DirectoryType.Folder : DirectoryType.File;
                                                         Uri icon = type == DirectoryType.Folder ? new Uri("/Content/Images/folder.png", UriKind.RelativeOrAbsolute) : new Uri("/Content/Images/file.png", UriKind.RelativeOrAbsolute);
                                                         string size = "";
                                                         size = (Int32.Parse(match.Groups[3].Value.Trim()) / 1024).ToString() + " kB";

                                                         return new FileDirectoryInfo(size, type, match.Groups[6].Value, match.Groups[4].Value,String.Format("{0}/", combine(this.Uri, match.Groups[6].Value))) { Icon = icon };
                                                     }
                                                     else return new FileDirectoryInfo();
                                                 }).ToList();
            list.Add(new FileDirectoryInfo("", DirectoryType.Back, "", "", this.derectoryUp(this.Uri), "Back") { Icon = new Uri("/Content/Images/back.png", UriKind.RelativeOrAbsolute) });
            list.Reverse();
            list = list.OrderByDescending(x => x.Type).ToList();
            return list ?? null;
        }
        #region Static load
        public static List<FileDirectoryInfo> LoadDerictory(string uri, string userName, string password)
        {

            // Создаем объект FtpClient по FTP
            FtpClient client = new FtpClient(uri, userName, password);

            // Регулярное выражение, которое ищет информацию о папках и файлах 
            // в строке ответа от сервера
            Regex regex = new Regex(@"^([d-])([rwxt-]{3}){3}\s+\d{1,}\s+.*?(\d{1,})\s+(\w+\s+\d{1,2}\s+(?:\d{4})?)(\d{1,2}:\d{2})?\s+(.+?)\s?$",
                RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            // Получаем список корневых файлов и папок
            // Используется LINQ to Objects и регулярные выражения
            List<FileDirectoryInfo> list = client.ListDirectoryDetails()
                                                 .Select(s =>
                                                 {
                                                     Match match = regex.Match(s);
                                                     if (match.Length > 5)
                                                     {

                                                         // Устанавливаем тип, чтобы отличить файл от папки (используется также для установки рисунка)
                                                         DirectoryType type = match.Groups[1].Value == "d" ? DirectoryType.Folder : DirectoryType.File;
                                                         Uri icon = type == DirectoryType.Folder ? new Uri("/Content/Images/folder.png", UriKind.RelativeOrAbsolute) : new Uri("/Content/Images/file.png", UriKind.RelativeOrAbsolute);
                                                             // Размер задаем только для файлов, т.к. для папок возвращается
                                                             // размер ярлыка 4кб, а не самой папки
                                                             string size = "";
                                                         //if (type == DirectoryType.File)
                                                             size = (Int32.Parse(match.Groups[3].Value.Trim()) / 1024).ToString() + " кБ";

                                                         return new FileDirectoryInfo(size, type, match.Groups[6].Value, match.Groups[4].Value, uri) { Icon = icon };
                                                     }
                                                     else return new FileDirectoryInfo();
                                                 }).ToList();

            // Добавить поле, которое будет возвращать пользователя на директорию выше
            list.Add(new FileDirectoryInfo("", DirectoryType.Back, "", "", uri) { Icon = new Uri("/Content/Images/back.png", UriKind.RelativeOrAbsolute) });
            list.Reverse();
            list = list.OrderByDescending(x => x.Type).ToList();
            return list ?? null;

            //MessageBox.Show(ex.ToString() + ": \n" + ex.Message);
            //return new List<FileDirectoryInfo>();
        }
        #endregion
    }
}
