using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.Items;

namespace Virtuelizacija_procesa_zadatak2.PubSub
{
    public class CSVLoading
    {

        public void LastLoad(object source, PubEventArgs e)
        {

            var howMany = ConfigurationManager.AppSettings["csv"];

            if (howMany.Equals("ONE"))
            {
                CreateOneCSV(e);
            }
            else 
            {

                CreateMoreCSV(e);
            
            }

            
        }

        private string GetFormattedHeader() {

            return "DATE,TIME,FORECAST_VALUE,MEASURED_VALUE";


        }

        private void CreateOneCSV(PubEventArgs e) 
        {

            FileStream fs = new FileStream(Path.Combine(e.fl.DirPath, e.fl.FileName.Replace(".xml", ".csv")), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(GetFormattedHeader());
            if (e.xmlFile != null)
            {
                foreach (var elem in e.xmlFile)
                {

                    sw.WriteLine(elem);

                }
            }
            else {

                Console.WriteLine("Cannot create CSV file, because error occured during loading .xml file!");
            
            }

            sw.Dispose();
            sw.Close();
            fs.Dispose();
            fs.Close();

        }

        private void CreateMoreCSV(PubEventArgs e) 
        {
            if (e.xmlFile == null)
            {
                CreateOneCSV(e);
                return;
            }

            
            FileStream fs = null;
            StreamWriter sw = null;
            int[] indexes = GetIndexes(e);

            for (int i = 0; i < indexes.Length-1; i++) {

                for (int j = indexes[i]; j < indexes[i + 1]; j++) {

                    if (fs == null && sw == null) {

                        var name= e.fl.FileName.Replace(".xml","_"+e.xmlFile[indexes[i]].TimeStamp.Date.ToString("d").Replace("-","_"))+".csv";
                        fs = new FileStream(Path.Combine(e.fl.DirPath,name),FileMode.Create);
                        sw = new StreamWriter(fs);
                        sw.WriteLine(GetFormattedHeader());
                        sw.WriteLine(e.xmlFile[j]);
                        continue;

                    }
                    if (fs != null && sw != null) 
                    {

                        sw.WriteLine(e.xmlFile[j]);

                    
                    }


                }

                sw.Dispose();
                sw.Close();
                fs.Dispose();
                fs.Close();
                fs = null;
                sw = null;
            }


        }


        private int[] GetIndexes(PubEventArgs e) {

            DateTime firstRow = e.xmlFile[0].TimeStamp.Date;
            List<int> index = new List<int>(10);
            index.Add(0);
            for (int j=1;j<e.xmlFile.Count;j++) 
            {

                if (!firstRow.ToString("d").Equals(e.xmlFile[j].TimeStamp.Date.ToString("d")))
                {
                    index.Add(j);
                    firstRow =e.xmlFile[j].TimeStamp;
                }

                if ((j+1) == e.xmlFile.Count) 
                {
                    index.Add(j);
                }

            }

            return index.ToArray();


        }




    }
}
