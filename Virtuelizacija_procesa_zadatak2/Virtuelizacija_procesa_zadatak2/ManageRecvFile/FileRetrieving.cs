using Common.FileManipulations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuelizacija_procesa_zadatak2.ManageRecvFile
{
    public class FileRetrieving
    {
        public static CSVFileResult GetCSVFiles(string dirPath) {

            var dirCsv = new CSVFileResult();
            string[] files = CheckIfCSV(Directory.GetFiles(dirPath));
            if (files.Length <= 0)
                return null;

            foreach (string s in files) {

                AddStreams(s, dirCsv);

            }

            return dirCsv;

        
        
        }

        private static string[] CheckIfCSV(string[] files) {

            List<string> csvFiles = new List<string>(files.Length + 1);

            if (files.Length <= 0)
                return null;

            foreach (string s in files) 
            {
                if (s.Contains(".csv")) 
                {
                    csvFiles.Add(s);
                }
            }

            if (csvFiles.Count() <= 0)
                return null;

            return csvFiles.ToArray();

        }

        private static void AddStreams(string fullPath, CSVFileResult rslt) {

            string fileName = Path.GetFileName(fullPath);
            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read)) {

                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                rslt.CsvFiles.Add(fileName,ms);
            
            }

        
        }


    }
}
