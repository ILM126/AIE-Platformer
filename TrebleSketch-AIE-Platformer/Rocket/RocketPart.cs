﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrebleSketch_AIE_Platformer
{
    class RocketPart : SceneObjects
    {
        public Texture2D m_texture;
        public Vector2 m_size;
        public Vector2 m_position;

        public RocketPart(Texture2D tex = null, Vector2 pos = new Vector2())
        {
            m_texture = tex;
            m_position = pos;
        }

        

        public void Draw(SpriteBatch spriteBatch, Texture2D texture = null)
        {
            Texture2D tex = texture; //Try to use the parameter texture
            if (tex == null) tex = m_texture; //If none was set try to use the base m_texture
            if (tex == null) return; //if the base m_texture is null then don't crash trying to draw nothing
            spriteBatch.Draw(tex
            , new Vector2(m_position.X
                , m_position.Y)
            , null
            , Color.White
            , 0
            , new Vector2(tex.Width / 2
                , tex.Height / 2)
            , new Vector2(1, 1)
            , SpriteEffects.None
            , 0);
        }


    }
}