﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using MonoGame.Extended.BitmapFonts;

namespace TrebleSketch_AIE_Platformer
{

    /// Notes for Reece:
    /// 01. Currently, as of 13st of April 2016, there are no:
    ///     Collisions
    ///     Sounds
    ///     SFX
    ///     Particle Systems
    ///     Scrolling
    ///     And others
    /// Coding ftw

    /// <summary>
    /// Name: Space Program Simulator 2016
    /// Genre: 2D Platformer
    /// Description: You must play as Treble Sketch or Adelaide as either of them must handle the everyday stress of being the head of
    /// a starting national space agency.
    /// Version: 0.0.7.108 (Developmental Stages, plus 10 builds before Git)
    /// Developer: Titus Huang (Treble Sketch/ILM126)
    /// Game Engine: MonoGame/XNA
    /// Language: C#
    /// Dev Notes: The second MonoGame project for the Academy of Interactive Entertainment (AIE) Cert II C# Course, hope to massively
    /// improve on my game design/programming skills. Yes, this game does contain ponies.
    /// </summary>

    /// <summary>
    /// TO DO:
    /// - Weapons System
    /// - Scene Movements
    /// - Scene Transitions
    /// - Speech System
    /// </summary>

    /// <summary>
    /// BUGS / UPCOMING FEATURES:
    /// - Incomplete Game
    /// - (Text) Currently using BitmapFonts, unable to change size. Need to convert to SpriteFonts
    /// - (Textures) Convert all textures to v2, where the standard for the scene is 50x50, 50x75 for rocket engines, players are 75x75, ect. All increments of 25.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // "Start" the Classes
        PlayerClass Player;
        RocketClass Rocket;
        WorldClass World;
        SceneClass Scene;
        LoadScene SceneLoad;
        SceneObjects SceneObject;
        AudioClass Audio;
        DevLogging Debug;

        Texture2D[] rocketParts = new Texture2D[4];

        public Vector2 CentreScreen;

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

            Console.WriteLine("[INFO] Started Initializing Game");

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            CentreScreen = new Vector2(graphics.PreferredBackBufferWidth / 2
                , graphics.PreferredBackBufferHeight / 2);

            Debug = new DevLogging();
            Debug.ShowDebug();

            Player = new PlayerClass();
            Player.SpawnPosition = CentreScreen;
            Player.InitialisePlayer();
            Player.InitializeTrebleSketch(graphics);

            Rocket = new RocketClass();

            Rocket.SpawnPosition = CentreScreen;

            World = new WorldClass();

            Scene = new SceneClass();
            SceneObject = new SceneObjects();
            Scene.InitiateSurface();
            SceneLoad = new LoadScene();
            SceneLoad.InitialiseScene(graphics);

            Audio = new AudioClass();
            Audio.Debug = Debug;

            Console.WriteLine("[INFO] Finished Initializing Game");

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Console.WriteLine("[INFO] Started Loading Game Textures");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // PlayerClass - Loads Treble Sketch and Adelaide Player Sprites
                // Treble Sketch
                Player.FaceLeft = Content.Load<Texture2D>("Player/treble-sketch_stand_left");
                Player.FaceRight = Content.Load<Texture2D>("Player/treble-sketch_stand_right");

                // Adelaide

            // RocketClass - Loads all rocket components
                // Engines
                rocketParts[0] = Content.Load<Texture2D>("Rocket/engine-Titus-v2");

                // Fuel Tanks
                rocketParts[1] = Content.Load<Texture2D>("Rocket/fuelTank-Medium-v1");

                // Capsules

            // WorldClass - Loads the planets and moons
                // Planets

                // Moons

            // SurfaceClass - Loads the Space Centre
                // 00 - Test Map
                SceneLoad.OutsideGrass = Content.Load<Texture2D>("Surface/surface-dirt1-v1");    

                // 01 - Reception

                // 02 - Front Lawns

                // 03 - Conference Room

            // AudioClass - Loads the Sounds and Sound Effects
                // Rocket

                // People

                // Music
                Audio.Bright_DJStartchAttack = Content.Load<Song>("Audio/Bright by DJStratchAttack");

            // Extra Assets - Loads the extra fonts and what no
                // Fonts
                Debug.InformationFont = Content.Load<BitmapFont>("informationfont");
                Debug.DebugFont = Content.Load<BitmapFont>("debugfont");

                SceneObject.scene_TextureError = Content.Load<Texture2D>("scene-errorTexturev1");

            Console.WriteLine("[INFO] Finished Loading Game Textures");

            RocketParts();
        }

        public void RocketParts()
        {
            Rocket.Position = CentreScreen;
            RocketPart FuelTank = new RocketPart(rocketParts[1], Rocket.SpawnPosition, new Vector2(rocketParts[1].Width, rocketParts[1].Height));
            RocketPart Engine = new RocketPart(rocketParts[0], Rocket.SpawnPosition, new Vector2(rocketParts[0].Width, rocketParts[0].Height));
            // RocketPart Capsule = new RocketPart(/*rocketParts[2]*/);
            Rocket.AddPart(FuelTank);
            Rocket.AddPart(Engine);
            // Rocket.AddPart(Capsule);
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
            Rocket.Update(gameTime);
            Audio.ToggleMusic(gameTime);

            SceneLoad.CheckCollisions(Player);

            Player.Update(gameTime);
            Player.IsGrounded = false;
            base.Update(gameTime);
        }

        private void GameBuild() // Displays the Game Build Number on the lower right hand side
        {
            // spriteBatch.DrawString(InformationFont, "SCORE : " + Ship.Score.ToString(), new Vector2(10, 10), Color.White);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // Console.WriteLine("[INFO] Started Drawing Game Textures");

            spriteBatch.Begin();

            // Console.WriteLine("[INFO] Drawing Rocket Textures");
            Rocket.Draw(spriteBatch);

            // Console.WriteLine("[INFO] Drawing Player Textures");
            Player.Draw(spriteBatch, graphics);

            // Console.WriteLine("[INFO] Drawing Scene Textures");
            SceneLoad.Draw(gameTime, spriteBatch);

            // Console.WriteLine("[INFO] Drawing Audio Name");
            Audio.CurrentSong(spriteBatch);

            // Debug.InGameDebug(spriteBatch);

            spriteBatch.End();

            // Console.WriteLine("[INFO] Finshed Drawing Game Textures");

            base.Draw(gameTime);
        }
    }
}
