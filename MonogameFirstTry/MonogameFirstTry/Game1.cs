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
        // Create Octree
        float worldSize = Settings.worldSize;
        Vector3 centerOfWorld = new Vector3(0,0,0);
        Octree octree;
        BasicEffect effect;

        Dice dice = new Dice();

        const int TOTALSHIPS = 10;

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
            shipModel = new ShipModel();
            DebugShapeRenderer.Initialize(graphics.GraphicsDevice);
            cam = new Camera();
<<<<<<< HEAD
            ships = new Ship[TOTALSHIPS];
=======
            ships = new Ship[50];
>>>>>>> 9c9af8e3556935e25051118952900daeecd96fba
            MessageBus.Instance.Initialize();
            ConsoleWriter.Instance.Initialize();
            SaveManager.Instance.Initialize();
            for (int i = 0; i < ships.Length; i++)
            {
<<<<<<< HEAD
                ships[i] = new Ship(new Vector3(-TOTALSHIPS * 15 + i * 40, 0, 0), ("ship " + (i + 1)));
=======
                ships[i] = new Ship(new Vector3(dice.RollDice(-500,500), dice.RollDice(-500, 500), dice.RollDice(-500, 500)));
                octree.Add(ships[i]);
>>>>>>> 9c9af8e3556935e25051118952900daeecd96fba
                if (i == 0 || i == 1)
                {
                    ships[i].ShipActive = true;
                }
            }
<<<<<<< HEAD
            
=======
            octree.Collapse(octree);
>>>>>>> 9c9af8e3556935e25051118952900daeecd96fba
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
            for (int i = 0; i < TOTALSHIPS; i++)
            {
                ships[i].LoadModel(shipModel);
            }
            SaveManager.Instance.SaveShipStates(ships);
            SaveManager.Instance.ConvertToJson();
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

            //CheckColissions();
            for (int i = 0; i < ships.Length; i++)
            {
                octree.ObjectChanged(ships[i]);
                ships[i].drawn = false;
            }
            
            // TODO: Add your update logic here
            for (int i = 0; i < TOTALSHIPS; i++)
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

            DebugShapeRenderer.Draw(gameTime, cam.View(), cam.Projection());
            //DebugShapeRenderer.AddBoundingBox(new BoundingBox(new Vector3(1, 1, 1), new Vector3(10, 10, 10)), Color.Red);

            octree.DrawBoxLines(/*cam.View(), cam.Projection(), graphics.GraphicsDevice, effect*/);
            // TODO: Add your drawing code here
<<<<<<< HEAD
            for (int i = 0; i < TOTALSHIPS; i++)
=======
            /*for (int i = 0; i < ships.Length; i++)
>>>>>>> 9c9af8e3556935e25051118952900daeecd96fba
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
            for (int i = 1; i < TOTALSHIPS; i++)
            {
                ships[0].CheckOctreeCollision(octree.bounds);
            }
        }
    }
}
