using Common.Commands;
using Common.FileManipulations;
using Common.Utils;
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
        private string pathConfig;
        private string download;
        public DirListening(ISendFile proxy,string path,string download)
        {
            this.download = download;
            this.pathConfig = path;
            this.sender = proxy;
            this.sysDir = new FileSystemWatcher(path);
            this.sysDir.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.Size;

            this.sysDir.Filter = "*.xml";
            this.sysDir.EnableRaisingEvents = true;
            this.sysDir.Changed += OnChanged;
            this.sysDir.Created += OnCreated;
            



        }


        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed || isCreated==true)
            {
                isCreated = false;
                return;
            }
            Console.WriteLine($"Changed: {e.FullPath}");

            //Additional variables
            string name = e.Name;
            //Sending files
            CSVFileResult files;
            SendingFiles(e.FullPath, e.Name, out files);
            //Recieved files
            if (files == null)
                Console.WriteLine("Error occured during processing ! Please try again to send the file !");
            else
                RecievedFiles(files);


            isCreated = true;
 
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created:{e.FullPath}";
            Console.WriteLine(value);
            //Additional variables
            isCreated = true;
            string name = e.Name;
            //Sending files
            CSVFileResult files;
            SendingFiles(e.FullPath,e.Name,out files);
            //Recieved files
            if (files == null)
                Console.WriteLine("Error occured during processing ! Please try again to send the file !");
            else
                RecievedFiles(files);

            
        }


        private void SendingFiles(string path,string name,out CSVFileResult csv) {

            //Sending File
            FileStream fs = new FileStream(path, FileMode.Open);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);
            FileMemOptions mem = new FileMemOptions(name, ms);
            //Recieving file if exists
            csv = this.sender.Send(mem);
            //Free resources
            ms.Dispose();
            ms.Close();
            fs.Dispose();
            fs.Close();


        }


        private void RecievedFiles(CSVFileResult res) {

            foreach (KeyValuePair<string, MemoryStream> r in res.CsvFiles) 
            {
                using (FileStream fs = new FileStream(Path.Combine(download, r.Key), FileMode.Create)) {

                    fs.Write(MoveToArray.FromStreamToByte(r.Value)
                        , 0
                        , MoveToArray.FromStreamToByte(r.Value).Length);

                }
                Console.WriteLine($"Created file on path: {Path.Combine(download, r.Key)}");
            }

            res.Dispose();

        }

        

    }
}
