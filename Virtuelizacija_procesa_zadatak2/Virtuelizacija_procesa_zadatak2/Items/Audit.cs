using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.Enum;

namespace Virtuelizacija_procesa_zadatak2.Items
{
    
    public class Audit
    {
        private static int idCntA = 0;
        private int id;
        private DateTime timeStamp;
        private MessageType messageType;
        private string message;

        public Audit() { }

        public Audit(DateTime timeStamp, MessageType messageType, string message)
        {
            this.Id = ++idCntA;
            this.TimeStamp = timeStamp;
            this.MessageType = messageType;
            this.Message = message;
        }

        
        public int Id { get => id; private set => id = value; }
       
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        
        public MessageType MessageType { get => messageType; set => messageType = value; }
       
        public string Message { get => message; set => message = value; }
    }
}
