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
    class SceneClass
    {
        protected enum UIMode
        {
            MainMenu,
            SettingsMenu,
            LoadMenu,
            StoryStage,
            BattleStage,
            InGameUIGone
        }

        public int SceneID;
        public string SceneName;

        // List of Tiles rendered :3
        // protected List<SceneObjects> sceneTiles;

        public void Update()
        {
            if (InputHandler.IsKeyDownOnce(Keys.D0))
            {
                SceneID = 0;
                Console.WriteLine("[INFO] Loaded " + SceneName);
            }
            if (InputHandler.IsKeyDownOnce(Keys.D1))
            {
                SceneID = 1;
                Console.WriteLine("[INFO] Loaded " + SceneName);
            }
        }

        public void Draw()
        {

        }

        public void ConfirmPlayerGrounded()
        {

        }
    }
}
