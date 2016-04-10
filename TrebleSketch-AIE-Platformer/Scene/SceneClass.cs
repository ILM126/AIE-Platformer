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

        // public float Gravity;

        // Scene Textures
        public Texture2D OutsideGrass;

        // public Texture2D BuildingOutsideWalls;
        // public Texture2D BuildingInsideWalls;

        // public Texture2D InsideTiledFloor;
        // public Texture2D InsideMetalFloor;
        // public Texture2D InsideConcreteFloor;

        // List of Tiles rendered :3
        protected List<SceneObjects> sceneTiles;
        protected Vector2 Tile_Size;

        // Scene Stats
        float Scene_Width;
        float Scene_Height;
        float Scene_Size;

        // Player Scene Stats
        public int PlayerInScene;

        public void InitiateSurface()
        {
            sceneTiles = new List<SceneObjects>();
        }

        public void ConfirmPlayerGrounded()
        {

        }
    }
}
