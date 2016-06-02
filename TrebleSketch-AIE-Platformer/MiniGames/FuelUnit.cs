using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class FuelUnit
    {
        public SquareCollision BoxCollision;
        public LoadScene SceneLoad;

        public Texture2D tex_FuelUnit;
        public float Gravity;
        public float GroundHeight;

        public Texture2D m_texture;
        public Vector2 m_velocity;
        public Vector2 m_size;
        public Vector2 m_position;
        public Vector2 m_origin;

        public float Scale;

        public bool IsGrounded;

        public FuelUnit(Texture2D texture = null, Vector2 position = new Vector2(), Vector2 size = new Vector2(), float scale = 1f)
        {
            m_texture = texture;
            m_position = position;
            m_size = size;
            m_origin = new Vector2(
                (int)m_size.X / 2,
                (int)m_size.Y / 2);
            Scale = scale;
            // UpdateBounds();
            Initialize();
        }

        public void Initialize()
        {
            Gravity = 20f;
            Scale = 1f;
        }

        public void Update()
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture = null)
        {
            Texture2D tex = texture; //Try to use the parameter texture
            if (tex == null) tex = m_texture; //If none was set try to use the base m_texture
            if (tex == null) /*Console.WriteLine("[ERROR] Texture Null");*/ return; //if the base m_texture is null then don't crash trying to draw nothing
            float scale = Scale;
            Rectangle srcRect = new Rectangle(
                                            0,
                                            0,
                                            (int)(m_size.X),
                                            (int)(m_size.Y));

            spriteBatch.Draw(tex
                , m_position
                , null
                , Color.White
                , 0
                , new Vector2(tex.Width / 2
                    , tex.Height / 2)
                , new Vector2(scale)
                , SpriteEffects.None
                , 0);
        }

        public void FuelManagement() // The size of the fuelunit will determine how much the rocket will be fueled!
        {

        }
    }
}
