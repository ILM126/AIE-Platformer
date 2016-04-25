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
using EclipsingGameUtils;

namespace TrebleSketch_AIE_Platformer
{
    class LoadScene : SceneClass
    {
        public InputHandler UserInput;

        public List<SceneObjects> GroundTiles;
        float Scale;
        float Tile_Size;
        public Rectangle Button;
        public Rectangle CursonRect;
        public bool isHoveringButton;
        public Vector2 button_Position;

        public MouseState state;

        float Scene_Width;
        float Scene_Height;
        public Vector2 CentreScreen;
        public bool PlayerInScene;
        public bool RocketInScene;

        // Scene Textures
        public Texture2D OutsideGrass;

        // Menu Texture
        public Texture2D MainMenu_StartButton;
        public Texture2D MainMenu_StartButton_Hover;
        public Texture2D MainMenu_StartButton_Clicked;

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
            SceneID = 1; // Controls what is being shown on screen

        }

        public void SceneLoader(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            switch(SceneID)
            {
                case 0:
                    GroundTiles.Clear();
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
                    SceneName = "Main Menu";
                    Scene_Width = 1280;
                    Scene_Height = 720;
                    button_Position = new Vector2(CentreScreen.X, CentreScreen.Y);
                    Button = new Rectangle(
                       0,
                       0,
                       (int)(MainMenu_StartButton.Width),
                       (int)(MainMenu_StartButton.Height));
                    Console.WriteLine("[INFO] Is cursor touch button rect? " + UserInput.MouseInRectangle(Button).ToString());
                    if (state.LeftButton == ButtonState.Pressed)
                    {
                        Console.WriteLine("[INFO] BUTTON LEFT PRESS VIA LOADSCENE");
                    }
                    if (UserInput.MouseInRectangle(Button))
                    {
                        isHoveringButton = true;
                        Console.WriteLine("[INFO] BUTTON HOVERED BY MOUSE VIA LOADSCENE");
                    } else
                    {
                        isHoveringButton = false;
                    }
                    // Console.WriteLine("[INFO] Button rect data: " + Button.ToString());
                    PlayerInScene = false;
                    RocketInScene = false;
                    // FirstButtonRectLoad = true;
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
            //spriteBatch.End();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (SceneObjects groundTile in GroundTiles)
            {
                groundTile.Draw(gameTime, spriteBatch, OutsideGrass);
            }

            if (SceneID == 1)
            {
                if (isHoveringButton)
                {
                    spriteBatch.Draw(
                        MainMenu_StartButton_Hover,
                        button_Position,
                        Button,
                        Color.White,
                        0,
                        new Vector2(MainMenu_StartButton_Hover.Width / 2, MainMenu_StartButton_Hover.Height / 2),
                        Scale,
                        0,
                        0);
                    Console.WriteLine("[INFO] Hovering over the button");
                    //if (state.LeftButton == ButtonState.Pressed)
                    //{
                    //    spriteBatch.Draw(
                    //    MainMenu_StartButton_Clicked,
                    //    new Vector2(CentreScreen.X, CentreScreen.Y),
                    //    Button,
                    //    Color.White,
                    //    0,
                    //    new Vector2(MainMenu_StartButton_Hover.Width / 2, MainMenu_StartButton_Hover.Height / 2),
                    //    Scale,
                    //    0,
                    //    0);
                    //    Console.WriteLine("[INFO] Clicking the button");
                    //}
                }
                else {
                    spriteBatch.Draw(
                        MainMenu_StartButton,
                        button_Position,
                        Button,
                        Color.White,
                        0,
                        new Vector2(MainMenu_StartButton_Hover.Width / 2, MainMenu_StartButton_Hover.Height / 2),
                        Scale,
                        0,
                        0);
                    // Console.WriteLine("[INFO] Drawing the button");
                }
                if (state.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(
                    MainMenu_StartButton_Clicked,
                    button_Position,
                    Button,
                    Color.White,
                    0,
                    new Vector2(MainMenu_StartButton_Hover.Width / 2, MainMenu_StartButton_Hover.Height / 2),
                    Scale,
                    0,
                    0);
                    Console.WriteLine("[INFO] Clicking the button");
                }
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
