using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.Items;

namespace Virtuelizacija_procesa_zadatak2.DBManipulation
{
    public interface IAuditingDB
    {
        bool InsertLoad(Load ld);
        Dictionary<int,Load> GetLoads();

        bool InsertAudit(Audit ad);
        Dictionary<int, Audit> GetAudits();

        bool InsertImportedFile(ImportedFile ld);
        Dictionary<int, ImportedFile> GetImportedFiles();

    }
}
