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
    class PlayerTrebleSketch
    {
        public bool PlayerFacingRight;
        public Texture2D FaceRight;
        public Texture2D FaceLeft;

        public void loadPlayerTrebleSketchRight(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(FaceRight
                , new Vector2(graphics.PreferredBackBufferWidth / 2
                    , graphics.PreferredBackBufferHeight / 2)
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
                , new Vector2(graphics.PreferredBackBufferWidth / 2
                    , graphics.PreferredBackBufferHeight / 2)
                , null
                , Color.White
                , 0
                , new Vector2(FaceLeft.Width / 2
                    , FaceLeft.Height / 2)
                , new Vector2(1, 1)
                , SpriteEffects.None
                , 0);
        }
    }
}
