using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
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
    class GameObjects
    {
        PlayerClass Player;

        public Texture2D map_texture;
        public Vector2 map_position;
        public Vector2 map_size;
        public Vector2 map_origin;

        public float map_scale;

        public GameObjects(Texture2D texture = null, Vector2 position = new Vector2(), Vector2 size = new Vector2(), float scale = 1f)
        {
            map_texture = texture;
            map_position = position;
            map_size = size;
            map_origin = new Vector2(
                (int)map_size.X / 2,
                (int)map_size.Y / 2);

            map_scale = scale;
        }
    }
}
