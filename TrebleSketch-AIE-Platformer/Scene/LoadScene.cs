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

        // Scene Textures
        public Texture2D OutsideGrass;

        // public Texture2D BuildingOutsideWalls;
        // public Texture2D BuildingInsideWalls;

        // public Texture2D InsideTiledFloor;
        // public Texture2D InsideMetalFloor;
        // public Texture2D InsideConcreteFloor;

        public void InitialiseScene(GraphicsDeviceManager graphics)
        {
            GroundTiles = new List<SceneObjects>();
            Scale = 0.75f;
            Tile_Size = 50f;
            SceneID = 0;
            SceneLoader(graphics);
        }

        public void SceneLoader(GraphicsDeviceManager graphics)
        {
            switch(SceneID)
            {
                case 0:
                    SceneName = "Test Map";
                    SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2(graphics.PreferredBackBufferWidth / 2
                            , graphics.PreferredBackBufferHeight / 2 + 70)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale);
                    GroundTiles.Add(GroundTile);
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
