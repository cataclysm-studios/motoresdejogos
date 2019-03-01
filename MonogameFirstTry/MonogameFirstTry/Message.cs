using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class Message
    {
        protected MessageType messageType;

        public MessageType MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        protected string messageText;

        public string MessageText
        {
            get { return messageText; }
            set { messageText = value; }
        }

        public Message(MessageType messageType, string messageText)
        {
            this.messageType = messageType;
            this.messageText = messageText;
        }

    }
}
