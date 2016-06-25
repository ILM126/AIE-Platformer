using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using EclipsingGameUtils;
using AIEResources.ParticleEffects;
using Microsoft.Xna.Framework.Graphics;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class BuildTheRocket
    {
        public ScrapMetal BTR_ScrapMetal;
        public FuelUnit BTR_FuelUnit;
        public LoadScene SceneLoad;
        public DevLogging Debug;
        public RocketClass Rocket;
        Emitter ParticleEmitter;

        public RocketClass.LaunchVehicles LaunchVehicle;
        public RocketPart.PartTypes PartType;
        public List<RocketPart> parts;

        public float PlannedRocketHeight;
        public float PlannedRocketFuel;
        public float ScrapMetalNeeded;
        public float fuelTotal;
        public int ScrapMetalCollected;
        public int RocketFuelCollected; // To be changed to AmountRocketFueled
        public int RocketsBuilt;
        public int RocketsLiftedOff;
        public bool rocketFuelFull;
        public bool ReadyForLiftOff;
        public bool LiftOff;
        public bool Reset;
        bool finishedRocket;
        bool actualLiftOff;
        bool poof;
        int index;

        public TimeSpan FiveMinGameTimer = new TimeSpan(0, 0, 5, 0, 0);
        public TimeSpan LiftOffTimer = new TimeSpan(0, 0, 0);
        public TimeSpan LiftOffTime = new TimeSpan(0, 0, 4);
        float launchTime;
        public bool startEmitting;

        int screenWidth;
        int screenHeight;
        int scrapMetalFrameCounter;
        int fuelUnitFrameCounter;

        Random randNum;

        public void Initialise()
        {
            scrapMetalFrameCounter = 0;
            randNum = new Random();
        }

        public void LoadBTR()
        {
            ReadyForLiftOff = false;
            rocketFuelFull = false;
            LiftOff = false;
            Reset = false;
            Vector2 pos = new Vector2(randNum.Next(20, (int)SceneLoad.CentreScreen.X * 2 - 50), randNum.Next(20, (int)SceneLoad.CentreScreen.Y * 2 - 60)); // Temporary...
            ScrapMetal scrapMetal = new ScrapMetal(
                    BTR_ScrapMetal.tex_ScrapMetal,
                    pos,
                    new Vector2(50, 30),
                    1f);
            SceneLoad.ScrapMetals.Add(scrapMetal);
            launchTime = 0f;
            poof = false;
        }

        public void ResetRocketBuild()
        {
            parts.Clear();
            RocketsLiftedOff++;
            ScrapMetalCollected = 0;
            RocketFuelCollected = 0;
            fuelTotal = 0f;
            launchTime = 0f;
            ReadyForLiftOff = false;
            rocketFuelFull = false;
            LiftOff = false;
            Reset = false;
            Rocket.Position = SceneLoad.CentreScreen;
            poof = false;
        }   

        public void SetRocketHeight(RocketClass.LaunchVehicles rocket)
        {
            if (LaunchVehicle == rocket)
            {
                PlannedRocketHeight = 300;
                PlannedRocketFuel = 2000;
            }
        }

        public void SpawnScrapMetals()
        {
            scrapMetalFrameCounter++;
            int scrapMetalRandomFrames = randNum.Next(165, 205);

            if (scrapMetalFrameCounter > scrapMetalRandomFrames && SceneLoad.ScrapMetals.Count < 20 && ScrapMetalCollected < ScrapMetalNeeded)
            {
                Vector2 pos = new Vector2(randNum.Next(20, (int)SceneLoad.CentreScreen.X * 2 - 50), randNum.Next(20, (int)SceneLoad.CentreScreen.Y * 2- 60)); // Temporary...
                ScrapMetal scrapMetal = new ScrapMetal(
                        BTR_ScrapMetal.tex_ScrapMetal,
                        pos,
                        new Vector2 (BTR_ScrapMetal.tex_ScrapMetal.Width, BTR_ScrapMetal.tex_ScrapMetal.Height),
                        1f);
                SceneLoad.ScrapMetals.Add(scrapMetal);
                scrapMetalFrameCounter = 0;
            }
            CheckScrapMetalPosition();
        }

        public void SpawnFuelUnits()
        {
            fuelUnitFrameCounter++;
            int fuelUnitRandomFrames = randNum.Next(375, 450);

            if (fuelUnitFrameCounter > fuelUnitRandomFrames && SceneLoad.FuelUnits.Count < 10 && RocketFuelCollected < PlannedRocketFuel)
            {
                Vector2 pos = new Vector2(randNum.Next(20, (int)SceneLoad.CentreScreen.X * 2 - 50), randNum.Next(20, (int)SceneLoad.CentreScreen.Y * 2 - 60)); // Temporary...
                FuelUnit fuelUnit = new FuelUnit(
                        BTR_FuelUnit.tex_FuelUnit,
                        pos,
                        new Vector2(BTR_FuelUnit.tex_FuelUnit.Width, BTR_FuelUnit.tex_FuelUnit.Height),
                        1f);
                SceneLoad.FuelUnits.Add(fuelUnit);
                fuelUnitFrameCounter = 0;
            }
            CheckFuelUnitPosition();
        }

        void CheckFuelUnitPosition()
        {
            int ToRemove = -1;
            foreach (FuelUnit fuelUnit in SceneLoad.FuelUnits)
            {
                ToRemove = SceneLoad.FuelUnits.IndexOf(fuelUnit);
            }
            if (ToRemove != -1)
            {
                if (SceneLoad.FuelUnits[ToRemove].m_position.Y > SceneLoad.CentreScreen.Y * 2)
                {
                    SceneLoad.FuelUnits.RemoveAt(ToRemove);
                    Debug.WriteToFile("Fuel Unit no. " + ToRemove + " is now deleted due to it being outside the screen", false, false);
                }
            }
        }

        void CheckScrapMetalPosition()
        {
            int ToRemove = -1;
            foreach (ScrapMetal scrapMetal in SceneLoad.ScrapMetals)
            {
                ToRemove = SceneLoad.ScrapMetals.IndexOf(scrapMetal);
            }
            if (ToRemove != -1)
            {
                if (SceneLoad.ScrapMetals[ToRemove].m_position.Y > SceneLoad.CentreScreen.Y * 2)
                {
                    SceneLoad.ScrapMetals.RemoveAt(ToRemove);
                    Debug.WriteToFile("Scrap Metal no. " + ToRemove + " is now deleted due to it being outside the screen", false, false);
                }
            }
        }

        public void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            parts = Rocket.parts;

            SetRocketHeight(RocketClass.LaunchVehicles.LightLauncher_Magpie_Crewed);

            RocketBuild(gameTime, spriteBatch);

            //(int)SceneLoad.CentreScreen.X - 200 = screenWidth;
            //screenHeight = (int)SceneLoad.CentreScreen.Y - 20;

            //Debug.WriteToFile("Rocket Engine Position after RocketBuild(): " + parts[0].m_position.ToString(), true, false);

            switch (parts.Count)
            {
                case 0:
                    Rocket.SetSize(new Vector2(0, 0));
                    break;
                case 1:
                    Rocket.SetSize(new Vector2(Rocket.rocketParts[0].Width, Rocket.rocketParts[0].Height));
                    break;
                case 2:
                    Rocket.SetSize(new Vector2(Rocket.rocketParts[0].Width, Rocket.rocketParts[0].Height + Rocket.rocketParts[1].Height));
                    break;
                case 3:
                    Rocket.SetSize(new Vector2(Rocket.rocketParts[0].Width, Rocket.rocketParts[0].Height + Rocket.rocketParts[1].Height + Rocket.rocketParts[2].Height));
                    break;
            }
            //Debug.WriteToFile("Rocket Engine Position after SetSize in BTR: " + parts[0].m_position.ToString(), true, false);
        }

        public void RocketBuild(GameTime gameTime, SpriteBatch spriteBatch) // Increments of 25 for one scrapmetal
        {
            ScrapMetalNeeded = PlannedRocketHeight / 25;

            // Needed for future modular fuel loading
            fuelTotal = 0;
            foreach (RocketPart part in parts)
            {
                if (part.m_type == RocketPart.PartTypes.FuelTank_Large || part.m_type == RocketPart.PartTypes.FuelTank_Medium || part.m_type == RocketPart.PartTypes.FuelTank_Small)
                {
                    fuelTotal += part.fuelTankSize;
                    //Debug.WriteToFile("Fuel Total: " + fuelTotal, false, false);
                }
            }

            Cheats();

            #region Actual Rocket Build
            if (ScrapMetalCollected == 3 && parts.Count == 0) // 3
            {
                RocketPart Engine = new RocketPart(RocketPart.PartTypes.Engine_Titus, Rocket.rocketParts[0], Rocket.SpawnPosition, new Vector2(Rocket.rocketParts[0].Width, Rocket.rocketParts[0].Height));
                Rocket.AddPart(Engine);
                Debug.WriteToFile("Player has constructed the 'Titus' Kerlox Engine", true, false);
                //Debug.WriteToFile("Engine Spawned at: " + parts[0].m_position.ToString(), true, false);
                //Debug.WriteToFile("Engine Velosity now: " + Rocket.Velocity.ToString(), false, false);
            }
            else if (ScrapMetalCollected == 10 && parts.Count == 1) // 10
            {
                RocketPart FuelTank = new RocketPart(RocketPart.PartTypes.FuelTank_Medium, Rocket.rocketParts[1], Rocket.SpawnPosition, new Vector2(Rocket.rocketParts[1].Width, Rocket.rocketParts[1].Height));
                Rocket.AddPart(FuelTank);
                Debug.WriteToFile("Player has constructed the Medium sized Fuel Tank", true, false);
            }
            else if (ScrapMetalCollected == 12 && parts.Count == 2) // 12
            {
                RocketPart Capsule = new RocketPart(RocketPart.PartTypes.Capsule_Manned_PipingShrike, Rocket.rocketParts[2], Rocket.SpawnPosition, new Vector2(Rocket.rocketParts[2].Width, Rocket.rocketParts[2].Height));
                Rocket.AddPart(Capsule);
                Debug.WriteToFile("Player has constructed the 'Piping Shrike' Manned Capsule", true, false);
                finishedRocket = true;
            } else
            {
                //Debug.WriteToFile("RocketBuild is being updated", false, false);
            }
            #endregion

            if (parts.Count == 3 && RocketFuelCollected == PlannedRocketFuel) // Rocket Animation
            {
                ReadyForLiftOff = true;

                //float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Debug.WriteToFile("Rocket Flight Animation complete", true, false);
                //Rocket.parts.Clear();
                //Rocket.InitialiseRocket();

                //if (Rocket.Position.Y < 0)
                //{
                //    //Rocket.Velocity.Y += 1 * time;
                //}
                //else /*if (Rocket.Position.Y == 0)*/
                //{
                //    Debug.WriteToFile("Rocket Flight Animation complete", true, false);
                //    Rocket.parts.Clear();
                //    Rocket.InitialiseRocket();
                //}
            }

            RocketLaunch(gameTime, spriteBatch);

            if (ScrapMetalCollected == ScrapMetalNeeded && finishedRocket)
            {
                RocketsBuilt++;
                finishedRocket = false;
            }

            if (Reset)
            {
                Debug.WriteToFile(RocketsBuilt + " rockets now built + launched", false, false);
                Debug.WriteToFile("Rocket " + RocketsBuilt + " just lifted off!", true, false);
                Reset = false;
            }
        }

        public void RocketLaunch(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (parts.Count == 3 && RocketFuelCollected == PlannedRocketFuel)
            {
                if (LiftOff)
                {
                    float time = (float)gameTime.TotalGameTime.Seconds;

                    if (!poof)
                    {
                        launchTime = time + 3;
                        Debug.WriteToFile("poofed!", false, false);
                        Debug.WriteToFile("launchTime: " + launchTime, false, false);
                        Debug.WriteToFile("time: " + time, false, false);
                        poof = true;
                    }

                    if (launchTime == time)
                    {
                        launchTime = 0;
                        actualLiftOff = true;
                        Debug.WriteToFile("If time equal launchTime", false, false);
                        Debug.WriteToFile("launchTime: " + launchTime, false, false);
                        Debug.WriteToFile("time: " + time, false, false);
                    }

                    if (index == (int)RocketPart.PartTypes.Engine_Titus)
                    {
                        Vector2 enginePosition;
                        enginePosition = Rocket.parts[index].m_position;
                        ParticleEmitter = Emitter.CreateFireEmitter(Rocket.particles_RocketExhaust, enginePosition);
                        ParticleEmitter.position = enginePosition;

                        Debug.WriteToFile("partivle position: " + enginePosition.ToString(), false, false);

                        ParticleEmitter.Update(gameTime);

                        startEmitting = true;

                        //RocketParticles(spriteBatch);
                    } else if (index != (int)RocketPart.PartTypes.Engine_Titus) {
                        foreach (RocketPart part in parts)
                        {
                            List<RocketPart.PartTypes> partsList = Enum.GetValues(typeof(RocketPart.PartTypes)).Cast<RocketPart.PartTypes>().ToList();
                            index = partsList.IndexOf(part.m_type);
                        }
                    }
                }

                if (actualLiftOff)
                {
                    Rocket.Velocity.Y -= 45f;
                }
            }
            if (LiftOff && Rocket.Position.Y < -1000f)
            {
                ResetRocketBuild();
                Rocket.Velocity.Y = 0;
                actualLiftOff = false;
                LiftOff = false;
                Reset = true;
                startEmitting = false;
                index = -1;
            }
            if (InputHandler.IsKeyDownOnce(Keys.R) && 0 < RocketFuelCollected || InputHandler.IsKeyDownOnce(Keys.R) && 0 < ScrapMetalCollected)
            {
                ResetRocketBuild();
            }
        }

        public void RocketParticles(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //ParticleEmitter.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Cheats()
        {
            if (InputHandler.IsKeyDownOnce(Keys.Y))
            {
                ScrapMetalCollected++;
            }
            if (InputHandler.IsKeyDownOnce(Keys.U))
            {
                RocketFuelCollected += 400;
            }
        }
    }
}
