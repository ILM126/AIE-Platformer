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
        public int InScene;

        // Player Movement
        public bool PlayerFacingRight;
        public bool BothSidesPressed;
        // public bool IsJumping;
        // public bool IsGrounded;

        public void InitializeTrebleSketch(GraphicsDeviceManager graphics)
        {
            PlayerFacingRight = true;
            PlayerFacingRight = false;
            BothSidesPressed = false;

            // Player.SpawnPosition = Player.Position;
            Position = new Vector2(graphics.PreferredBackBufferWidth / 2
                    , graphics.PreferredBackBufferHeight / 2);
            Velocity = new Vector2(0, 0);
            Acceleration = Velocity.X;
            // Player.Size = new Vector2(85.0f, 85.0f);
        }

        public void PlayerMovement()
        {
            Velocity = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A) && PlayerFacingRight == false) // Press A
            {
                Velocity = new Vector2(-2.5f, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && PlayerFacingRight) // Press D
            {
                Velocity = new Vector2(2.5f, 0);
            }

            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
            {
                BothSidesPressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                BothSidesPressed = true;
            }

            Position += Velocity;
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

        public void RenderPlayer(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) && BothSidesPressed == false)
            {
                loadPlayerTrebleSketchLeft(spriteBatch, graphics);
                PlayerFacingRight = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && BothSidesPressed == false)
            {
                loadPlayerTrebleSketchRight(spriteBatch, graphics);
                PlayerFacingRight = true;
            }
            if (PlayerFacingRight)
            {
                loadPlayerTrebleSketchRight(spriteBatch, graphics);
            }
            else if (PlayerFacingRight == false)
            {
                loadPlayerTrebleSketchLeft(spriteBatch, graphics);
            }
        }
    }
}
