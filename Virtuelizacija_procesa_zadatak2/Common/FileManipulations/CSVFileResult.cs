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
    public class CSVFileResult:IDisposable
    {
        private Dictionary<string, MemoryStream> csvFiles;
        

        public CSVFileResult(){

            this.CsvFiles = new Dictionary<string,MemoryStream>();

        }

        [DataMember]
        public Dictionary<string, MemoryStream> CsvFiles { get => csvFiles; set => csvFiles = value; }


        public void Dispose()
        {

            if (CsvFiles == null)
                return;

            try
            {
                foreach (KeyValuePair<string, MemoryStream> val in CsvFiles) {

                    if (val.Value != null) {

                        val.Value.Dispose();
                        val.Value.Close();
                    
                    }
                
                
                }

                csvFiles.Clear();

            }
            catch (Exception) {

                Console.WriteLine("Problem with disposing!");
            
            }


        }
    }
}
