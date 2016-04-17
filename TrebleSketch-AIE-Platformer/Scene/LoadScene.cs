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
        float Scale;
        float Tile_Size;

        float Scene_Width;
        float Scene_Height;
        float Scene_Size;
        public Vector2 CentreScreen;

        // Scene Textures
        public Texture2D OutsideGrass;

        // public Texture2D BuildingOutsideWalls;
        // public Texture2D BuildingInsideWalls;

        // public Texture2D InsideTiledFloor;
        // public Texture2D InsideMetalFloor;
        // public Texture2D InsideConcreteFloor;

        public void InitialiseScene()
        {
            GroundTiles = new List<SceneObjects>();
            Scale = 1f;
            Tile_Size = 50f;
            SceneID = 0;
            SceneLoader();
        }

        public void SceneLoader()
        {
            switch(SceneID)
            {
                case 0:
                    SceneName = "Test Map";
                    Scene_Width = 5f;
                    for (int i = 0; i < 20; i++)
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2(CentreScreen.X + i * 50
                            , CentreScreen.Y + 75)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale);
                        GroundTiles.Add(GroundTile);
                    }
                        
                    
                    break;
                case 1:
                    SceneName = "Main Menu";
                    break;
                case 2:
                    SceneName = "Settings Menu";
                    break;
                case 3:
                    SceneName = "Front Lawns";
                    break;
                default:
                    SceneName = "Test Map";
                    // Scene00_TestMap();
                    break;
            }

            Console.WriteLine("[INFO] Loaded " + SceneName + ".");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (SceneObjects groundTile in GroundTiles)
            {
                groundTile.Draw(gameTime, spriteBatch, OutsideGrass);
            }
        }

        public void CheckCollisions(PlayerClass player)
        {

            // Check collision with ground tiles
            foreach (SceneObjects groundTile in GroundTiles)
            {
                if (player.CheckCollisionsGround(groundTile))
                {
                    // Console.WriteLine("[INFO] " + player.);
                }
            }

        }
    }
}
