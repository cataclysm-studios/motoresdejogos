using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class ExplosionCaller
    {
        #region Singleton
        private static ExplosionCaller _explosionCaller;
        public static ExplosionCaller Instance
        {
            get
            {
                if (_explosionCaller == null)
                {
                    _explosionCaller = new ExplosionCaller();
                }
                return _explosionCaller;
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

        public void Explode(Message message)
        {
            ExplosionParticlesSystem.InserirExplosao(message.MessagePosition, message.NumberOfParticles, 1, 1, new Vector3(1, 1, 1));
        }

        public void Update()
        {
            messages = MessageBus.GetMessagesOfType(MessageType.ParticleEffect);
            foreach (Message message in messages)
            {
                Explode(message);
                Console.WriteLine("PUM E TCHE");
            }
        }
    
}
}
