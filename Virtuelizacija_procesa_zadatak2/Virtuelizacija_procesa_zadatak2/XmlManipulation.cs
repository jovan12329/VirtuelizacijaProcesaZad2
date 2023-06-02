using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Virtuelizacija_procesa_zadatak2.Enum;
using Virtuelizacija_procesa_zadatak2.Items;
using Virtuelizacija_procesa_zadatak2.TableIdCounter;

namespace Virtuelizacija_procesa_zadatak2
{
    public class XmlManipulation
    {

        public static List<Load> ParseItems(string path, string fileName) {

            Console.WriteLine("Parsing items from file "+Path.Combine(path,fileName));
            var filename = fileName;
            var currentDirectory = path;
            var purchaseOrderFilepath = Path.Combine(currentDirectory, filename);
            TableId.InitTable();

            XElement purchaseOrder = null;
            IEnumerable<XElement> partNos = null;

            List<Load> loads = new List<Load>(100);
            List<Audit> audits = new List<Audit>(100);
            int i = 0;
            string message = "";

            try
            {
                purchaseOrder = XElement.Load(purchaseOrderFilepath);
            }

            catch (Exception)
            {
                //No fileds are read that can be transferred to Audit, using empty ones
                //WriteLine is for testing, del after

                message = "Error trying to load file!";
                Audit asd = new Audit(DateTime.Now, MessageType.ERROR, message);
                InMemoryDataBase.InMemoryDataBase.Instance.InsertAudit(asd);
                Console.WriteLine($"{asd.MessageType.ToString()}[{TableId.TableRow()}]: {asd.TimeStamp}, {asd.MessageType}");
                return null;
            }

            try
            {
                partNos = from item in purchaseOrder.Descendants("row")
                          select item;
            }

            catch (Exception)
            {
                //No fileds are read that can be transferred to Audit, using empty ones
                message = "Error trying to load file!";
                Audit ds = new Audit(DateTime.Now, MessageType.ERROR, message);
                InMemoryDataBase.InMemoryDataBase.Instance.InsertAudit(ds);
                Console.WriteLine($"{ds.MessageType.ToString()}[{TableId.TableRow()}]: {ds.TimeStamp}, {ds.Message}");
                Thread.Sleep(500);
                return null;
            }


            i = -1;
            XElement last = partNos.Last();
            foreach (XElement item in partNos)
            {
                double measuredValue = 0;
                double forecastValue = 0;
                DateTime parsedTimeStamp = DateTime.MinValue;
                message = "";
                i++;

                //Self-exp
                try
                {
                    parsedTimeStamp = DateTime.ParseExact((string)item.Element("TIME_STAMP"), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    message = "Error trying to parse TimeStamp attribute to DateTime!";
                    Audit a6 = new Audit(DateTime.Now, MessageType.ERROR, message);
                    InMemoryDataBase.InMemoryDataBase.Instance.InsertAudit(a6);
                    Console.WriteLine($"{a6.MessageType.ToString()}[{TableId.TableRow()}]: {a6.TimeStamp}, {a6.Message}");
                    Thread.Sleep(500);
                    continue;
                }

                try
                {
                    measuredValue = (double)item.Element("MEASURED_VALUE");
                }
                catch (Exception)
                {
                    message = "Error trying to parse MeasuredValue attribute to double!";
                    Audit a1 = new Audit(DateTime.Now, MessageType.ERROR, message);
                    InMemoryDataBase.InMemoryDataBase.Instance.InsertAudit(a1);
                    Console.WriteLine($"{a1.MessageType.ToString()}[{TableId.TableRow()}]: {a1.TimeStamp}, {a1.Message}");
                    Thread.Sleep(500);
                    continue;
                }

                try
                {
                    forecastValue = (double)item.Element("FORECAST_VALUE");
                }
                catch (Exception)
                {
                    message = "Error trying to parse ForecastValue attribute to double!";
                    Audit a2 = new Audit(DateTime.Now, MessageType.ERROR, message);
                    InMemoryDataBase.InMemoryDataBase.Instance.InsertAudit(a2);
                    Console.WriteLine($"{a2.MessageType.ToString()}[{TableId.TableRow()}]: {a2.TimeStamp}, {a2.Message}");
                    Thread.Sleep(500);
                    continue;
                }

                //Self-exp
                if (measuredValue == 0 || forecastValue == 0 || parsedTimeStamp == DateTime.MinValue)
                {
                    message = "Attributes are successfully parsed, but error occured during assign!";
                    Audit a3 = new Audit(DateTime.Now, MessageType.ERROR, message);
                    InMemoryDataBase.InMemoryDataBase.Instance.InsertAudit(a3);
                    Console.WriteLine($"{a3.MessageType.ToString()}[{TableId.TableRow()}]: {a3.TimeStamp}, {a3.Message}");
                    Thread.Sleep(500);
                    continue;
                }

                //Does audit exist alr
                if (message == "")
                    message = "Load successfully read and parsed!";
                Audit a = new Audit(DateTime.Now, MessageType.INFO, message);
                InMemoryDataBase.InMemoryDataBase.Instance.InsertAudit(a);
                Console.WriteLine($"{a.MessageType.ToString()}[{TableId.TableRow()}]: {a.TimeStamp}, {a.Message}");
                Thread.Sleep(500);
                

                //Does load exist alr
                bool loadCheck = false;
                foreach (Load t in loads)
                {
                    if (t.TimeStamp == parsedTimeStamp)
                        loadCheck = true;
                }
                if (loadCheck == false) {
                    Load l = new Load(parsedTimeStamp, measuredValue, forecastValue);
                    loads.Add(l);
                    InMemoryDataBase.InMemoryDataBase.Instance.InsertLoad(l);
                }
                
            }

            ImportedFile imp = new ImportedFile(filename);
            InMemoryDataBase.InMemoryDataBase.Instance.InsertImportedFile(imp);
            Console.WriteLine("File read successfully!");

            //Testing DB inserts
            //Console.WriteLine("Test DB LOAD:");
            //foreach (Load l in InMemoryDataBase.InMemoryDataBase.Instance.GetLoads().Values.ToList())
            //{
            //    Console.WriteLine($"Load: {l.Id}, {l.TimeStamp},{l.MeasuredValue}, {l.ForecastValue}");
            //}
            //Console.WriteLine("Test DB AUDIT:");
            //foreach (Audit a in InMemoryDataBase.InMemoryDataBase.Instance.GetAudits().Values.ToList())
            //{
            //    Console.WriteLine($"Audit: {a.Id}, {a.TimeStamp}, {a.TimeStamp}, {a.Message}");
            //}
            //Console.WriteLine("TEST DB IMPORTED FILE:");
            //foreach (ImportedFile a in InMemoryDataBase.InMemoryDataBase.Instance.GetImportedFiles().Values.ToList())
            //{
            //    Console.WriteLine($"Imported File: {a.Id}, {a.FileName}");
            //}


            return loads;

        }

       



    }
}
