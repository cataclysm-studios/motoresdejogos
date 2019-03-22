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
        private Command buttonQ = new StrafeLeft();
        private Command buttonE = new StrafeRight();
        private Command buttonK = new SaveState();
        private Command buttonL = new LoadState();
        //private Command buttonR = new Replay();
        private Message inputDebugMessage = new Message(MessageType.Console, "");
        private List<Command> gameplayCommands = new List<Command>();
        private List<Command> systemCommands = new List<Command>();
        public List<Command> usedGameplayCommands = new List<Command>();

        //private bool sentUsedCommands = false;
        public Command HandleSystemInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                inputDebugMessage.MessageText = "K";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                return buttonK;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                inputDebugMessage.MessageText = "L";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                return buttonL;
            }
            return null;
        }
        public List<Command> HandleGameplayInput()
        {
            gameplayCommands.Clear();
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
                gameplayCommands.Add(buttonW);
                usedGameplayCommands.Add(buttonW);
                //return buttonW;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                inputDebugMessage.MessageText = "A";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                gameplayCommands.Add(buttonA);
                usedGameplayCommands.Add(buttonA);
                //return buttonA;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                inputDebugMessage.MessageText = "S";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                gameplayCommands.Add(buttonS);
                usedGameplayCommands.Add(buttonS);
                //return buttonS;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                inputDebugMessage.MessageText = "D";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                gameplayCommands.Add(buttonD);
                usedGameplayCommands.Add(buttonD);
                //return buttonD;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                inputDebugMessage.MessageText = "Q";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                gameplayCommands.Add(buttonQ);
                usedGameplayCommands.Add(buttonQ);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                inputDebugMessage.MessageText = "E";
                MessageBus.Instance.AddMessage(inputDebugMessage);
                gameplayCommands.Add(buttonE);
                usedGameplayCommands.Add(buttonE);
            }
            if (gameplayCommands.Count > 0)
            {
                //inputDebugMessage.MessageText = commands.Count.ToString() + " " + usedCommands.Count.ToString();
               // MessageBus.Instance.AddMessage(inputDebugMessage);
                return gameplayCommands;
            }
            else return null;
        }
    }
}
