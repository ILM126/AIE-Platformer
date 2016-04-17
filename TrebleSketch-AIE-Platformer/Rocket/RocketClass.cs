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
    class RocketClass
    {
        //Plan: Have base rocket transmit position, rotation but have nothing special itself
        //      Add list of parts that have the textures and stack dynamically
        public List<RocketPart> parts = new List<RocketPart>();

        public Vector2 Position;
        public Vector2 SpawnPosition;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Vector2 Size;
        public float Acceleration;
        public float DeltaV;
        public float Rotation;

        public bool Spawned;

        public void AddPart(RocketPart part)
        {
            parts.Add(part);
        }

        public void Update(GameTime gameTime)
        {
            StackParts();
        }

        void StackParts()
        {
            float height = 25;
            foreach (RocketPart part in parts)
            {
                int partHeight = (int)part.m_size.Y;
                switch (partHeight)
                {
                    case 75:
                        part.m_position.Y = Position.Y - height - 38;
                        height -= part.m_size.Y;
                        break;
                    case 150:
                        part.m_position.Y = Position.Y - height;
                        height -= part.m_size.Y;
                        break;
                }
                // part.m_position.Y = Position.Y - height;
                // height -= part.m_size.Y;
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
    }
}
