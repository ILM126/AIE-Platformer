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
        public float Speed;
        public float Acceleration;

        public Vector2 Position;

        public float Height;
        public float Width;

        public bool Spawned;

        public EngineClass Engine;
        public FuelTankClass FuelTank;

        public void RocketSpawnPosition(GraphicsDeviceManager graphics)
        {
            Position = new Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2);
        }

        public RocketClass()
        {
            Engine = new EngineClass();
            FuelTank = new FuelTankClass();
        }
        
    }
}
