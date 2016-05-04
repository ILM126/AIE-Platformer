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
using EclipsingGameUtils;

namespace TrebleSketch_AIE_Platformer
{
    class RocketClass
    {
        //Plan: Have base rocket transmit position, rotation but have nothing special itself
        //      Add list of parts that have the textures and stack dynamically
        public List<RocketPart> parts = new List<RocketPart>();

        public InputHandler UserInput;
        public SquareCollision BoxCollision;
        public DevLogging Debug;

        public Vector2 Position;
        public Vector2 SpawnPosition;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Vector2 Size;
        public float Acceleration;
        public float DeltaV;
        public float Rotation;

        public bool Spawned;
        public bool IsGrounded;
        bool IsFlying;
        public float Scale;
        public float Gravity;
        public float GroundHeight;

        float height;

        public void AddPart(RocketPart part)
        {
            parts.Add(part);
        }

        public void InitialiseRocket()
        {
            BoxCollision = new SquareCollision(Position, Size);

            IsGrounded = false;

            Position = new Vector2(SpawnPosition.X
                , SpawnPosition.Y / 2);
            Velocity = new Vector2(0);
            Acceleration = Velocity.X;
            Rotation = 0;
        }

        public void SetSize(Vector2 size)
        {
            Size = new Vector2(size.X, size.Y);
            Debug.WriteToFile("Rocket Size: " + Size.ToString(), false);
            Origin = new Vector2(
                (int)Size.X / 2,
                (int)Size.Y / 2);
            Debug.WriteToFile("Rocket Origin: " + Origin.ToString(), false);
            Debug.WriteToFile("Rocket Origin: " + Position.ToString(), false);
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputHandler.IsKeyDownOnce(Keys.Up))
            {
                if (IsGrounded)
                {
                    IsFlying = true;
                    IsGrounded = false;
                    Velocity.Y -= 500f;
                }
            }

            foreach (RocketPart part in parts)
            {
                
            }

            if (!IsGrounded) Velocity.Y += Gravity * time;
            else Velocity.Y = 0;
            Position.Y += Velocity.Y * time;
            Position.X += Velocity.X;
            
            StackParts();
            UpdateBounds();
        }

        void StackParts()
        {
            height = 38;
            foreach (RocketPart part in parts)
            {
                int partHeight = (int)part.m_size.Y;
                switch (partHeight)
                {
                    case 75:
                        part.m_position.Y = Position.Y - height - 50;
                        height -= part.m_size.Y;
                        break;
                    case 175:
                        part.m_position.Y = Position.Y - height;
                        height -= part.m_size.Y;
                        break;
                }
                //part.m_position.Y = Position.Y - height;
                //height -= part.m_size.Y;
                // This is the default code, keeping it here as a reference
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(RocketPart part in parts)
            {
                part.Draw(spriteBatch);

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
            IsFlying = false;
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
            bool rocketCollision = SquareCollisionCheck(other);
            if (rocketCollision)
            {
                return true;
            }
            return false;
        }
    }
}
