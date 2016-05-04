using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class BuildTheRocket
    {
        public int SceneID;
        public Texture2D ScrapMetal;
        public Vector2 CentreScreen;

        public float Scale;

        public void Initialize()
        {
            // SceneID = 5;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                        ScrapMetal,
                        CentreScreen,
                        null,
                        Color.White,
                        0,
                        new Vector2(ScrapMetal.Width / 2, ScrapMetal.Height / 2),
                        Scale,
                        0,
                        0);
        }
    }
}
