using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class MoveToArray
    {

        public static byte[] FromStreamToByte(MemoryStream ms) {

            return ms.ToArray();

        
        }


    }
}
