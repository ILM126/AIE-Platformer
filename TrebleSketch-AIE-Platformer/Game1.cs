using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;
using EclipsingGameUtils;
using TrebleSketch_AIE_Platformer.MiniGames;
using System.Collections.Generic;

namespace TrebleSketch_AIE_Platformer
{

    /// Notes for Reece:
    /// 01. Currently, as of 26rd of April 2016, there are no:
    ///     Enemy AIs
    ///     Sounds/SFX
    ///     Particle Systems
    ///     Scrolling
    ///     And others
    /// Coding ftw

    /// <summary>
    /// Name: Space Program Simulator 2016
    /// Genre: 2D Platformer
    /// Description: You must play as Treble Sketch or Adelaide as either of them must handle the everyday stress of being the head of
    /// a starting national space agency.
    /// Version: 0.0.17.200 (Developmental Stages)
    /// Developer: Titus Huang (Treble Sketch/ILM126)
    /// Game Engine: MonoGame/XNA
    /// Language: C#
    /// Dev Notes: The second MonoGame project for the Academy of Interactive Entertainment (AIE) Cert II C# Course, hope to massively
    /// improve on my game design/programming skills. Yes, this game does contain ponies.
    /// Also, thanks to my friend Eclipsing Rainbows (@EclipsingR) and Max Iraci-Sareri (@SerMax_) for helping me out with the code!
    /// </summary>

    /// <summary>
    /// TO DO:
    /// - Weapons System
    /// - Scene Scrolling for maps larger then screen
    /// - Scene Transitions
    /// - Audio System
    /// - Speech System
    /// - Enemy AI
    /// </summary>

    /// <summary>
    /// BUGS / UPCOMING FEATURES:
    /// - Incomplete Game
    /// - (Text) Currently using BitmapFonts, unable to change size. Need to convert to SpriteFonts
    /// - (Enemy) Make Enemy textures
    /// - (Enemy) Get enemy AI working
    /// - (Mini Game - Build the Rocket) Scrap Metal to get collisions with players for points!
    /// - (UI) Able to take user's name and use it in the game
    /// </summary>
    public class Game1 : Game
    {
        #region Begin
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // "Start" the Classes
        PlayerClass Player;
        EnemyClass Enemy;
        RocketClass Rocket;
        RocketPart RocketParts;
        WorldClass World;
        SceneClass Scene;
        LoadScene SceneLoad;
        SceneObjects SceneObject;
        AudioClass Audio;

        DevLogging Debug;
        Cursor MouseMovement;
        InputHandler UserInput;
        Message ListMessages;

        public Rectangle Button;
        public Rectangle CursorRect;

        // MiniGames
        BuildTheRocket MiniGame_BuildTheRocket;
        ScrapMetal BTR_ScrapMetal;

        Texture2D[] rocketParts = new Texture2D[4];

        public Vector2 CentreScreen;
        public float Scale;
        public float Gravity;
        public float GroundHeight;
        string GameVersionBuild;

        #endregion

        public Game1()
        {
            Debug = new DevLogging();
            File.Delete(Debug.GetCurrentDirectory());
            GameVersionBuild = "v0.0.17.200 (30/05/16)";
            Debug.WriteToFile("Starting Space Program Simulator 2016 " + GameVersionBuild, true);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteToFile("Started Initializing Game", true);

            CentreScreen = new Vector2(graphics.PreferredBackBufferWidth / 2
                , graphics.PreferredBackBufferHeight / 2);

            Debug = new DevLogging();
            Debug.ShowDebug();

            MouseMovement = new Cursor();
            MouseMovement.CursorRect = CursorRect;
            UserInput = new InputHandler();
            ListMessages = new Message();
            ListMessages.messages = new List<Message>();

            #region Scene

            Scale = 1f;
            Gravity = 500f;
            GroundHeight = 0f;

            Scene = new SceneClass();
            SceneObject = new SceneObjects();
            SceneLoad = new LoadScene();
            MiniGame_BuildTheRocket = new BuildTheRocket(); // MiniGame - Build The Rocket
            BTR_ScrapMetal = new ScrapMetal(); // Scrap Metal for "Build The Rocket" Minigame
            // SceneObject.MiniGame_BuildTheRocket = MiniGame_BuildTheRocket;
            // SceneLoad.Button = Button;
            SceneLoad.Debug = Debug;
            SceneLoad.CursonRect = CursorRect;
            SceneLoad.CentreScreen = CentreScreen;
            SceneLoad.UserInput = UserInput;
            SceneLoad.MiniGame_BuildTheRocket = MiniGame_BuildTheRocket;
            //SceneLoad.BTR_ScrapMetal = BTR_ScrapMetal;
            BTR_ScrapMetal.SceneLoad = SceneLoad;
            MiniGame_BuildTheRocket.BTR_ScrapMetal = BTR_ScrapMetal;
            MiniGame_BuildTheRocket.SceneLoad = SceneLoad;
            SceneLoad.ListMessages = ListMessages;
            SceneLoad.InitialiseScene();
            MiniGame_BuildTheRocket.Initialise();
            BTR_ScrapMetal.Initialize();

            #endregion

            #region Player/Enemy/Rocket

            Player = new PlayerClass();
            Player.Debug = Debug;
            Player.SpawnPosition = CentreScreen;
            Player.PlayerInScene = SceneLoad.PlayerInScene;
            Player.SceneLoad = SceneLoad;
            Player.Scale = Scale;
            Player.Gravity = Gravity;
            Player.GroundHeight = GroundHeight;
            Player.MiniGame_BuildTheRocket = MiniGame_BuildTheRocket;
            Player.InitialisePlayer();

            Enemy = new EnemyClass();
            Enemy.Debug = Debug;

            Rocket = new RocketClass();
            RocketParts = new RocketPart();
            Rocket.UserInput = UserInput;
            Rocket.Debug = Debug;
            Rocket.SpawnPosition = CentreScreen;
            Rocket.Scale = Scale;
            Rocket.Gravity = Gravity;
            Rocket.GroundHeight = GroundHeight;
            RocketParts.m_scale = Scale;
            Rocket.InitialiseRocket();

            #endregion

            #region World/Audio/Debugs
            World = new WorldClass();

            Audio = new AudioClass();
            Audio.Debug = Debug;

            //Debug.WriteToFile(SceneLoad.SceneID + "", true);

            Debug.WriteToFile("Finished Initializing Game", true);
            #endregion

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Debug.WriteToFile("Started Loading Game Textures", true);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
                Player.FaceLeft = Content.Load<Texture2D>("Player/treble-sketch_stand_left-v2");
                Player.FaceRight = Content.Load<Texture2D>("Player/treble-sketch_stand_right-v2");
                rocketParts[0] = Content.Load<Texture2D>("Rocket/engine-Titus-v2");
                rocketParts[1] = Content.Load<Texture2D>("Rocket/fuelTank-Medium-v2");
                SceneLoad.OutsideGrass = Content.Load<Texture2D>("Surface/surface-dirt1-v1");    
                SceneLoad.MainMenu_StartButton = Content.Load<Texture2D>("Menu/menu-StartGameButton-v1");
                SceneLoad.MainMenu_StartButton_Hover = Content.Load<Texture2D>("Menu/menu-StartGameButton-v1-hover");
                SceneLoad.MainMenu_StartButton_Clicked = Content.Load<Texture2D>("Menu/menu-StartGameButton-v1-clicked");
                BTR_ScrapMetal.tex_ScrapMetal = Content.Load<Texture2D>("MiniGame/miniGame-ScrapMetal-v1");
                Audio.Bright_DJStartchAttack = Content.Load<Song>("Audio/Bright by DJStratchAttack");
                Debug.InformationFont = Content.Load<BitmapFont>("informationfont");
                Debug.DebugFont = Content.Load<BitmapFont>("debugfont");
                Debug.scoreText = Content.Load<SpriteFont>("scoreFont");
                SceneObject.scene_TextureError = Content.Load<Texture2D>("scene-errorTexturev1");
                MouseMovement.MouseTexture = Content.Load<Texture2D>("Cursor-v1");
                MouseMovement.MouseTexturePressed = Content.Load<Texture2D>("Cursor-v1-clicked");

            Debug.WriteToFile("Finished Loading Game Textures", true);

            InitialiseRocketParts();
            Rocket.InitialiseRocket();
            Rocket.SetSize(new Vector2(rocketParts[0].Width, rocketParts[1].Height + rocketParts[0].Height));
        }

        public void InitialiseRocketParts()
        {
            // Rocket.Position = new Vector2(CentreScreen.X, 0);
            RocketPart FuelTank = new RocketPart(rocketParts[1], Rocket.SpawnPosition, new Vector2(rocketParts[1].Width, rocketParts[1].Height));
            RocketPart Engine = new RocketPart(rocketParts[0], Rocket.SpawnPosition, new Vector2(rocketParts[0].Width, rocketParts[0].Height));
            // RocketPart Capsule = new RocketPart(/*rocketParts[2]*/);
            Rocket.AddPart(FuelTank);
            Rocket.AddPart(Engine);
            // Rocket.AddPart(Capsule);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>  
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Debug.WriteToFile("Ending Game...", true);
                Exit();
            }

            MouseMovement.Update();

            SceneLoad.state = MouseMovement.state;

            SceneLoad.SceneLoader(spriteBatch, gameTime);

            if (SceneLoad.ScrapMetals.Count > 0)
            {
                SceneLoad.MiniGame = true;
            }
            else
            {
                SceneLoad.MiniGame = false;
            }

            #region Rocket/Player/MiniGames
            if (SceneLoad.RocketInScene)
            {
                Rocket.Update(gameTime);
                Rocket.IsGrounded = false;
            }
            //else if (!SceneLoad.RocketInScene)
            //{
            //    Debug.WriteToFile("Rocket is not being updated on screen");
            //}

            Audio.ToggleMusic(gameTime);

            SceneLoad.CheckCollisions(Player);
            SceneLoad.CheckCollisions(Rocket);
            SceneLoad.CheckCollisions(BTR_ScrapMetal);
            // SceneLoad.PlayerInScene = Player.PlayerInScene;

            if (SceneLoad.PlayerInScene)
            {
                Player.Update(gameTime);
                Player.IsGrounded = false;
                Player.PewPew = false;
                Player.CheckCollisions(BTR_ScrapMetal);
                // Debug.WriteToFile("Playing...");
            }
            //else if (!SceneLoad.PlayerInScene)
            //{
            //    Debug.WriteToFile("Player is not being updated on screen");
            //}

            if (SceneLoad.MiniGame)
            {
                foreach (ScrapMetal scrapMetal in SceneLoad.ScrapMetals)
                {
                    scrapMetal.IsGrounded = false;
                    scrapMetal.Update(gameTime);
                    //Debug.WriteToFile("Position via list: " + SceneLoad.ScrapMetals[0].m_position.ToString(), false);
                    //Debug.WriteToFile("Scale via list: " + SceneLoad.ScrapMetals[0].Scale, false);
                    //Debug.WriteToFile("Size via list: " + SceneLoad.ScrapMetals[0].m_size.ToString(), false);
                }

                //if (BTR_ScrapMetal.m_position.Y == 50)
                //{
                //    Debug.WriteToFile("ScrapMetal Y POSITION: " + BTR_ScrapMetal.m_position.ToString(), false);
                //}

                //Debug.WriteToFile("MiniGame 'Build The Rocket' is updating", false);
                //Debug.WriteToFile("Started checking for Scrap Metal Info", false);

                //Debug.WriteToFile("Position: " + BTR_ScrapMetal.m_position.ToString(), false);
                //Debug.WriteToFile("Size: " + BTR_ScrapMetal.m_size.ToString(), false);
                //Debug.WriteToFile("Scale: " + BTR_ScrapMetal.Scale.ToString(), false);

                //Debug.WriteToFile("Finished checking for Scrap Metal Info", false);
            }
            #endregion

            // Debug.WriteToFile("Mouse Intersecting with Button: " + UserInput.MouseInRectangle(Button).ToString());

            while (ListMessages.messages.Count > 0 && ListMessages.messages[0].Appeared + ListMessages.MaxAgeMessage < gameTime.TotalGameTime)
            {
                ListMessages.messages.RemoveAt(0);
                Debug.WriteToFile("Message being removed", false);
            }

            InputHandler.Update();

            base.Update(gameTime);
        }

        //void DrawRectangle(Rectangle coords, Color color)
        //{
        //    var rect = new Texture2D(GraphicsDevice, 1, 1);
        //    rect.SetData(new[] { color });
        //    spriteBatch.Draw(rect, coords, color);
        //}

        #region Draw UI
        
        public void BTR_UI()
        {
            spriteBatch.DrawString // Score
                        (Debug.scoreText,
                        "Score: " + MiniGame_BuildTheRocket.Score,
                        new Vector2(20, 45), Color.Black);

            //spriteBatch.DrawString // Rockets Built
            //            (Debug.scoreText,
            //            "blah blah",
            //            new Vector2(150, 45), Color.White);

            //spriteBatch.DrawString // Percentage Complete with rocket
            //            (Debug.scoreText,
            //            "blah blah",
            //            new Vector2(CentreScreen.X, 45), Color.White);

            //spriteBatch.DrawString // Time Remaining!
            //            (Debug.scoreText,
            //            "blah blah",
            //            new Vector2(CentreScreen.X, 45), Color.White);
        }

        #endregion

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (SceneLoad.SceneID == 1)
            {
                GraphicsDevice.Clear(Color.Black);
            } else
            {
                GraphicsDevice.Clear(Color.SkyBlue);
            }
            // GraphicsDevice.Clear(Color.Black);

            // Debug.WriteToFile("Started Drawing Game Textures");

            spriteBatch.Begin();

            // Debug.WriteToFile("Drawing Scene Textures");
            SceneLoad.Draw(gameTime, spriteBatch);

            // Debug.WriteToFile("Drawing Rocket Textures");
            if (SceneLoad.RocketInScene)
            {
                Rocket.Draw(spriteBatch);

                Rectangle tempRect = new Rectangle((int)Rocket.Position.X - 5, (int)Rocket.Position.Y - 5, 10, 10);
                //DrawRectangle(tempRect, Color.White);
            }
            //else if (!RocketInScene)
            //{
            //    Debug.WriteToFile("Rocket is not being drawn on screen");
            //}

            // Debug.WriteToFile("Drawing Player Textures");
            if (SceneLoad.PlayerInScene)
            {
                Player.Draw(spriteBatch, graphics);
            }
            //else if (!PlayerInScene)
            //{
            //    Debug.WriteToFile("Player is not being drawn on screen");
            //}

            // Debug.WriteToFile("Drawing Audio Name");
            //if (SceneLoad.SceneID == 1)
            //{
            //    Audio.CurrentSong(spriteBatch, Color.White);
            //} else {
            //    Audio.CurrentSong(spriteBatch, Color.Black);
            //}

            if (SceneLoad.SceneID == 5)
            {
                BTR_UI();
            }

            MouseMovement.Draw(spriteBatch);

            foreach (var message in ListMessages.messages)
                spriteBatch.DrawString(Debug.scoreText, message.Text, message.Position, Color.Lime);

            spriteBatch.End();

            // Debug.WriteToFile("Finshed Drawing Game Textures");

            base.Draw(gameTime);
        }
    }
}
