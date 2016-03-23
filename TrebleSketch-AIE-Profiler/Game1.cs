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

namespace TrebleSketch_AIE_Profiler
{
    /// <summary>
    /// Name: Adelaide Space Adventures
    /// Genre: 2D Profiler
    /// Description: You must play as Adelaide as she must handle the everyday stress of being the head of a national space agency.
    /// Version: 0.1.12 (Developmental Stages)
    /// Developer: Titus Huang (Treble Sketch/ILM126)
    /// Game Engine: MonoGame
    /// Dev Notes: The second MonoGame project for the Academy of Interactive Entertainment (AIE), hope to massively
    /// improve on this game and also to improve on my programming skills for future industry uses.
    /// </summary>

    /// <summary>
    /// BUGS:
    /// - Incomplete Game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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

            // RocketClass - Anything to do with a Rocket goes here
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

            Rocket.Engine.loadEngineTitus(spriteBatch, graphics);

            Rocket.FuelTank.loadFuelTankMedium(spriteBatch, graphics);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
