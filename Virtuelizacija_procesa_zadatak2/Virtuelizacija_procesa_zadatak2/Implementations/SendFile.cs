using Common.Commands;
using Common.FileManipulations;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.InMemoryDataBase;
using Virtuelizacija_procesa_zadatak2.ManageRecvFile;

namespace Virtuelizacija_procesa_zadatak2.Implementations
{
    public class SendFile : ISendFile
    {

        [OperationBehavior(AutoDisposeParameters = true)]
        public CSVFileResult Send(FileMemOptions fs)
        {
            InMemoryDataBase.InMemoryDataBase.Instance.InsertFile(fs.Name, MoveToArray.FromStreamToByte(fs.Ms));
            var path= ConfigurationManager.AppSettings["path"];
            DirUtils.CheckCreatePath(path);
            DirUtils.EmptyDirectory(path);

            FileProcessing fp=new FileProcessing(InMemoryDataBase.InMemoryDataBase.Instance.GetFileData(fs.Name),
                                                 path,
                                                 fs.Name);
            fp.MakeFile();
            fp.ReadFromXml();

            CSVFileResult cs = FileRetrieving.GetCSVFiles(path);
            DirUtils.EmptyDirectory(path);
            Console.WriteLine("Sending back file(s)!");
            Console.WriteLine();
            return cs;
            
            
        }

        

        

    }
}
