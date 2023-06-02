using Common.FileManipulations;
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
            CSVFileResult Send(FileMemOptions fs);

        }
    
}
