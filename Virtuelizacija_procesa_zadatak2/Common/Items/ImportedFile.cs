using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Items
{
    [DataContract]
    public class ImportedFile
    {
        private int id;
        private string fileName;

        public ImportedFile() { }
        public ImportedFile(int id, string fileName)
        {
            this.Id = id;
            this.FileName = fileName;
        }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string FileName { get => fileName; set => fileName = value; }
    }
}
