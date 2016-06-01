using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TrebleSketch_AIE_Platformer;
using EclipsingGameUtils;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class BuildTheRocket
    {
        public ScrapMetal BTR_ScrapMetal;
        public LoadScene SceneLoad;

        public int Score;
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
            
        }

        public void RocketBuild() // Increments of 25 for one scrapmetal
        {
            
        }
    }
}
