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
using TrebleSketch_AIE_Platformer.Player;
using TrebleSketch_AIE_Platformer.Scene;

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

        protected int SceneID;
        public string SceneName;

        PlayerClass Player;
        SceneClass Scene;
        LoadScene SceneLoad;

        // List of Tiles rendered :3
        protected List<SceneObjects> sceneTiles;

        // Scene Stats
        float Scene_Width;
        float Scene_Height;
        float Scene_Size;

        // Player Scene Stats
        public int PlayerInScene;

        public void InitiateSurface()
        {
        }

        public void Draw()
        {

        }

        public void ConfirmPlayerGrounded()
        {

        }
    }
}
