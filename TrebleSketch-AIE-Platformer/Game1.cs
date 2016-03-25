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

namespace TrebleSketch_AIE_Platformer
{
    /// <summary>
    /// Name: Pony Space Simulator
    /// Genre: 2D Platformer
    /// Description: You must play as Treble Sketch or Adelaide as either of them must handle the everyday stress of being the head of
    /// a starting national space agency.
    /// Version: 0.1.22 (Developmental Stages)
    /// Developer: Titus Huang (Treble Sketch/ILM126)
    /// Game Engine: MonoGame
    /// Dev Notes: The second MonoGame project for the Academy of Interactive Entertainment (AIE) Cert II C# Course, hope to massively
    /// improve on my game design/programming skills. Yes, this game does contain ponies. As suggested in the title.
    /// </summary>

    /// <summary>
    /// TO DO:
    /// - Make Game "First Playable" by 30th of March, 2016.
    /// - Follow Rainlender's Schedule
    /// </summary>
    
    /// <summary>
    /// BUGS:
    /// - Incomplete Game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerClass Player;
        RocketClass Rocket;
        WorldClass World;
        SurfaceClass Surface;
        AudioClass Audio;

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
            Rocket = new RocketClass();
            World = new WorldClass();
            Surface = new SurfaceClass();
            Audio = new AudioClass();


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
                Player.TrebleSketch.FaceLeft = Content.Load<Texture2D>("Player/treble-sketch_stand_left");
                Player.TrebleSketch.FaceRight = Content.Load<Texture2D>("Player/treble-sketch_stand_right");

                // Adelaide

            // RocketClass - Loads all rocket components
                // Engines
                // Rocket.Engine.Titus = Content.Load<Texture2D>("Rocket/engine-Titus-v1");

                // Fuel Tanks
                // Rocket.FuelTank.Medium = Content.Load<Texture2D>("Rocket/fuelTank-Medium-v1");

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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            // Rocket.Engine.loadEngineTitus(spriteBatch, graphics);

            // Rocket.FuelTank.loadFuelTankMedium(spriteBatch, graphics);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Player.TrebleSketch.loadPlayerTrebleSketchLeft(spriteBatch, graphics);
                Player.TrebleSketch.PlayerFacingRight = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Player.TrebleSketch.loadPlayerTrebleSketchRight(spriteBatch, graphics);
                Player.TrebleSketch.PlayerFacingRight = true;
            }
            else if (Player.TrebleSketch.PlayerFacingRight == false)
            {
                Player.TrebleSketch.loadPlayerTrebleSketchLeft(spriteBatch, graphics);
            }
            else if (Player.TrebleSketch.PlayerFacingRight == true)
            {
                Player.TrebleSketch.loadPlayerTrebleSketchRight(spriteBatch, graphics);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
