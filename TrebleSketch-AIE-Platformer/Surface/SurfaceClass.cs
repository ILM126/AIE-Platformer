﻿using System;
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
    class SurfaceClass
    {
        PlayerClass Player;

        public Vector2 Gravity;

        // Scene Stats
        public float SceneSize;
        public float SceneHeight;
        public int PlayerInScene;

        public void ConfirmPlayerInScene()
        {
            if (Player.InScene == PlayerInScene)
            {
                
            }
        }

    }
}