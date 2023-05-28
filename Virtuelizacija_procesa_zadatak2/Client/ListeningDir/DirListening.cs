using Common.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.ListeningDir
{
    public class DirListening
    {
        private FileSystemWatcher sysDir;
        private ISendFile sender;
        private bool isCreated = false;

        public DirListening(ISendFile proxy,string path)
        {
            this.sender = proxy;
            this.sysDir = new FileSystemWatcher(path);
            this.sysDir.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            this.sysDir.Filter = "*.xml";
            this.sysDir.IncludeSubdirectories = false;
            this.sysDir.EnableRaisingEvents = true;
            this.sysDir.Changed += OnChanged;
            this.sysDir.Created += OnCreated;
            



        }


        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed || isCreated)
            {
                isCreated=false;
                return;
            }
            Console.WriteLine($"Changed: {e.FullPath}");
 
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created:{e.FullPath}";
            Console.WriteLine(value);

            string name = e.Name;
            byte[] data;
            FileStream fs = new FileStream(e.FullPath, FileMode.Open);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);
            data = ms.GetBuffer();
            ms.Dispose();
            ms.Close();
            fs.Dispose();
            fs.Close();
            this.sender.Send(name, data);
            isCreated = true;
        }



    }
}
