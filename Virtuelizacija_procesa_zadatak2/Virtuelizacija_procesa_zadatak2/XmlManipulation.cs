using Common.Items;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Virtuelizacija_procesa_zadatak2
{
    public class XmlManipulation
    {

        public static IEnumerable<XElement> LoadItems(string path,string fileName) {

            var filename = fileName;
            var currentDirectory = path;
            var purchaseOrderFilepath = Path.Combine(currentDirectory, filename);

            XElement purchaseOrder = XElement.Load(purchaseOrderFilepath);

            IEnumerable<XElement> partNos = from item in purchaseOrder.Descendants("row")
                                            where (int)item.Element("MEASURED_VALUE") > 0
                                            select item;

            return partNos;

        }

        public static List<Load> ParseItems(string path, string fileName) {

            IEnumerable<XElement> parts = LoadItems(path,fileName);
            List<Load> testici = new List<Load>();
            
            XElement last = parts.Last();
            int i = 0;
            foreach (XElement item in parts)
            {
                
                double measuredValue = (double)item.Element("MEASURED_VALUE");
                double forecastValue = (double)item.Element("FORECAST_VALUE");
                string timeStamp = (string)item.Element("TIME_STAMP");

                DateTime parsedTimeStamp = DateTime.ParseExact(timeStamp, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                


                //Does exist
                bool vecIma = false;
                foreach (Load t in testici)
                {
                    if (t.TimeStamp == parsedTimeStamp)
                        vecIma = true;
                }
                if (vecIma == false)
                    testici.Add(new Load(6, parsedTimeStamp, forecastValue, measuredValue));

                Console.WriteLine($"Load: {testici[i].Id + i + 1}, {testici[i].MeasuredValue}, {testici[i].ForecastValue}, {testici[i].TimeStamp}");
                //Check if last
                if (item == last)
                {
                    Console.WriteLine("Da li 1 ili vise datoteka?");
                    int broj = Convert.ToInt32(Console.ReadLine());
                    if (broj > 1)
                    {
                        Console.WriteLine("Veca");
                    }
                    else
                    {
                        Console.WriteLine("Jedna");
                    }
                }


                i++;
            }


            return testici;

        }

       



    }
}
