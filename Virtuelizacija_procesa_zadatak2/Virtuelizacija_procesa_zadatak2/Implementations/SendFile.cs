using Common.Commands;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.InMemoryDataBase;

namespace Virtuelizacija_procesa_zadatak2.Implementations
{
    public class SendFile : ISendFile
    {
        public void Send(string name, byte[] buffer)
        {
            InMemoryDataBase.InMemoryDataBase.Instance.InsertFile(name, buffer);

            //Check if directory is valid
            var path= ConfigurationManager.AppSettings["path"];
            DirUtils.CheckCreatePath(path);
            Dictionary<string,byte[]> rec=InMemoryDataBase.InMemoryDataBase.Instance.GetFileData(name);
            string fullPath = Path.Combine(path, name);
            FileStream fs = new FileStream(fullPath, FileMode.Create);
            fs.Write(rec[name], 0, rec[name].Length);
            Thread.Sleep(1000);
            fs.Dispose();
            fs.Close();
            XmlManipulation.ParseItems(path,name);
        }

        

        

    }
}
