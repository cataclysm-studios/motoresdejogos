using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonogameFirstTry
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ShipModel shipModel;
        Camera cam;
        Ship[] ships;
        InputHandler inputHandler = new InputHandler();
        List<Command> tempCommands = new List<Command>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            shipModel = new ShipModel();
            cam = new Camera();
            ships = new Ship[10];
            MessageBus.Instance.Initialize();
            ConsoleWriter.Instance.Initialize();
            for (int i = 0; i < ships.Length; i++)
            {
                ships[i] = new Ship(new Vector3(-ships.Length * 15 + i * 40, 0, 0));
                if (i == 0 || i == 1)
                {
                    ships[i].ShipActive = true;
                }
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            shipModel.LoadShipModel(Content);
            for (int i = 0; i < ships.Length; i++)
            {
                ships[i].LoadModel(shipModel);
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CheckColissions();


            // TODO: Add your update logic here
            for (int i = 0; i < ships.Length; i++)
            {
                ships[i].UpdateShip(gameTime);
            }
            if(inputHandler.HandleInput() != null)
            {
                foreach (Command action in inputHandler.HandleInput())
                {
                    action.Execute(ships[0], gameTime, inputHandler.usedCommands);
                }
                //inputHandler.HandleInput().Execute(ships[0], gameTime);
            }
            if(inputHandler.usedCommands.Count > 0)
            {
                tempCommands.Clear();
                foreach (Command action in inputHandler.usedCommands)
                {
                    tempCommands.Add(action);
                    //action.Execute(ships[1], gameTime, inputHandler.usedCommands);
                }
                foreach (Command usedReplay in tempCommands)
                {
                    inputHandler.usedCommands.Remove(usedReplay);
                }
            }
            ConsoleWriter.Instance.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            for (int i = 0; i < ships.Length; i++)
            {
                ships[i].DrawShip(cam.View(),cam.Projection());
            }
            base.Draw(gameTime);
        }

        public void CheckColissions()
        {
            for (int i = 1; i < ships.Length; i++)
            {
                ships[0].isColliding(ships[i].boundingSphere, i + 1);
            }
        }
    }
}
