using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Client.ListeningDir;
using Common;
using Common.Commands;
using Common.Utils;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ChannelFactory<ISendFile> channelBezbednost =
                new ChannelFactory<ISendFile>("ServiceName");
            ISendFile proxy = channelBezbednost.CreateChannel();
            var uploadPath = ConfigurationManager.AppSettings["uploadPath"];
            var downloadPath = ConfigurationManager.AppSettings["downloadPath"];
            DirUtils.CheckCreatePath(uploadPath);
            DirUtils.CheckCreatePath(downloadPath);
            Console.WriteLine("Listening from directory:" + uploadPath + "...");
            DirListening dir = new DirListening(proxy,uploadPath,downloadPath);


            Console.ReadKey();

        }
    }
}
