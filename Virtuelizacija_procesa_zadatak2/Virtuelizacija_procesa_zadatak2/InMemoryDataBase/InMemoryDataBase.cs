using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.DBManipulation;

namespace Virtuelizacija_procesa_zadatak2.InMemoryDataBase
{
    public class InMemoryDataBase : IData
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

        private ConcurrentDictionary<string, byte[]> dB = new ConcurrentDictionary<string, byte[]>();

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
            return dB.TryAdd(fileName, fileData);
        }
    }
}
