﻿using Microsoft.Xna.Framework;
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
        public enum GameState
        {
            Initializing = 0,
            Ingame,
            TimeIsOver,
        }
        public enum ControlledShip
        {
            Ship0 = 0,
            Ship1,
            Ship2
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ResourceManager resourceManager;
        SkyBox skyBox;
        public static Camera cam;
        List<Ship> ships;
        
        GameState currentGameState = 0;
        ControlledShip shipUnderControl = 0;

        const int TOTALSHIPS = 200;

        float roundTimer = 0f;
        public static float currentScore = 0f;
        public static float maxScore = 0f;
        
        InputHandler inputHandler = new InputHandler();
        List<Command> tempCommands = new List<Command>();

        // Create Octree
        float worldSize = Settings.WorldSize;
        Vector3 centerOfWorld = new Vector3(0, 0, 0);
        Octree octree;

        BasicEffect effect;

        //Dice dice = new Dice();
        Random random = new Random();


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
            currentGameState = GameState.Initializing;
            octree = new Octree(centerOfWorld, worldSize * 8 + worldSize);
            effect = new BasicEffect(graphics.GraphicsDevice);
            resourceManager = new ResourceManager();
            resourceManager.Initalize();
            DebugShapeRenderer.Initialize(graphics.GraphicsDevice);
            cam = new Camera();
            ships = new List<Ship>();
            MessageBus.Instance.Initialize();
            ConsoleWriter.Instance.Initialize();
            SaveManager.Instance.Initialize();
            ExplosionCaller.Instance.Initialize();

            for (int i = 0; i < TOTALSHIPS; i++)
            {
                //ships.Insert(i, new Ship(new Vector3(dice.RollDice(-500,500), dice.RollDice(-500, 500), dice.RollDice(-500, 500)),("ship " + (i + 1))));
                ships.Add(new Ship(Settings.SuggestedEnemyStartingPosition, ("ship " + (i + 1))));
                octree.Add(ships[i]);
                if (i == 0)
                {
                    ships[i].Instantiate(Settings.SuggestedPlayerStartingPosition, Settings.PlayerRotationFactor);
                }
                else if ( i > 0 && i <= Settings.StartingEnemyNumber)
                {
                    ships[i].Instantiate(new Vector3(Settings.SuggestedEnemyStartingPosition.X + Dice.RollDice(-280,280), 0, Settings.SuggestedEnemyStartingPosition.Z + Dice.RollDice(-300, 0)), Settings.EnemyRotationFactor);
                }
                else
                {
                    ships[i].ShipActive = false;
                }
            }
            int c = 0;
            foreach (Ship ship in ships)
            {
                if (ship.ShipActive)
                    c++;
            }
            Console.WriteLine(c);
            ExplosionParticlesSystem.Initialize(random);
            octree.Collapse(octree);
            currentGameState = GameState.Ingame;
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
            skyBox = new SkyBox(graphics.GraphicsDevice);
            skyBox.LoadResources();
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
            //RoundTime(gameTime);
            if (currentGameState == GameState.Ingame)
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
                /*if(Keyboard.GetState().IsKeyDown(Keys.D1))
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
                }*/

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

                ExplosionParticlesSystem.Update(random, gameTime);

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
                    if (i != (int)shipUnderControl)
                    {
                        ships[i].UpdateShip(gameTime);
                    }
                }
                ConsoleWriter.Instance.Update();
                ExplosionCaller.Instance.Update();
                ScoreController.Instance.Update();
            }
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
            //DebugShapeRenderer.Draw(gameTime, cam.View(), cam.Projection());
            //DebugShapeRenderer.AddBoundingBox(new BoundingBox(new Vector3(1, 1, 1), new Vector3(10, 10, 10)), Color.Red);
            ExplosionParticlesSystem.Draw(GraphicsDevice, effect);
            //octree.DrawBoxLines(/*cam.View(), cam.Projection(), graphics.GraphicsDevice, effect*/);
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

        public void RoundTime(GameTime gameTime)
        {
            if(currentGameState == GameState.Ingame)
            {
                roundTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(roundTimer >= Settings.TotalRoundTime)
                {
                    currentGameState = GameState.TimeIsOver;
                    roundTimer = 0f;
                }
            }
            if(currentGameState == GameState.TimeIsOver)
            {
                //show score
                //update maxscore
                if (currentScore > maxScore)
                {
                    maxScore = currentScore;
                }
                //wait x seconds
                //start game again
            }
        }
        public static void UpdateScore()
        {
            currentScore++;
            Console.WriteLine(currentScore);
        }
        public void CheckColissions()
        {
            for (int i = 0; i < TOTALSHIPS; i++)
            {
                if (i != (int)shipUnderControl)
                {
                    if (ships[i].ShipActive == true)
                    {
                        ships[(int)shipUnderControl].CheckCollision(ships[i]);
                    }
                }
            }
        }
    }
}
