using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Commands
{
    
        [ServiceContract]
        public interface ISendFile
        {
            [OperationContract]
            void Send(string name,byte[] buffer);

        }
    
}
