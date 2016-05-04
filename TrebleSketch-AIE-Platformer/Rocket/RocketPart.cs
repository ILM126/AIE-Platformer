using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrebleSketch_AIE_Platformer
{
    class RocketPart : SceneObjects
    {
        public Texture2D m_texture;
        public Vector2 m_size;
        public Vector2 m_position;
        public float m_scale;

        public RocketPart(Texture2D tex = null, Vector2 pos = new Vector2(), Vector2 size = new Vector2(), float scale = 1f)
        {
            m_texture = tex;
            m_position = pos;
            m_size = size;
            m_scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture = null)
        {
            Texture2D tex = texture; //Try to use the parameter texture
            if (tex == null) tex = m_texture; //If none was set try to use the base m_texture
            if (tex == null) /*Console.WriteLine("[ERROR] Texture Null");*/ return; //if the base m_texture is null then don't crash trying to draw nothing
            spriteBatch.Draw(tex
            , new Vector2(m_position.X
                , m_position.Y)
            , null
            , Color.White
            , 0
            , new Vector2(tex.Width / 2
                , tex.Height / 2)
            , new Vector2(m_scale)
            , SpriteEffects.None
            , 0);
        }


    }
}