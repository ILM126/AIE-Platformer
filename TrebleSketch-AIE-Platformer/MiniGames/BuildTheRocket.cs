using System;
using Microsoft.Xna.Framework;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class BuildTheRocket
    {
        public ScrapMetal BTR_ScrapMetal;
        public LoadScene SceneLoad;
        public DevLogging Debug;

        public enum LaunchVehicles
        {
            LightLauncher_Magpie
        }

        public LaunchVehicles LaunchVehicle;
        public int PlannedRocketHeight;

        public int ScrapMetalCollected;
        public int RocketFuelCollected;
        public int RocketsBuilt;
        public float PercentageOfRocket;

        public TimeSpan FiveMinGameTimer = new TimeSpan(0, 0, 5, 0, 0);
        
        int frameCounter;
        Random randNum;

        public void Initialise()
        {
            frameCounter = 0;
            randNum = new Random();
        }

        public void SetRocketHeight(LaunchVehicles rocket)
        {
            if (LaunchVehicle == rocket)
            {
                PlannedRocketHeight = 300;
            }
        }

        public void SpawnScrapMetals()
        {
            frameCounter++;

            if (frameCounter > 150 && SceneLoad.ScrapMetals.Count < 20)
            {
                Vector2 pos = new Vector2(randNum.Next(20, 1000), randNum.Next(20, 400)); // Temporary...
                ScrapMetal scrapMetal = new ScrapMetal(
                        BTR_ScrapMetal.tex_ScrapMetal,
                        pos,
                        new Vector2(50, 30),
                        1f);
                SceneLoad.ScrapMetals.Add(scrapMetal);
                frameCounter = 0;
            }

        }

        void RandomPostion()
        {
            // Grab number of blocks via Count, minus it by one. Random Next thingy
            // randNum.Next(0, groundTiles - 1)
        }

        public void Update()
        {
            SetRocketHeight(LaunchVehicles.LightLauncher_Magpie);

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
                Debug.WriteToFile("Rockets now built: " + RocketsBuilt, false);
            }
        }
    }
}
