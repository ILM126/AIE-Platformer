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
    class SceneObjects
    {
        public Texture2D scene_Texture;
        public Vector2 scene_Position;
        public Vector2 scene_Size;

        public SceneObjects(Texture2D texture = null, Vector2 position = new Vector2(), Vector2 size = new Vector2())
        {
            scene_Texture = texture;
            scene_Position = position;
            scene_Size = size;
        }


    }
}
