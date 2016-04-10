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
    class PlayerClass
    {
        // Player Avaliable Textures
        public Texture2D FaceRight;
        public Texture2D FaceLeft;

        // Player Values
        //public Vector2 SpawnPosition;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Acceleration;

        public float Rotation;

        //public Vector2 Size;

        // Player Scene Stuff
        float Gravity;
        public int InScene;
        public int FromScene;
        public int ToScene;

        // Player Movement
        public bool PlayerFacingRight;
        public bool BothSidesPressed;
        bool IsJumping;
        bool IsGrounded;
        bool isGrounded;

        public void InitializeTrebleSketch(GraphicsDeviceManager graphics)
        {
            PlayerFacingRight = true;
            BothSidesPressed = false;

            // Player.SpawnPosition = Player.Position;
            Position = new Vector2(graphics.PreferredBackBufferWidth / 2
                    , graphics.PreferredBackBufferHeight / 2);
            Velocity = new Vector2(0, 0);
            Acceleration = Velocity.X;
            Gravity = 50f;
            // Player.Size = new Vector2(85.0f, 85.0f);
        }

        void PlayerMovement(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity = new Vector2(0, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.A) && !BothSidesPressed)
            {
                loadPlayerTrebleSketchLeft(spriteBatch, graphics);
                PlayerFacingRight = false;
                Velocity.X = -4.8f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && !BothSidesPressed)
            {
                loadPlayerTrebleSketchRight(spriteBatch, graphics);
                PlayerFacingRight = true;
                Velocity.X = 4.8f;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
            {
                BothSidesPressed = false;

                if (PlayerFacingRight) loadPlayerTrebleSketchRight(spriteBatch, graphics);
                else if (!PlayerFacingRight) loadPlayerTrebleSketchLeft(spriteBatch, graphics);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                BothSidesPressed = true;

                if (PlayerFacingRight) loadPlayerTrebleSketchRight(spriteBatch, graphics);
                else if (!PlayerFacingRight) loadPlayerTrebleSketchLeft(spriteBatch, graphics);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !IsJumping)
            {
                Jump();
            }

            if (!isGrounded) Velocity.Y += Gravity * time;
            else Velocity.Y = 0;

            Position.Y += Velocity.Y * time;
            // isGrounded = false;

            Position += Velocity;
        }

        public void Jump()
        {
            if (isGrounded)
            {
                IsJumping = true;
                Velocity.Y = -100f;
            }
        }

        public void loadPlayerTrebleSketchRight(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(FaceRight
                , Position
                , null
                , Color.White
                , 0
                , new Vector2(FaceRight.Width / 2
                    , FaceRight.Height / 2)
                , new Vector2(1, 1)
                , SpriteEffects.None
                , 0);
        }

        public void loadPlayerTrebleSketchLeft(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(FaceLeft
                , Position
                , null
                , Color.White
                , 0
                , new Vector2(FaceLeft.Width / 2
                    , FaceLeft.Height / 2)
                , new Vector2(1, 1)
                , SpriteEffects.None
                , 0);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            PlayerMovement(gameTime, spriteBatch, graphics);
        }
    }
}
