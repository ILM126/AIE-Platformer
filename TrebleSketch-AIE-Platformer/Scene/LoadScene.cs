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
        //public ScrapMetal BTR_ScrapMetal;
        public Message ListMessages;

        public List<SceneObjects> GroundTiles; // For all scenes uses

        /// MiniGame Uses only
        public List<ScrapMetal> ScrapMetals;
        public List<FuelUnit> FuelUnits;

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

        // Menu Button Textures
        public Texture2D MainMenu_StartButton;
        public Texture2D MainMenu_StartButton_Hover;
        public Texture2D MainMenu_StartButton_Clicked;

        // Rocket Launch Button Textures
        public Texture2D Rocket_LaunchRocketButton;
        public Texture2D Rocket_LaunchRocketButton_Hover;
        public Texture2D Rocket_LaunchRocketButton_Clicked;

        // public Texture2D BuildingOutsideWalls;
        // public Texture2D BuildingInsideWalls;

        // public Texture2D InsideTiledFloor;
        // public Texture2D InsideMetalFloor;
        // public Texture2D InsideConcreteFloor;

        public bool MessageNotLoaded;

        public void InitialiseScene()
        {
            GroundTiles = new List<SceneObjects>();
            ScrapMetals = new List<ScrapMetal>();
            FuelUnits = new List<FuelUnit>();
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
            MessageNotLoaded = true;

            ListMessages.MessagePosition = new Vector2(CentreScreen.X * 2 - 140, CentreScreen.Y * 2 - 30);

            //RunOnceTest = false;
            // Debug.WriteToFile(ScrapMetals[0].m_position.ToString(), false);
        }

        public void AddPart(RocketPart p)
        {
            // parts.Add(part);
        }

        public void SceneLoader(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch(SceneID)
            {
                case 0:
                    #region Test Map
                    GroundTiles.Clear();
                    SceneName = "Test Map";
                    Scene_Width = 1280;
                    Scene_Height = 720;
                    MouseButtonRelationships();
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
                    #endregion
                    break;
                case 1:
                    GroundTiles.Clear();
                    SceneName = "Main Menu";
                    MouseButtonRelationships();
                    if (UserInput.MouseButtonClickedOnce(MouseButton.Left) && UserInput.MouseInRectangle(Button))
                    {
                        SceneID = 5;
                        MessageNotLoaded = true;
                        button_Position = new Vector2(CentreScreen.X, 40);
                        Button = new Rectangle(
                            (int)button_Position.X - 50,
                            (int)button_Position.Y - 20,
                            100,
                            40);
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
                    MiniGame_BTR(gameTime);
                    break;
                default:
                    SceneName = "Test Map";
                    break;
            }
            MessageOnLoad(gameTime);
        }

        public void MouseButtonRelationships()
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                isClickingWhileHovering = true;
            }
            else if (UserInput.MouseInRectangle(Button))
            {
                isHoveringButton = true;
                isClickingWhileHovering = false;
            }
            else
            {
                isClickingWhileHovering = false;
                isHoveringButton = false;
            }
        }

        public void MessageOnLoad(GameTime gameTime)
        {
            if (MessageNotLoaded)
            {
                if (ListMessages.messages.Count > 0)
                {
                    ListMessages.messages.Clear();
                }
                ListMessages.messages.Add(new Message()
                    {
                        Text = "Scene ID: " + SceneID,
                        Appeared = gameTime.TotalGameTime,
                        Position = ListMessages.MessagePosition
                    });
                Debug.WriteToFile("Switched to Scene: " + SceneID, true, false);
                Debug.WriteToFile("Message Appeared Time: " + ListMessages.messages[0].Appeared.ToString(), false, false);
                MessageNotLoaded = false;
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
                    button_Position,
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

            if (SceneID == 5 && MiniGame_BuildTheRocket.parts.Count == 3 && MiniGame_BuildTheRocket.RocketFuelCollected == MiniGame_BuildTheRocket.PlannedRocketFuel)
            {
                if (isClickingWhileHovering && isHoveringButton)
                {
                    spriteBatch.Draw(
                    Rocket_LaunchRocketButton_Clicked,
                    button_Position,
                    null,
                    Color.White,
                    0,
                    new Vector2(Rocket_LaunchRocketButton_Hover.Width / 2, Rocket_LaunchRocketButton_Hover.Height / 2),
                    Scale,
                    0,
                    0);
                }
                else if (isHoveringButton)
                {
                    spriteBatch.Draw(
                        Rocket_LaunchRocketButton_Hover,
                        button_Position,
                        null,
                        Color.White,
                        0,
                        new Vector2(Rocket_LaunchRocketButton_Hover.Width / 2, Rocket_LaunchRocketButton_Hover.Height / 2),
                        Scale,
                        0,
                        0);
                }
                else {
                    spriteBatch.Draw(
                        Rocket_LaunchRocketButton,
                        button_Position,
                        null,
                        Color.White,
                        0,
                        new Vector2(Rocket_LaunchRocketButton_Hover.Width / 2, Rocket_LaunchRocketButton_Hover.Height / 2),
                        Scale,
                        0,
                        0);
                }
            }
        }

        public void MiniGame_BTR(GameTime gameTime)
        {
            GroundTiles.Clear();
            SceneName = "Mini Game: Build the Rocket";
            Scene_Width = 1280;
            Scene_Height = 720;
            MouseButtonRelationships();
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

            MiniGame_BuildTheRocket.SpawnScrapMetals(gameTime);
            MiniGame_BuildTheRocket.SpawnFuelUnits(gameTime);

            PlayerInScene = true;
            RocketInScene = true;
        }

        #region Check Collisions
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

        public void CheckCollisions(ScrapMetal BTR_ScrapRocket)
        {
            // Check collision with ground tiles
            foreach (SceneObjects groundTile in GroundTiles)
            {
                foreach (ScrapMetal scrapMetal in ScrapMetals)
                {
                    if (scrapMetal.CheckCollisionsGround(groundTile))
                    {
                        // Debug.WriteToFile("Rocket is touching the terrain", false);
                    }
                }
            }
        }

        public void CheckCollisions(FuelUnit BTR_FuelUnit)
        {
            // Check collision with ground tiles
            foreach (SceneObjects groundTile in GroundTiles)
            {
                foreach (FuelUnit fuelUnit in FuelUnits)
                {
                    if (fuelUnit.CheckCollisionsGround(groundTile))
                    {
                        // Debug.WriteToFile("Rocket is touching the terrain", false);
                    }
                }
            }
        }
        #endregion
    }
}
