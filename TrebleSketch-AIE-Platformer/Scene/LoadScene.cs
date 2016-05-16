using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EclipsingGameUtils;
using TrebleSketch_AIE_Platformer.MiniGames;

namespace TrebleSketch_AIE_Platformer
{
    class LoadScene : SceneClass
    {
        public InputHandler UserInput;
        public DevLogging Debug;
        public BuildTheRocket MiniGame_BuildTheRocket;
        public ScrapMetal BTR_ScrapMetal;

        public List<SceneObjects> GroundTiles;
        public List<ScrapMetal> ScrapMetals;

        public Rectangle Button;
        public Rectangle CursonRect;
        public bool isHoveringButton;
        public bool isClickingWhileHovering;
        public Vector2 button_Position;

        bool StartButton;
        bool ExitButton;

        public MouseState state;

        public bool RunOnceTest;

        float Scene_Width;
        float Scene_Height;
        float Scale;
        float Tile_Size;

        public Vector2 CentreScreen;
        public bool PlayerInScene;
        public bool RocketInScene;
        public bool MiniGame;

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

        public int scrapMetalCount;

        public void InitialiseScene()
        {
            GroundTiles = new List<SceneObjects>();
            ScrapMetals = new List<ScrapMetal>();
            Scale = 1f;
            Tile_Size = 50f;
            SceneID = 1; // Controls what is being shown on screen
            button_Position = new Vector2(CentreScreen.X, CentreScreen.Y);
            Button = new Rectangle(
                    (int)button_Position.X - 50,
                    (int)button_Position.Y - 20,
                    100,
                    40);
            RunOnceTest = true;
            //RunOnceTest = false;
            // Debug.WriteToFile(ScrapMetals[0].m_position.ToString(), false);
        }

        public void AddPart(RocketPart p)
        {
            // parts.Add(part);
        }

        public void SceneLoader(SpriteBatch spriteBatch)
        {
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
                    MiniGame = false;
                    break;
                case 1:
                    GroundTiles.Clear();
                    SceneName = "Main Menu";
                    Scene_Width = 1280;
                    Scene_Height = 720;
                    if (state.LeftButton == ButtonState.Pressed)
                    {
                        isClickingWhileHovering = true;
                    }
                    else if (UserInput.MouseInRectangle(Button))
                    {
                        isHoveringButton = true;
                        isClickingWhileHovering = false;
                    } else
                    {
                        isClickingWhileHovering = false;
                        isHoveringButton = false;
                    }
                    if (UserInput.MouseButtonClickedOnce(MouseButton.Left) && UserInput.MouseInRectangle(Button))
                    {
                        SceneID = 5;
                    }
                    PlayerInScene = false;
                    RocketInScene = false;
                    MiniGame = false;
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
                case 5:
                    GroundTiles.Clear();
                    SceneName = "Mini Game: Build the Rocket";
                    Scene_Width = 1280;
                    Scene_Height = 720;
                    for (int i = 0; i < CentreScreen.X / 25; i++) // Ground at the bottom
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2(5 + i * 50
                            , CentreScreen.Y * 2 - 25)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale
                        , false);
                        GroundTiles.Add(GroundTile);
                    }
                    for (int i = 0; i < CentreScreen.X / 50; i++) // 1st floor, right
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2((CentreScreen.X * 2f) - (i / 3) * 50
                            , CentreScreen.Y * 1.45f)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale
                        , false);
                        GroundTiles.Add(GroundTile);
                    }
                    for (int i = 0; i < CentreScreen.X / 50; i++) // 1st floor, left
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2(5 + (i / 3) * 50
                            , CentreScreen.Y * 1.45f)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale
                        , false);
                        GroundTiles.Add(GroundTile);
                    }
                    for (int i = 0; i < CentreScreen.X / 50; i++) // 2nd floor, right
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2((CentreScreen.X * 2f) - (i / 2) * 50
                            , CentreScreen.Y - 5)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale
                        , false);
                        GroundTiles.Add(GroundTile);
                    }
                    for (int i = 0; i < CentreScreen.X / 50; i++) // 2nd floor, left
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2(5 + (i / 2) * 50
                            , CentreScreen.Y - 5)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale
                        , false);
                        GroundTiles.Add(GroundTile);
                    }
                    for (int i = 0; i < CentreScreen.X / 50; i++) // top floor, middle
                    {
                        SceneObjects GroundTile = new SceneObjects(
                        OutsideGrass
                        , new Vector2((CentreScreen.X / 4 * 3) + 15 + (i / 2) * 50
                            , CentreScreen.Y - 125)
                        , new Vector2(Tile_Size, Tile_Size)
                        , Scale
                        , false);
                        GroundTiles.Add(GroundTile);
                    }
                    //for (int i = 0; i < scrapMetalCount; i++)
                    //{
                    //}
                    if (InputHandler.IsKeyDownOnce(Keys.Z))
                    {
                        ScrapMetal scrapMetal = new ScrapMetal(
                                BTR_ScrapMetal.tex_ScrapMetal,
                                new Vector2(300, 50),
                                new Vector2(50, 30),
                                1f);
                        ScrapMetals.Add(scrapMetal);
                    }
                    if (BTR_ScrapMetal.m_position.Y == 50)
                    {
                        Debug.WriteToFile("ScrapMetal Y POSITION: " + BTR_ScrapMetal.m_position.ToString(), false);
                    }
                    PlayerInScene = true;
                    RocketInScene = true;
                    break;
                default:
                    SceneName = "Test Map";
                    break;
            }
            if (InputHandler.IsKeyDownOnce(Keys.D0))
            {
                SceneID = 0;
                Debug.WriteToFile("Loaded " + SceneName, true);
            }
            if (InputHandler.IsKeyDownOnce(Keys.D1))
            {
                SceneID = 1;
                Debug.WriteToFile("Loaded " + SceneName, true);
            }
            if (ScrapMetals.Count > 0)
            {
                //Debug.WriteToFile("Scrap Metal location: " + ScrapMetals[0].m_position.Y, false);
                //Debug.WriteToFile("Scrap Metal count: " + ScrapMetals.Count, false);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (SceneObjects groundTile in GroundTiles)
            {
                groundTile.Draw(gameTime, spriteBatch, OutsideGrass);
            }

            if (SceneID == 1)
            {
                if (isClickingWhileHovering && isHoveringButton)
                {
                    spriteBatch.Draw(
                    MainMenu_StartButton_Clicked,
                    new Vector2(CentreScreen.X, CentreScreen.Y),
                    null,
                    Color.White,
                    0,
                    new Vector2(MainMenu_StartButton_Hover.Width / 2, MainMenu_StartButton_Hover.Height / 2),
                    Scale,
                    0,
                    0);
                }
                else if (isHoveringButton)
                {
                    spriteBatch.Draw(
                        MainMenu_StartButton_Hover,
                        button_Position,
                        null,
                        Color.White,
                        0,
                        new Vector2(MainMenu_StartButton_Hover.Width / 2, MainMenu_StartButton_Hover.Height / 2),
                        Scale,
                        0,
                        0);
                }
                else {
                    spriteBatch.Draw(
                        MainMenu_StartButton,
                        button_Position,
                        null,
                        Color.White,
                        0,
                        new Vector2(MainMenu_StartButton_Hover.Width / 2, MainMenu_StartButton_Hover.Height / 2),
                        Scale,
                        0,
                        0); 
                }
            }
            if (SceneID == 5) // Mini Game: Build the Rocket
            {
                foreach (ScrapMetal scrapMetal in ScrapMetals)
                {
                    BTR_ScrapMetal.Draw(gameTime, spriteBatch, BTR_ScrapMetal.tex_ScrapMetal);
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

        public void CheckCollisions(RocketClass rocket)
        {
            // Check collision with ground tiles
            foreach (SceneObjects groundTile in GroundTiles)
            {
                if (rocket.CheckCollisionsGround(groundTile))
                {
                    // Debug.WriteToFile("Rocket is touching the terrain", false);
                }
            }
        }

        public void CheckCollisions(ScrapMetal BTR_ScrapMetal)
        {
            // Check collision with ground tiles
            foreach (SceneObjects groundTile in GroundTiles)
            {
                if (BTR_ScrapMetal.CheckCollisionsGround(groundTile))
                {
                    // Debug.WriteToFile("Rocket is touching the terrain", false);
                }
            }
        }
    }
}
