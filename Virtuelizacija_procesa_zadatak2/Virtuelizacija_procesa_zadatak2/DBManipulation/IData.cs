using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuelizacija_procesa_zadatak2.DBManipulation
{
    public interface IData
    {
        bool InsertFile(string fileName, byte[] fileData);
        Dictionary<string, byte[]> GetFileData(string fileBeginsWith);

    }
}
