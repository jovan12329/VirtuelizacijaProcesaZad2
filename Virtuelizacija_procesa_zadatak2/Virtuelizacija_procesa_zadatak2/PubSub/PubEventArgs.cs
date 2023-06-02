using Common.FileManipulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.Items;

namespace Virtuelizacija_procesa_zadatak2.PubSub
{
    public class PubEventArgs:EventArgs
    {
        public LoadingFile fl { get; set; }
        public List<Load> xmlFile { get; set; }

    }
}
