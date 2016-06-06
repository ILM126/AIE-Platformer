using System;
using Microsoft.Xna.Framework;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class BuildTheRocket
    {
        public ScrapMetal BTR_ScrapMetal;
        public FuelUnit BTR_FuelUnit;
        public LoadScene SceneLoad;
        public DevLogging Debug;

        public RocketClass.LaunchVehicles LaunchVehicle;
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

        public void Update()
        {
            SetRocketHeight(RocketClass.LaunchVehicles.LightLauncher_Magpie);

            RocketBuild();
        }

        public void RocketBuild() // Increments of 25 for one scrapmetal
        {
            int ScrapMetalNeeded = PlannedRocketHeight / 25;

            if (ScrapMetalCollected >= ScrapMetalNeeded)
            {
                // fire the rocket, next level etc.

                ScrapMetalCollected = 0;
                RocketsBuilt++;
                Debug.WriteToFile("Rockets now built: " + RocketsBuilt, false, false);
            }
            
            
        }
    }
}
