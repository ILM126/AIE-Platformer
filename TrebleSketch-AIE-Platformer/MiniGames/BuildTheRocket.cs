using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class BuildTheRocket
    {
        public ScrapMetal BTR_ScrapMetal;
        public FuelUnit BTR_FuelUnit;
        public LoadScene SceneLoad;
        public DevLogging Debug;
        public RocketClass Rocket;

        public RocketClass.LaunchVehicles LaunchVehicle;
        public List<RocketPart> parts;
        public int PlannedRocketHeight;

        public int ScrapMetalCollected;
        public int RocketFuelCollected; // To be changed to AmmountRocketFueled
        public int RocketsBuilt;
        public float PercentageOfRocketBuilt; // Not nessasary?

        public TimeSpan FiveMinGameTimer = new TimeSpan(0, 0, 5, 0, 0);
        
        int scrapMetalFrameCounter;
        int fuelUnitFrameCounter;
        Random randNum;

        public void Initialise()
        {
            scrapMetalFrameCounter = 0;
            randNum = new Random();
        }

        public void SetRocketHeight(RocketClass.LaunchVehicles rocket)
        {
            if (LaunchVehicle == rocket)
            {
                PlannedRocketHeight = 300;
            }
        }

        public void SpawnScrapMetals()
        {
            scrapMetalFrameCounter++;

            if (scrapMetalFrameCounter > 150 && SceneLoad.ScrapMetals.Count < 20)
            {
                Vector2 pos = new Vector2(randNum.Next(20, 1000), randNum.Next(20, 400)); // Temporary...
                ScrapMetal scrapMetal = new ScrapMetal(
                        BTR_ScrapMetal.tex_ScrapMetal,
                        pos,
                        new Vector2(50, 30),
                        1f);
                SceneLoad.ScrapMetals.Add(scrapMetal);
                scrapMetalFrameCounter = 0;
            }
        }

        public void SpawnFuelUnits()
        {
            fuelUnitFrameCounter++;

            if (fuelUnitFrameCounter > 450 && SceneLoad.FuelUnits.Count < 10)
            {
                Vector2 pos = new Vector2(randNum.Next(20, 1000), randNum.Next(20, 400)); // Temporary...
                FuelUnit fuelUnit = new FuelUnit(
                        BTR_FuelUnit.tex_FuelUnit,
                        pos,
                        new Vector2(50, 30),
                        1f);
                SceneLoad.FuelUnits.Add(fuelUnit);
                fuelUnitFrameCounter = 0;
            }
        }

        void RandomPostion()
        {
            // Grab number of blocks via Count, minus it by one. Random Next thingy
            // randNum.Next(0, groundTiles - 1)
        }

        public void Update(GameTime gameTime)
        {
            parts = Rocket.parts;

            SetRocketHeight(RocketClass.LaunchVehicles.LightLauncher_Magpie_Crewed);

            RocketBuild(gameTime);

            Rocket.SetSize(new Vector2(Rocket.rocketParts[0].Width, Rocket.rocketParts[2].Height + Rocket.rocketParts[1].Height + Rocket.rocketParts[0].Height));
        }

        public void RocketBuild(GameTime gameTime) // Increments of 25 for one scrapmetal
        {
            int ScrapMetalNeeded = PlannedRocketHeight / 25;

            if (ScrapMetalCollected >= ScrapMetalNeeded)
            {
                // fire the rocket, next level etc.

                ScrapMetalCollected = 0;
                RocketsBuilt++;
                Debug.WriteToFile("Rockets now built: " + RocketsBuilt, false, false);
            }
            
            if (ScrapMetalCollected == 3 && parts.Count == 0)
            {
                RocketPart Engine = new RocketPart(RocketPart.PartType.Engine_Titus, Rocket.rocketParts[0], Rocket.SpawnPosition, new Vector2(Rocket.rocketParts[0].Width, Rocket.rocketParts[0].Height));
                Rocket.AddPart(Engine);
            }
            else if (ScrapMetalCollected == 7 && parts.Count == 1)
            {
                RocketPart FuelTank = new RocketPart(RocketPart.PartType.FuelTank_Medium, Rocket.rocketParts[1], Rocket.SpawnPosition, new Vector2(Rocket.rocketParts[1].Width, Rocket.rocketParts[1].Height));
                Rocket.AddPart(FuelTank);
            }
            else if (ScrapMetalCollected == 2 && parts.Count == 2)
            {
                RocketPart Capsule = new RocketPart(RocketPart.PartType.Capsule_Manned_PipingShrike, Rocket.rocketParts[2], Rocket.SpawnPosition, new Vector2(Rocket.rocketParts[2].Width, Rocket.rocketParts[2].Height));
                Rocket.AddPart(Capsule);
            } else
            {
                Debug.WriteToFile("RocketBuild is being updated", false, false);
            }

            if (parts.Count == 3)
            {
                float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Rocket.Position.Y < 0)
                {
                    Rocket.Velocity.Y += 1 * time;
                } else if (Rocket.Position.Y == 0)
                {
                    Debug.WriteToFile("Rocket Flight Animation complete", true, false);
                    Rocket.parts.Clear();
                    Rocket.InitialiseRocket();
                }
            }
        }
    }
}
