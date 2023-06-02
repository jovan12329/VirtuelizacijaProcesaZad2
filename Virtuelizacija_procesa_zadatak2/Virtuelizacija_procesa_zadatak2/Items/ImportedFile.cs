using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuelizacija_procesa_zadatak2.Items
{
    
    public class ImportedFile
    {
        private static int idCntI = 0;
        private int id;
        private string fileName;

        public ImportedFile() { }
        public ImportedFile(string fileName)
        {
            this.Id = ++idCntI;
            this.FileName = fileName;
        }

        
        public int Id { get => id; private set => id = value; }
        
        public string FileName { get => fileName; set => fileName = value; }
    }
}
