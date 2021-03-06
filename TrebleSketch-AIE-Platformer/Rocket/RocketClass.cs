﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EclipsingGameUtils;
using AIEResources.ParticleEffects;

namespace TrebleSketch_AIE_Platformer
{
    class RocketClass
    {
        //Plan: Have base rocket transmit position, rotation but have nothing special itself
        //      Add list of parts that have the textures and stack dynamically
        public List<RocketPart> parts = new List<RocketPart>();

        public Emitter ParticleEmitter;
        public Texture2D[] rocketParts = new Texture2D[4];
        public Texture2D particles_RocketExhaust;
        public Vector2 particlesPosition;

        public Game1 Game;
        public InputHandler UserInput;
        public SquareCollision BoxCollision;
        public DevLogging Debug;
        public RocketPart.PartTypes PartTypes;
        public LaunchVehicles LaunchVehicle;

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

        public bool LiftOff;

        float height;
        int lowerPart;
        int higherPart;

        public enum LaunchVehicles
        {
            LightLauncher_Magpie_Crewed,
            LightLauncher_Magpie_Uncrewed
        }

        public void AddPart(RocketPart part)
        {
            parts.Add(part);
        }

        public void InitialiseRocket()
        {
            BoxCollision = new SquareCollision(Position, Size);
            ParticleEmitter = Emitter.CreateBurstEmitter(particles_RocketExhaust, Game.CentreScreen);

            IsGrounded = false;

            Position = new Vector2(SpawnPosition.X
                , SpawnPosition.Y * 2 - 150);
            Velocity = new Vector2(0);
            Acceleration = Velocity.X;
            Rotation = 0;
        }

        public void SetSize(Vector2 size)
        {
            Size = new Vector2(size.X, size.Y);
            //Debug.WriteToFile("Rocket Size: " + Size.ToString(), true, false);
            Origin = new Vector2(
                (int)Size.X / 2,
                (int)Size.Y / 2);
        }

        public void Update(GameTime gameTime)
        {
            //Debug.WriteToFile("Engine position before time in Update in RocketClass: " + parts[0].m_position.ToString(), true, false);
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Debug.WriteToFile("Engine position after time in Update in RocketClass: " + parts[0].m_position.ToString(), true, false);

            if (InputHandler.IsKeyDownOnce(Keys.Up))
            {
                if (IsGrounded)
                {
                    IsFlying = true;
                    IsGrounded = false;
                    Velocity.Y -= 1000f;
                }
            }

            if (!IsGrounded) Velocity.Y += Gravity * time;
            else Velocity.Y = 0;
            Position.Y += Velocity.Y * time;
            Position.X += Velocity.X;

            // if engine is built and actualLiftOff is true, then emitter update will run!

            if (LiftOff)
            {
                foreach (RocketPart part in parts)
                {
                    switch (PartTypes = part.m_type)
                    {
                        case RocketPart.PartTypes.Engine_Titus:
                            particlesPosition = new Vector2(part.m_position.X, part.m_position.Y + 290);
                            ParticleEmitter.position = particlesPosition; // check if the part if engine and then particle appear when actualLiftOff is true;
                            ParticleEmitter = Emitter.CreateRocketFireEmitter(particles_RocketExhaust, particlesPosition);
                            ParticleEmitter.Update(gameTime);
                            //Debug.WriteToFile("Updating particle position", false, false);
                            //Debug.WriteToFile("Particle Position: " + ParticleEmitter.position.ToString(), false, false);
                            break;
                        default:
                            break;
                    }
                }
            }

            CalculatePositionAndCollisionBox();
            StackParts();
            UpdateBounds();
            //Debug.WriteToFile("Rocket Engine Position end of RocketClass Update: " + parts[0].m_position.ToString(), true, false);
        }

        void CalculatePositionAndCollisionBox()
        {
            int partCount = parts.Count;
            if (LaunchVehicle == LaunchVehicles.LightLauncher_Magpie_Crewed)
            {
                switch (partCount)
                {
                    case 0:
                    case 1:
                        height = 0;
                        lowerPart = 0;
                        break;
                    case 2:
                        height = 113;
                        lowerPart = 200;
                        break;
                    case 3:
                        height = 88;
                        lowerPart = 200;
                        higherPart = 287;
                        break;
                    default:
                        break;
                }
            }
        }

        void StackParts()
        {
            foreach (RocketPart part in parts)
            {
                if (LaunchVehicle == LaunchVehicles.LightLauncher_Magpie_Crewed)
                {
                    switch (PartTypes = part.m_type)
                    {
                        case RocketPart.PartTypes.Capsule_Manned_PipingShrike:
                            part.m_position.Y = Position.Y - height - higherPart;
                            //Debug.WriteToFile("Position of Piping Shrike: " + part.m_position.ToString(), true, false);
                            height -= part.m_size.Y;
                            break;
                        case RocketPart.PartTypes.FuelTank_Medium:
                            part.m_position.Y = Position.Y - height;
                            height -= part.m_size.Y;
                            break;
                        case RocketPart.PartTypes.Engine_Titus:
                            part.m_position.Y = Position.Y - height + lowerPart;
                            height -= part.m_size.Y;
                            break;
                        default:
                            int currentPart = -1;
                            currentPart = parts.IndexOf(part);
                            Debug.WriteToFile("Part no. " + currentPart + ", is not assosiated with a 'PartTypes'", true, true);
                            break;
                    }
                }
                //part.m_position.Y = Position.Y - height;
                //height -= part.m_size.Y;
                // This is the default code, keeping it here as a reference
                //Debug.WriteToFile("Rocket Engine Position end of StackParts: " + parts[0].m_position.ToString(), true, false);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // emitter draw will be updated once engine is built and actualiftOff is true

            if (LiftOff)
            {
                foreach (RocketPart part in parts)
                {
                    switch (PartTypes = part.m_type)
                    {
                        case RocketPart.PartTypes.Engine_Titus:
                            ParticleEmitter.Draw(spriteBatch);
                            //Debug.WriteToFile("Drawing particle", false, false);
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (RocketPart part in parts)
            {
                part.Draw(spriteBatch);
            }
        }

        #region Collisions
        protected virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            BoxCollision = new SquareCollision(Position, Size * Scale);
        }

        protected void SetGrounded(float groundHeight)
        {
            IsGrounded = true;
            IsFlying = false;
            GroundHeight = groundHeight;
            Position.Y = groundHeight;
            Velocity.Y = 0;
            UpdateBounds();
        }

        protected bool SquareCollisionCheck(SceneObjects pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
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
        #endregion
    }
}
