using Microsoft.Xna.Framework;
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

        protected Vector3 position;

        public Vector3 MessagePosition
        {
            get { return position; }
            set { position = value; }
        }

        protected int numberOfParticles;

        public int NumberOfParticles
        {
            get { return numberOfParticles; }
            set { numberOfParticles = value; }
        }

        /// <summary>
        /// Message constructor for Console type
        /// </summary>
        public Message(MessageType messageType, string messageText)
        {
            this.messageType = messageType;
            this.messageText = messageText;
        }

        /// <summary>
        /// Message constructor for ParticleEffects type
        /// </summary>
        public Message(MessageType messageType, Vector3 position, int numberOfParticles)
        {
            this.messageType = messageType;
            this.position = position;
            this.numberOfParticles = numberOfParticles;
        }

        /// <summary>
        /// Message constructor for UpdateScore
        /// </summary>
        public Message(MessageType messageType)
        {
            this.messageType = messageType;
        }
    }
}
