using EclipsingGameUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace TrebleSketch_AIE_Platformer.MiniGames
{
    class ScrapMetal
    {
        public SquareCollision BoxCollision;
        public LoadScene SceneLoad;
        //public DevLogging Debug;
        //public Game1 RealGame;

        public Texture2D tex_ScrapMetal;
        //public Vector2 CentreScreen;

        public float Gravity;
        public float GroundHeight;

        public Texture2D m_texture;
        public Vector2 m_velocity;
        public Vector2 m_size;
        public Vector2 m_position;
        public Vector2 m_origin;

        public float Scale;

        public bool IsGrounded;

        public void Initialize()
        {
            // SceneID = 5;
            Gravity = 20f;
            Scale = 1f;
        }

        public ScrapMetal(Texture2D texture = null, Vector2 position = new Vector2(), Vector2 size = new Vector2(), float scale = 1f)
        {
            m_texture = texture;
            m_position = position;
            m_size = size;
            m_origin = new Vector2(
                (int)m_size.X / 2,
                (int)m_size.Y / 2);
            Scale = scale;
            UpdateBounds();
            Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (m_position.Y == 50)
            {
                Debug.WriteLine("[DEBUG] ScrapMetal Y POSITION: " + m_position.ToString());
            }

            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputHandler.IsKeyDownOnce(Keys.Up))
            {
                if (IsGrounded)
                {
                    IsGrounded = false;
                    m_velocity.Y -= 500f;
                }
            }

            if (!IsGrounded) m_velocity.Y += Gravity * time;
            else m_velocity.Y = 0;
            m_position.Y += m_velocity.Y * time;
            m_position.X += m_velocity.X;
            UpdateBounds();
        }

        public void DrawScrapMetal()
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

        protected virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            BoxCollision = new SquareCollision(m_position, m_size * Scale);
        }
        protected bool SquareCollisionCheck(SceneObjects pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
        }

        protected void SetGrounded(float groundHeight)
        {
            IsGrounded = true;
            GroundHeight = groundHeight;
            m_position.Y = groundHeight;
            UpdateBounds();
        }

        public bool CollisionCheck(SceneObjects other)
        {
            bool scrapMetalCollision = SquareCollisionCheck(other);
            if (scrapMetalCollision)
            {
                return true;
            }
            return false;
        }

        public bool CheckCollisionsGround(SceneObjects other)
        {
            bool scrapMetalCollision = SquareCollisionCheck(other);
            if (scrapMetalCollision)
            {
                /// if player position is above top of ground and player is falling
                if (m_position.Y < other.BoxCollision.min.Y && m_velocity.Y > 0)
                {
                    SetGrounded(other.BoxCollision.min.Y - m_origin.Y * Scale);
                }
                return true;
            }
            return false;
        }
    }
}
