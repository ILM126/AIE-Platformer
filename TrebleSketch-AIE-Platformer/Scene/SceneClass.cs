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
        protected List<RenderObjects> renderedTiles;

        // Scene Stats
        public float SceneSize;
        public float SceneHeight;
        public int PlayerInScene;

        public void InitiateSurface()
        {
            
            renderedTiles = new List<RenderObjects>();
        }

        public void HandleGravity()
        {
            // Gravity = 15.936375f; // This is Earth's Gravity in this in real time m/2^s | That is, if Treble is about 1.2m tall
        }

        public void ConfirmPlayerGrounded()
        {

        }

        /* public void ConfirmPlayerInScene()
        {
            if (Player.InScene == PlayerInScene)
            {
                
            }
        }*/

        public void SpawnScene()
        {

            // if ()
        }

    }
}
