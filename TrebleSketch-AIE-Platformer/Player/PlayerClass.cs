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
        public SquareCollision BoxCollision;
        // public SceneObjects SceneObject;

        // Player Textures (Treble Sketch only)
        public Texture2D FaceRight;
        public Texture2D FaceLeft;

        // Player Values
        //public Vector2 SpawnPosition;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Vector2 Size;
        public float Acceleration;
        public float Rotation;

        // Player Scene Stuff
        float Gravity;
        float GroundHeight;
        float Scale;
        public int InScene;
        public int FromScene;
        public int ToScene;

        // Player Movement
        public bool PlayerFacingRight;
        public bool BothSidesPressed;
        bool IsJumping;
        bool IsGrounded;
        float JumpForce;

        public void InitialisePlayer()
        {
            BoxCollision = new SquareCollision(Position, Size);

            Gravity = 40.0f;
            JumpForce = 20.0f;
            Scale = 1f;
            GroundHeight = 40f;
            IsGrounded = false;
            IsJumping = false;
        }

        public void InitializeTrebleSketch(GraphicsDeviceManager graphics)
        {
            PlayerFacingRight = true;

            // Player.SpawnPosition = Player.Position;


            Position = new Vector2(graphics.PreferredBackBufferWidth / 2
                , graphics.PreferredBackBufferHeight / 2);
            Velocity = new Vector2(0);
            Size = new Vector2(80, 80);
            Origin = new Vector2(
                (int)Size.X / 2,
                (int)Size.Y / 2);
            //public float Acceleration;
            //public float Rotation;

            /*
            Position = new Vector2(graphics.PreferredBackBufferWidth / 2
                    , graphics.PreferredBackBufferHeight / 2);
            Velocity = new Vector2(0, 0);
            Origin = new Vector2(
                Size.X / 2,
                Size.Y / 2);
            Size = new Vector2(80, 80);
            Acceleration = Velocity.X;
            Velocity = new Vector2(0, 0);
            Acceleration = Velocity.X;*/
            // Rotation = 0;
        }

        void PlayerMovement(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            Rectangle srcRect = new Rectangle(
                                    0,
                                    0,
                                    (int)(Size.X),
                                    (int)(Size.Y));

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

            if (!IsGrounded) Velocity.Y += Gravity * time;
            else Velocity.Y = 0;

            Jumping(gameTime);

            Position.Y += Velocity.Y * time;

            Position += Velocity;
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                IsJumping = true;
                IsGrounded = false;
                Velocity.Y = -100f;
            }
        }

        protected void Jumping(GameTime gameTime)
        {
            if (IsJumping)
            {
                if (JumpForce > 0)
                {
                    Velocity.Y -= JumpForce * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    JumpForce -= Gravity * 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    IsJumping = false;
                }
            }

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

        public void Update(GameTime gameTime)
        {
            UpdateBounds();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            PlayerMovement(gameTime, spriteBatch, graphics);
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
