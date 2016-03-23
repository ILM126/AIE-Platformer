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
    class FuelTankClass
    {
        EngineClass Engine;

        /// <summary>
        /// Engine: "Titus"
        /// Version: 1
        /// Thrust (ASL):
        /// Thrust (Vaccum):
        /// TWR:
        /// Weight:
        /// Fuel: 
        /// </summary>

        public Texture2D Medium;

        public void loadFuelTankMedium(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(Medium
                , new Vector2(graphics.PreferredBackBufferWidth / 2
                    , graphics.PreferredBackBufferHeight / 2 - 62.5f)
                , null
                , Color.White
                , 0
                , new Vector2(Medium.Width / 2
                    , Medium.Height / 2)
                , new Vector2(1, 1)
                , SpriteEffects.None
                , 0);
        }
    }
}
