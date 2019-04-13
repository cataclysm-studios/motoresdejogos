using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    class ScoreController
    {
        #region Singleton
        private static ScoreController _scoreController;
        public static ScoreController Instance
        {
            get
            {
                if (_scoreController == null)
                {
                    _scoreController = new ScoreController();
                }
                return _scoreController;
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

        public void UpdateScore(Message message)
        {
            Game1.UpdateScore();
        }

        public void Update()
        {
            messages = MessageBus.GetMessagesOfType(MessageType.UpdateScore);
            foreach (Message message in messages)
            {
                UpdateScore(message);
            }
        }

    }
}
