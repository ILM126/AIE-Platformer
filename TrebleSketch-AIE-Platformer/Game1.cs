using System;
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
    /// <summary>
    /// Name: Space Program Simulator 2016
    /// Genre: 2D Platformer
    /// Description: You must play as Treble Sketch or Adelaide as either of them must handle the everyday stress of being the head of
    /// a starting national space agency.
    /// Version: 0.0.2.75 (Developmental Stages)
    /// Developer: Titus Huang (Treble Sketch/ILM126)
    /// Game Engine: MonoGame
    /// Dev Notes: The second MonoGame project for the Academy of Interactive Entertainment (AIE) Cert II C# Course, hope to massively
    /// improve on my game design/programming skills. Yes, this game does contain ponies. As suggested in the title.
    /// </summary>

    /// <summary>
    /// TO DO:
    /// - Player-to-Surface Collisions
    /// - Scene Movements
    /// - Scene Transitions
    /// - Speech System
    /// - Make Game "First Playable" by 6th of April, 2016.
    /// - Follow Rainlender's Schedule
    /// </summary>

    /// <summary>
    /// BUGS:
    /// - Incomplete Game
    /// - When you press D/A and then
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
        AudioClass Audio;
        DevLogging Debug;

        // "Start" the fonts
        // SpriteFont InformationFont;

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
            Player = new PlayerClass();
            Player.InitializeTrebleSketch(graphics);
            Player.PlayerFacingRight = true;

            Rocket = new RocketClass();
            World = new WorldClass();

            Scene = new SceneClass();

            Audio = new AudioClass();

            Debug = new DevLogging();

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

            // PlayerClass - Loads Treble Sketch and Adelaide Player Sprites
                // Treble Sketch
                Player.FaceLeft = Content.Load<Texture2D>("Player/treble-sketch_stand_left");
                Player.FaceRight = Content.Load<Texture2D>("Player/treble-sketch_stand_right");

                // Adelaide

            // RocketClass - Loads all rocket components
                // Engines
                Rocket.Engine.Titus = Content.Load<Texture2D>("Rocket/engine-Titus-v1");

                // Fuel Tanks
                Rocket.FuelTank.Medium = Content.Load<Texture2D>("Rocket/fuelTank-Medium-v1");

                // Capsules
            
            // WorldClass - Loads the planets and moons
                // Planets

                // Moons

            // SurfaceClass - Loads the Space Centre
                // Stage 01

                // Stage 02

                // Stage 03

            // AudioClass - Loads the Sounds and Sound Effects
                // Rocket

                // People

                // Music

            // Extra Assets - Loads the extra fonts and what no
                // Fonts
                // InformationFont = Content.Load<SpriteFont>("");
                Debug.DebugFont = Content.Load<BitmapFont>("debugfont");

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

            Player.PlayerMovement();

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

            spriteBatch.Begin();

            Rocket.Engine.loadEngineTitus(spriteBatch, graphics);

            Rocket.FuelTank.loadFuelTankMedium(spriteBatch, graphics);

            if (Keyboard.GetState().IsKeyDown(Keys.A) && Player.BothSidesPressed == false)
            {
                Player.loadPlayerTrebleSketchLeft(spriteBatch, graphics);
                Player.PlayerFacingRight = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && Player.BothSidesPressed == false)
            {
                Player.loadPlayerTrebleSketchRight(spriteBatch, graphics);
                Player.PlayerFacingRight = true;
            }

            if (Player.PlayerFacingRight)
            {
                Player.loadPlayerTrebleSketchRight(spriteBatch, graphics);
            }
            else if (Player.PlayerFacingRight == false)
            {
                Player.loadPlayerTrebleSketchLeft(spriteBatch, graphics);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
