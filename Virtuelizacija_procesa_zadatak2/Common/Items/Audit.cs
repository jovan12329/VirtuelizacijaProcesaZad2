using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Items
{
    [DataContract]
    public class Audit
    {
        private int id;
        private DateTime timeStamp;
        private MessageType messageType;
        private string message;

        public Audit() { }

        public Audit(int id, DateTime timeStamp, MessageType messageType, string message)
        {
            this.Id = id;
            this.TimeStamp = timeStamp;
            this.MessageType = messageType;
            this.Message = message;
        }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        [DataMember]
        public MessageType MessageType { get => messageType; set => messageType = value; }
        [DataMember]
        public string Message { get => message; set => message = value; }
    }
}
