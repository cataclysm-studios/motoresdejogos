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
        //Gameplay Commands
        private Command buttonW = new MoveShipForward();
        private Command buttonA = new StrafeLeft();
        private Command buttonS = new MoveShipBackward();
        private Command buttonD = new StrafeRight();
        private Command buttonQ = new RotateShipLeft();
        private Command buttonE = new RotateShipRight();
        //System Commands
        private Command buttonK = new SaveState();
        private Command buttonL = new LoadState();
        //Camera Commands
        private Command buttonZoomIn = new MoveCamForward();
        private Command buttonZoomDown = new MoveCamBackward();
        private Command buttonCamLeft = new StrafeCamLeft();
        private Command buttonCamRight = new StrafeCamRight();
        private Command buttonCamUp = new MoveCamUp();
        private Command buttonCamDown = new MoveCamDown();
        //private Command buttonR = new Replay();

        private Message inputDebugMessage = new Message(MessageType.Console, "");
        private List<Command> gameplayCommands = new List<Command>();
        private List<Command> systemCommands = new List<Command>();
        public List<Command> usedGameplayCommands = new List<Command>();

        //private bool sentUsedCommands = false;

        //System Input
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
        //Camera Input
        public Command HandleCameraInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
            {
                return buttonZoomIn;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
            {
                return buttonZoomDown;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
            {
                return buttonCamLeft;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
            {
                return buttonCamRight;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad9))
            {
                return buttonCamUp;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3))
            {
                return buttonCamDown;
            }
            return null;
        }
        //Gameplay Input
        public List<Command> HandleGameplayInput()
        {
            KeyboardState newState = Keyboard.GetState();
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

            /*if (Keyboard.GetState().IsKeyDown(Keys.Q))
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
            }*/


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
