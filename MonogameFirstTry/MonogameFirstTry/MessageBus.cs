using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class MessageBus
    {
        #region Singleton
        private static MessageBus _messageBus;
        public static MessageBus Instance
        {
            get
            {
                if (_messageBus == null)
                {
                    _messageBus = new MessageBus();
                }
                return _messageBus;
            }

        }
        #endregion

        protected static List<Message> messages;
        public static List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        protected static List<Message> tempMessages;
        protected static List<Message> tempMessagesDelete;

        public void Initialize()
        {
            messages = new List<Message>();
            tempMessages = new List<Message>();
            tempMessagesDelete = new List<Message>();
        }
        

        public void AddMessage(Message message)
        {
            messages.Add(message);
        }

        public static List<Message> GetMessagesOfType(MessageType messageType)
        {
            tempMessages.Clear();
            tempMessagesDelete.Clear();
            foreach (Message message in messages)
            {
                if (message.MessageType == messageType)
                {
                    tempMessages.Add(message);
                    tempMessagesDelete.Add(message);
                }
            }
            foreach (Message message in tempMessagesDelete)
            {
                if (messages.Contains(message))
                {
                    messages.Remove(message);
                }
            }
            return tempMessages;
        }
    }
}
