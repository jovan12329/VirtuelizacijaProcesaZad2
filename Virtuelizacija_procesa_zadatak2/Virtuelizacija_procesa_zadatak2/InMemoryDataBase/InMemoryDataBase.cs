using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.DBManipulation;
using Virtuelizacija_procesa_zadatak2.Items;

namespace Virtuelizacija_procesa_zadatak2.InMemoryDataBase
{
    public class InMemoryDataBase : IData,IAuditingDB
    {

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> lazyInstance = new Lazy<InMemoryDataBase>(() =>
        {
            return new InMemoryDataBase();
        });

        public static InMemoryDataBase Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        //Keeps files in DB
        private ConcurrentDictionary<string, byte[]> dB = new ConcurrentDictionary<string, byte[]>();
        //Keeps file records in DB
        private ConcurrentDictionary<int, Load> lddB = new ConcurrentDictionary<int, Load>();
        private ConcurrentDictionary<int, Audit> audB = new ConcurrentDictionary<int, Audit>();
        private ConcurrentDictionary<int, ImportedFile> imdB = new ConcurrentDictionary<int, ImportedFile>();

        public Dictionary<string, byte[]> GetFileData(string fileBeginsWith)
        {
            List<string> keys = dB.Keys.Where(t => t.StartsWith(fileBeginsWith, StringComparison.InvariantCultureIgnoreCase)).ToList();
            Dictionary<string, byte[]> fileData = new Dictionary<string, byte[]>();
            keys.ForEach(t => AddToDict(fileData, t));
            return fileData;
        }

        private void AddToDict(Dictionary<string, byte[]> fileData, string s)
        {
            byte[] fd;
            if (dB.TryGetValue(s, out fd))
            {
                fileData.Add(s, fd);
            }
        }

        public bool InsertFile(string fileName, byte[] fileData)
        {
            if (dB.ContainsKey(fileName)) {

                dB[fileName] = fileData;
                return false;
            
            }

            return dB.TryAdd(fileName, fileData);
        }

        //Auditing DBManipulation
        public bool InsertLoad(Load ld)
        {
            return lddB.TryAdd(ld.Id, ld);
        }
        public bool InsertAudit(Audit ad)
        {
            return audB.TryAdd(ad.Id, ad);
        }
        public bool InsertImportedFile(ImportedFile ld)
        {
            return imdB.TryAdd(ld.Id, ld);
        }

        public Dictionary<int, Load> GetLoads()
        {
            Dictionary<int, Load> dict = new Dictionary<int, Load>();
            List<int> keys = lddB.Keys.ToList();
            keys.ForEach(i => AddLoad(i, dict));
            return dict;
        }

        private void AddLoad(int i, Dictionary<int, Load> dict) 
        {
            Load fd;
            if (lddB.TryGetValue(i, out fd))
            {
                dict.Add(i, fd);
            }

        }

        private void AddAudit(int i, Dictionary<int, Audit> dict)
        {
            Audit fd;
            if (audB.TryGetValue(i, out fd))
            {
                dict.Add(i, fd);
            }

        }

        private void AddImportedFile(int i, Dictionary<int, ImportedFile> dict)
        {
            ImportedFile fd;
            if (imdB.TryGetValue(i, out fd))
            {
                dict.Add(i, fd);
            }

        }

        public Dictionary<int, Audit> GetAudits()
        {
            Dictionary<int, Audit> dict = new Dictionary<int, Audit>();
            List<int> keys = audB.Keys.ToList();
            keys.ForEach(i => AddAudit(i, dict));
            return dict;
        }


        public Dictionary<int, ImportedFile> GetImportedFiles()
        {
            Dictionary<int, ImportedFile> dict = new Dictionary<int, ImportedFile>();
            List<int> keys = imdB.Keys.ToList();
            keys.ForEach(i => AddImportedFile(i, dict));
            return dict;
        }
    }
}
