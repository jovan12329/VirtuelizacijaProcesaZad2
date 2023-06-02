using Common.FileManipulations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.PubSub;

namespace Virtuelizacija_procesa_zadatak2.ManageRecvFile
{
    public class FileProcessing
    {

        private Dictionary<string, byte[]> fileConst;
        private string serviceDirPath;
        private string fileName;

        public FileProcessing(Dictionary<string, byte[]> fileConst, string serviceDirPath,string file)
        {
            this.FileConst = fileConst;
            this.ServiceDirPath = serviceDirPath;
            this.FileName = file;
        }

        public Dictionary<string, byte[]> FileConst { get => fileConst; set => fileConst = value; }
        public string ServiceDirPath { get => serviceDirPath; set => serviceDirPath = value; }
        public string FileName { get => fileName; set => fileName = value; }

        /// <summary>
        /// Reconstruction of file from InMemoryDB 
        /// </summary>
        public void MakeFile() {

            FileStream f = new FileStream(Path.Combine(ServiceDirPath,FileName), FileMode.OpenOrCreate);
            f.Write(fileConst[fileName], 0, fileConst[fileName].Length);
            f.Dispose();
            f.Close();

        }

        public void ReadFromXml() {

            var publisher = new PubXml();
            var notifier = new CSVLoading();
            publisher.CSVGenerating += notifier.LastLoad;
            LoadingFile lf = new LoadingFile() { FileName = fileName, DirPath = serviceDirPath };
            publisher.SubscribeXmlFile(lf);



        }

        
    }
}
