using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.FileManipulations
{
    [DataContract]
    public class FileMemOptions:IDisposable
    {
        private string name;
        private MemoryStream ms;

        public FileMemOptions(string name, MemoryStream ms)
        {
            this.name = name;
            this.ms = ms;
        }

        [DataMember]
        public string Name { get => name; set => name = value; }
        
        [DataMember]
        public MemoryStream Ms { get => ms; set => ms = value; }

        
        public void Dispose()
        {
            if (ms == null)
                return;

            try
            {
                ms.Dispose();
                ms.Close();
                ms = null;

            }
            catch (Exception) {

                Console.WriteLine("Problem with disposing!");
            
            }
            
        }
    }
}
