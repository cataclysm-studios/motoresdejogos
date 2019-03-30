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
        ResourceManager resourceManager;
        SkyBox skyBox;

        public static Camera cam;
        List<Ship> ships;

        public enum ControlledShip
        {
            Ship0 = 0,
            Ship1,
            Ship2
        }
        ControlledShip shipUnderControl = 0;
        const int TOTALSHIPS = 3;
        InputHandler inputHandler = new InputHandler();
        List<Command> tempCommands = new List<Command>();
        // Create Octree
        float worldSize = Settings.WorldSize;
        Vector3 centerOfWorld = new Vector3(0,0,0);
        Octree octree;
        BasicEffect effect;

        Dice dice = new Dice();
        

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
            octree = new Octree(centerOfWorld, worldSize * 8 + worldSize);
            effect = new BasicEffect(graphics.GraphicsDevice);
            resourceManager = new ResourceManager();
            resourceManager.Initalize();
            DebugShapeRenderer.Initialize(graphics.GraphicsDevice);
            cam = new Camera();
            skyBox = new SkyBox(graphics.GraphicsDevice);
            ships = new List<Ship>();
            MessageBus.Instance.Initialize();
            ConsoleWriter.Instance.Initialize();
            SaveManager.Instance.Initialize();
            for (int i = 0; i < TOTALSHIPS; i++)
            {
                //ships.Insert(i, new Ship(new Vector3(dice.RollDice(-500,500), dice.RollDice(-500, 500), dice.RollDice(-500, 500)),("ship " + (i + 1))));
                ships.Add(new Ship(new Vector3(dice.RollDice(-200, 200), 0, dice.RollDice(-200, 0)), ("ship " + (i + 1))));
                octree.Add(ships[i]);
                //if (i == 0 || i == 1 || i == 2)
                //{
                    ships[i].ShipActive = true;
                //}
            }
            Console.WriteLine(ships.Count);
            octree.Collapse(octree);
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
            resourceManager.LoadModel(Content, 0);
            for (int i = 0; i < TOTALSHIPS; i++)
            {
                ships[i].LoadModel(resourceManager);
            }
            skyBox.LoadResources();
            skyBox = new SkyBox(graphics.GraphicsDevice);
            skyBox.Textures[0] = Content.Load<Texture2D>("skybox/front");
            skyBox.Textures[1] = Content.Load<Texture2D>("skybox/back");
            skyBox.Textures[2] = Content.Load<Texture2D>("skybox/bot");
            skyBox.Textures[3] = Content.Load<Texture2D>("skybox/top");
            skyBox.Textures[4] = Content.Load<Texture2D>("skybox/left");
            skyBox.Textures[5] = Content.Load<Texture2D>("skybox/right");
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
            skyBox.Update(gameTime, cam);
            CheckColissions();
            for (int i = 0; i < TOTALSHIPS; i++)
            {
                octree.ObjectChanged(ships[i]);
                ships[i].drawn = false;
            }

            //ALERTA MARTELO
            if(Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                shipUnderControl = ControlledShip.Ship0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                shipUnderControl = ControlledShip.Ship1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                shipUnderControl = ControlledShip.Ship2;
            }

            //not being used yet
            /*for (int i = 0; i < TOTALSHIPS; i++)
            {
                ships[i].UpdateShip(gameTime);
            }*/


            //System Input
            if (inputHandler.HandleSystemInput() != null)
            {
                inputHandler.HandleSystemInput().Execute(cam, ships, ships[(int)shipUnderControl], gameTime, inputHandler.usedGameplayCommands, resourceManager);
            }
            //Gameplay Input
            if (inputHandler.HandleGameplayInput() != null)
            {
                foreach (Command action in inputHandler.HandleGameplayInput())
                {
                    action.Execute(cam, ships, ships[(int)shipUnderControl], gameTime, inputHandler.usedGameplayCommands, resourceManager);
                }
                //inputHandler.HandleInput().Execute(ships[0], gameTime);
            }
            //Camera Input
            if (inputHandler.HandleCameraInput() != null)
            {
                inputHandler.HandleCameraInput().Execute(cam, ships, ships[(int)shipUnderControl], gameTime, inputHandler.usedGameplayCommands, resourceManager);
            }




            /*
            if(inputHandler.usedGameplayCommands.Count > 0)
            {
                tempCommands.Clear();
                foreach (Command action in inputHandler.usedGameplayCommands)
                {
                    tempCommands.Add(action);
                    //action.Execute(ships[1], gameTime, inputHandler.usedCommands);
                }
                foreach (Command usedReplay in tempCommands)
                {
                    inputHandler.usedGameplayCommands.Remove(usedReplay);
                }
            }*/
            for (int i = 0; i < TOTALSHIPS; i++)
            {
                if(i != (int)shipUnderControl)
                {
                    ships[i].UpdateShip(gameTime);
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
            skyBox.Draw();
            DebugShapeRenderer.Draw(gameTime, cam.View(), cam.Projection());
            //DebugShapeRenderer.AddBoundingBox(new BoundingBox(new Vector3(1, 1, 1), new Vector3(10, 10, 10)), Color.Red);

            octree.DrawBoxLines(/*cam.View(), cam.Projection(), graphics.GraphicsDevice, effect*/);
            // TODO: Add your drawing code here
            /*for (int i = 0; i < ships.Length; i++)
            {
                if(cam.frustum.Intersects(ships[i].boundingSphere))
                {
                    ships[i].Draw(cam.View(), cam.Projection());
                }
                
            }*/
            octree.ModelsDrawn = 0; // this just resets the models drawn every frame (if you want the statistics)
            octree.Draw(cam.View(), cam.Projection(), cam.Frustum());
            //octree.DrawZoneOfDeath(cam.View(), cam.Projection(), graphics.GraphicsDevice,effect);
            
            base.Draw(gameTime);
        }

        

        public void CheckColissions()
        {
            for (int i = 0; i < TOTALSHIPS; i++)
            {
                if(i != (int)shipUnderControl)
                ships[(int)shipUnderControl].CheckCollision(ships[i]);
            }
        }
    }
}
