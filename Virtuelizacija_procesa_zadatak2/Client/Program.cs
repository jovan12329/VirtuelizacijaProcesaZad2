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
            DirUtils.CheckCreatePath(uploadPath);
            DirListening dir = new DirListening(proxy,uploadPath);


            Console.ReadKey();

        }
    }
}
