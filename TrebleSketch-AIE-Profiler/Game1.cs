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
    /// Version: 0.0.01 (Developmental Stages)
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
                //Rocket.engineTitus = Content.Load<Texture2D>("Rocket/engine-Titus-v1");

                // Fuel Tanks
                //Rocket.fuelTanksMedium = Content.Load<Texture2D>("Rocket/fuelTank-Medium-v1");

                // Capsules


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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(Rocket.engineTitus
                , new Vector2(0, 0)
                , null
                , Color.White
                , 0
                , new Vector2(Rocket.engineTitus.Width / 2
                    , Rocket.engineTitus.Height / 2)
                , new Vector2( / Rocket.engineTitus.Width
                    , 50 / Rocket.engineTitus.Height)
                , SpriteEffects.None
                , 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
