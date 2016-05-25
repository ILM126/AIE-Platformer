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
using EclipsingGameUtils;

namespace TrebleSketch_AIE_Platformer
{
    class PlayerClass
    {
        public SquareCollision BoxCollision;
        public DevLogging Debug;

        // Player Textures (Treble Sketch only)
        public Texture2D FaceRight;
        public Texture2D FaceLeft;

        // Player Values
        public Vector2 SpawnPosition;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Vector2 Size;
        public float Acceleration;
        public float Rotation;

        // Player Scene Stuff
        public float Gravity;
        public float GroundHeight;
        public float Scale;
        public int InScene;
        public int FromScene;
        public int ToScene;

        // Player Movement
        public bool PlayerFacingRight;
        public bool BothSidesPressed;
        bool IsJumping;
        public bool IsGrounded;
        float JumpForce;
        public bool PlayerInScene;
        public bool PewPew;
        public bool JumpingDown;
        TimeSpan HalfSecond = new TimeSpan(0, 0, 0, 0, 500);
        TimeSpan FiftyMilliseconds = new TimeSpan(0, 0, 0, 0, 50);
        TimeSpan SeventyFiveMilliseconds = new TimeSpan(0, 0, 0, 0, 75);
        TimeSpan lastJumpingDown;

        public void InitialisePlayer()
        {
            BoxCollision = new SquareCollision(Position, Size);

            IsGrounded = false;
            IsJumping = false;

            PlayerFacingRight = true;
            BothSidesPressed = false;

            Position = new Vector2(SpawnPosition.X
                , SpawnPosition.Y * 2 - 180);
            Velocity = new Vector2(0);
            Size = new Vector2(80, 80);
            Origin = new Vector2(
                (int)Size.X / 2,
                (int)Size.Y / 2);
            Acceleration = Velocity.X;
            Rotation = 0;
        }

        public void loadPlayerTrebleSketchRight(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(FaceRight
                , Position
                , null
                , Color.White
                , 0
                , new Vector2(Origin.X
                        , Origin.Y)
                , new Vector2(Scale
                        , Scale)
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
                , new Vector2(Origin.X
                        , Origin.Y)
                , new Vector2(Scale
                        , Scale)
                , SpriteEffects.None
                , 0);
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                IsJumping = true;
                IsGrounded = false;
                Velocity.Y -= 425f * Scale;
            }
        }

        public void JumpDown()
        {
            if (IsGrounded)
            {
                IsJumping = true;
                IsGrounded = false;
                JumpingDown = true;
                Velocity.Y += 475f * Scale;
            }
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
            {
                if (IsGrounded)
                {
                    Velocity = new Vector2(0);
                }
            }

            if (InputHandler.IsKeyDownOnce(Keys.A)) // Move Left
            {
                Velocity.X = -2.7f;
                PlayerFacingRight = false;
            }

            if (InputHandler.IsKeyDownOnce(Keys.D)) // Move Right
            {
                Velocity.X = 2.7f;
                PlayerFacingRight = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && IsGrounded) Jump(); // Jump!

            if (InputHandler.IsKeyDownOnce(Keys.Space)) // Pew Pew
            {
                PewPew = true;
            }

            TimeSpan lastSwitch = gameTime.TotalGameTime - lastJumpingDown;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Position.Y < 200)
                {
                    if (lastSwitch > HalfSecond)
                    {
                        JumpDown();
                        lastJumpingDown = gameTime.TotalGameTime;
                    }
                } else if (200 < Position.Y && Position.Y < 500) {
                    if (lastSwitch > SeventyFiveMilliseconds)
                    {
                        JumpDown();
                        lastJumpingDown = gameTime.TotalGameTime;
                    }
                } else if (Position.Y > SpawnPosition.Y * 2 - 150)
                {
                    JumpingDown = false;
                }
            }
            else if (JumpingDown)
            {
                if (Position.Y < 200)
                {
                    if (lastSwitch > HalfSecond)
                    {
                        JumpingDown = false;
                    }
                } else if (200 < Position.Y && Position.Y < 500)
                {
                    if (lastSwitch > SeventyFiveMilliseconds)
                    {
                        JumpingDown = false;
                    }
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.B)) if (!IsGrounded) { Position = SpawnPosition; Velocity = new Vector2(0); JumpingDown = false; Console.WriteLine("[Player] Spawned at " + Position.ToPoint()); } // Get Your Pony Ass Back Here Treble!

            if (!IsGrounded) Velocity.Y += Gravity * time;
            else Velocity.Y = 0;
            Position.Y += Velocity.Y * time;
            Position.X += Velocity.X;
            UpdateBounds();
            // Debug.WriteToFile("Player is being updated on screen");
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            Rectangle srcRect = new Rectangle(
                                                0,
                                                0,
                                                (int)(Size.X),
                                                (int)(Size.Y));

            if (PlayerFacingRight) loadPlayerTrebleSketchRight(spriteBatch, graphics);
            else if (!PlayerFacingRight) loadPlayerTrebleSketchLeft(spriteBatch, graphics);

            if (BothSidesPressed)
            {
                if (PlayerFacingRight) loadPlayerTrebleSketchRight(spriteBatch, graphics);
                else if (!PlayerFacingRight) loadPlayerTrebleSketchLeft(spriteBatch, graphics);
            }

            if (PewPew)
            {
                if (PlayerFacingRight)
                {
                    spriteBatch.DrawString(Debug.InformationFont, "Pew Pew!", new Vector2(Position.X + 10, Position.Y - 10), Color.Black);
                } else if (!PlayerFacingRight)
                {
                    spriteBatch.DrawString(Debug.InformationFont, "Pew Pew!", new Vector2(Position.X - 50, Position.Y - 10), Color.Black);
                }
                Debug.WriteToFile("Pew Pew is activated", false);
            }
        }

        protected virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            BoxCollision = new SquareCollision(Position, Size * Scale);
        }

        protected bool SquareCollisionCheck(SceneObjects pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
        }

        //protected bool SquareCollisionCheck(RocketClass pOther)
        //{
        //    return BoxCollision.CollsionCheck(pOther.BoxCollision);
        //}

        protected void SetGrounded(float groundHeight)
        {
            IsGrounded = true;
            IsJumping = false;
            JumpingDown = false;
            GroundHeight = groundHeight;
            Position.Y = groundHeight;
            Velocity.Y = 0;
            UpdateBounds();
        }

        public bool CheckCollisionsGround(SceneObjects other)
        {
            bool sceneCollision = SquareCollisionCheck(other);
            if (sceneCollision)
            {
                if (!JumpingDown)
                {
                    /// if player position is above top of ground and player is falling
                    if (Position.Y < other.BoxCollision.min.Y && Velocity.Y > 0)
                    {
                        SetGrounded(other.BoxCollision.min.Y - Origin.Y * Scale);
                    }
                    return true;
                }
            }
            return false;
        }

        //public bool CheckCollisionsGround(RocketClass other)
        //{
        //    bool rocketCollision = SquareCollisionCheck(other);
        //    if (rocketCollision)
        //    {
        //        /// if player position is above top of ground and player is falling
        //        if (Position.Y < other.BoxCollision.min.Y && Velocity.Y > 0)
        //        {
        //            SetGrounded(other.BoxCollision.min.Y - Origin.Y * Scale);
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        public bool CollisionCheck(SceneObjects other)
        {
            bool playerCollision = SquareCollisionCheck(other);
            if (playerCollision)
            {
                return true;
            }
            return false;
        }
    }
}
