using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrebleSketch_AIE_Platformer;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class BuildTheRocket
    {
        public SquareCollision BoxCollision;

        public Texture2D ScrapMetal;
        public Vector2 CentreScreen;

        public Texture2D m_texture;
        public Vector2 m_size;
        public Vector2 m_position;
        public Vector2 m_origin;
        public float m_scale;

        public float Scale;

        public void Initialize()
        {
            BoxCollision = new SquareCollision(m_position, m_size);
            // SceneID = 5;
        }

        public BuildTheRocket(Texture2D texture = null, Vector2 position = new Vector2(), Vector2 size = new Vector2(), float scale = 1f)
        {
            m_texture = texture;
            m_position = position;
            m_size = size;
            m_origin = new Vector2(
                (int)m_size.X / 2,
                (int)m_size.Y / 2);
            m_scale = scale;
            UpdateBounds();
        }

        public void Update()
        {

        }

        public void SpawnScrapMetal()
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture = null)
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

            //Texture2D tex = texture; //Try to use the parameter texture
            //if (tex == null) tex = m_texture; //If none was set try to use the base m_texture
            //if (tex == null) /*Console.WriteLine("[ERROR] Texture Null");*/ return; //if the base m_texture is null then don't crash trying to draw nothing
            //spriteBatch.Draw(tex
            //, new Vector2(m_position.X
            //    , m_position.Y)
            //, null
            //, Color.White
            //, 0
            //, new Vector2(tex.Width / 2
            //    , tex.Height / 2)
            //, new Vector2(m_scale)
            //, SpriteEffects.None
            //, 0);
        }

        protected virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            BoxCollision = new SquareCollision(m_position, m_size * m_scale);
        }
        protected bool SquareCollisionCheck(PlayerClass pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
        }
    }
}
