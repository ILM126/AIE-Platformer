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
    class LoadScene : SceneClass
    {
        public List<SceneObjects> GroundTiles;
        public List<SceneObjects> MenuTiles;
        float Scale;
        float Tile_Size;

        float Scene_Width;
        float Scene_Height;
        public Vector2 CentreScreen;
        public bool PlayerInScene;
        public bool RocketInScene;

        // Scene Textures
        public Texture2D OutsideGrass;

        // Menu Texture
        public Texture2D MainMenu_StartButton;

        // public Texture2D BuildingOutsideWalls;
        // public Texture2D BuildingInsideWalls;

        // public Texture2D InsideTiledFloor;
        // public Texture2D InsideMetalFloor;
        // public Texture2D InsideConcreteFloor;

        public void InitialiseScene()
        {
            GroundTiles = new List<SceneObjects>();
            MenuTiles = new List<SceneObjects>();
            Scale = 1f;
            Tile_Size = 50f;
            SceneID = 1; // Controls what is being shown on screen
            SceneLoader();
        }

        public void SceneLoader()
        {
            switch(SceneID)
            {
                case 0:
                    GroundTiles.Clear();
                    MenuTiles.Clear();
                    SceneName = "Test Map";
                    Scene_Width = 1280;
                    Scene_Height = 720;
                    for (int i = 0; i < CentreScreen.X / 25; i++)
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2(i * 50
                            , CentreScreen.Y * 2 - 25)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale
                        , false);
                        GroundTiles.Add(GroundTile);
                    }
                    PlayerInScene = true;
                    RocketInScene = true;
                    break;
                case 1:
                    GroundTiles.Clear();
                    MenuTiles.Clear();
                    SceneName = "Main Menu";
                    Scene_Width = 1280;
                    Scene_Height = 720;
                    SceneObjects MenuTile = new SceneObjects(
                        MainMenu_StartButton
                        , new Vector2(CentreScreen.X
                            , CentreScreen.Y)
                        , new Vector2(100
                            , 40)
                        , Scale
                        , true);
                    MenuTiles.Add(MenuTile);
                    PlayerInScene = false;
                    RocketInScene = false;
                    break;
                case 2:
                    SceneName = "Settings Menu";
                    break;
                case 3:
                    SceneName = "Pause Menu";
                    break;
                case 4:
                    SceneName = "Front Lawns";
                    break;
                default:
                    SceneName = "Test Map";
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (SceneObjects groundTile in GroundTiles)
            {
                groundTile.Draw(gameTime, spriteBatch, OutsideGrass);
            }
            foreach (SceneObjects MenuTile in MenuTiles)
            {
                MenuTile.Draw(gameTime, spriteBatch, MainMenu_StartButton);
            }
        }

        public void CheckCollisions(PlayerClass player)
        {

            // Check collision with ground tiles
            foreach (SceneObjects groundTile in GroundTiles)
            {
                if (player.CheckCollisionsGround(groundTile))
                {
                    // Console.WriteLine("[TERRAIN] I am being touched!");
                }
            }

        }
    }
}
