using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class ConsoleWriter
    {
        #region Singleton
        private static ConsoleWriter _consoleWriter;
        public static ConsoleWriter Instance
        {
            get
            {
                if (_consoleWriter == null)
                {
                    _consoleWriter = new ConsoleWriter();
                }
                return _consoleWriter;
            }
        }
        #endregion

        protected static List<Message> messages;
        private Message lastMessage;
        public static List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }


        public void Initialize()
        {
            messages = new List<Message>();
        }

        public void AddMessage(Message message)
        {
            messages.Add(message);
        }

        public void Write(Message message)
        {
            if(message != lastMessage)
            {
                Console.WriteLine(message.MessageText);
                lastMessage = message;
            }
            
        }

        public void Update()
        {
            messages = MessageBus.GetMessagesOfType(MessageType.Console);
            foreach (Message message in messages)
            {
                Write(message);
            }
        }
    }
}
