using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuelizacija_procesa_zadatak2.TableIdCounter
{
    public class TableId
    {
        private static int id;

        public static void InitTable() {

            id = 0;
        
        }

        public static int TableRow() {

            return ++id;
        
        }

    }
}
