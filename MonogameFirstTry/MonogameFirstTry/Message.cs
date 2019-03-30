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


        public Message(MessageType messageType, string messageText)
        {
            this.messageType = messageType;
            this.messageText = messageText;
        }
        public Message(MessageType messageType, Vector3 position, int numberOfParticles)
        {
            this.messageType = messageType;
            this.position = position;
            this.numberOfParticles = numberOfParticles;
        }

    }
}
