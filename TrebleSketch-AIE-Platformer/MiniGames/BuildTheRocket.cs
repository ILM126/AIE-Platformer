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

        public int scrapMetalCount;
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

            if (frameCounter > 200 && SceneLoad.ScrapMetals.Count < 20)
            {
                Vector2 pos = new Vector2(randNum.Next(800), randNum.Next(200));
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

        }
    }
}
