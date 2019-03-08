using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class InputHandler
    {
        private Command buttonW = new MoveShipForward();
        private Command buttonA = new RotateShipLeft();
        private Command buttonS = new MoveShipBackward();
        private Command buttonD = new RotateShipRight();
        //private Command buttonR = new Replay();
        private Message inputDebugMessage = new Message(MessageType.Console, "");
        private List<Command> commands = new List<Command>();
        public List<Command> usedCommands = new List<Command>();
        private bool sentUsedCommands = false;

        public List<Command> HandleInput()
        {
            commands.Clear();
            //usedCommands.Clear();
            /*if(sentUsedCommands)
            {
                usedCommands.Clear();
                sentUsedCommands = false;
            }*/
            /*if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                inputDebugMessage.MessageText = "R";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                usedCommands.Insert(0, buttonR);
                sentUsedCommands = true;
                return usedCommands;
            }*/
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                inputDebugMessage.MessageText = "W";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                commands.Add(buttonW);
                usedCommands.Add(buttonW);
                //return buttonW;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                inputDebugMessage.MessageText = "A";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                commands.Add(buttonA);
                usedCommands.Add(buttonA);
                //return buttonA;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                inputDebugMessage.MessageText = "S";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                commands.Add(buttonS);
                usedCommands.Add(buttonS);
                //return buttonS;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                inputDebugMessage.MessageText = "D";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                commands.Add(buttonD);
                usedCommands.Add(buttonD);
                //return buttonD;
            }
            if (commands.Count > 0)
            {
                inputDebugMessage.MessageText = commands.Count.ToString() + " " + usedCommands.Count.ToString();
                MessageBus.Instance.AddMessage(inputDebugMessage);
                return commands;
            }
            else return null;
        }
    }
}
