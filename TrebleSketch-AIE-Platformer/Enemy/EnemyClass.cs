﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using EclipsingGameUtils;

namespace TrebleSketch_AIE_Platformer
{
    class EnemyClass
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

        public void InitialisePlayer()
        {
            BoxCollision = new SquareCollision(Position, Size);

            IsGrounded = false;
            IsJumping = false;

            PlayerFacingRight = true;
            BothSidesPressed = false;
            IsGrounded = false;
            IsJumping = false;

            Position = new Vector2(SpawnPosition.X
                , SpawnPosition.Y);
            Velocity = new Vector2(0);
            Size = new Vector2(80, 80);
            Origin = new Vector2(
                (int)Size.X / 2,
                (int)Size.Y / 2);
            //Size = new Vector2(80, 80);
            //Velocity = new Vector2(0);
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
                Velocity.Y -= 400f;
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
                Velocity.X = -4.8f;
                PlayerFacingRight = false;
            }

            if (InputHandler.IsKeyDownOnce(Keys.D)) // Move Right
            {
                Velocity.X = 4.8f;
                PlayerFacingRight = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && IsGrounded) Jump(); // Jump!

            if (InputHandler.IsKeyDownOnce(Keys.Space)) // Pew Pew
            {
                PewPew = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.B)) if (!IsGrounded) { Position = SpawnPosition; Velocity = new Vector2(0); Debug.WriteToFile("[Player] Spawned at " + Position.ToString(), true, false); } // Get Your Pony Ass Back Here Treble!

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
                }
                else if (!PlayerFacingRight)
                {
                    spriteBatch.DrawString(Debug.InformationFont, "Pew Pew!", new Vector2(Position.X - 50, Position.Y - 10), Color.Black);
                }
                Debug.WriteToFile("Pew Pew is activated", false, false);
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

        protected void SetGrounded(float groundHeight)
        {
            IsGrounded = true;
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
                /// if player position is above top of ground and player is falling
                if (Position.Y < other.BoxCollision.min.Y && Velocity.Y > 0)
                {
                    SetGrounded(other.BoxCollision.min.Y - Origin.Y * Scale);
                }
                return true;
            }
            return false;

        }

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
