using Common.FileManipulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.Items;

namespace Virtuelizacija_procesa_zadatak2.PubSub
{
    public class PubXml
    {
        public delegate void PubXmlEventHandler(object source, PubEventArgs e);
        public event PubXmlEventHandler CSVGenerating;
        private void OnReadLastLoad(LoadingFile fs,List<Load> ld)
        {
            if (CSVGenerating != null)
            {
                CSVGenerating(this, new PubEventArgs() {fl=fs, xmlFile=ld });
            }
        }
        public void SubscribeXmlFile(LoadingFile fs)
        {
            List<Load> loadovi=XmlManipulation.ParseItems(fs.DirPath, fs.FileName);
            OnReadLastLoad(fs,loadovi);
        }


    }
}
