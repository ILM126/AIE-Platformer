using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrebleSketch_AIE_Platformer.MiniGames;


namespace TrebleSketch_AIE_Platformer
{
    class SceneObjects
    {
        public Texture2D scene_Texture;
        public Texture2D scene_TextureError;
        public Vector2 scene_Position;
        public Vector2 scene_Size;
        public Vector2 scene_Origin;
        public float scene_Scale;
        public bool scene_IsMenu;

        public SquareCollision BoxCollision;

        public SceneObjects(Texture2D texture = null, Vector2 position = new Vector2(), Vector2 size = new Vector2(), float scale = 1f, bool isMenu = false)
        {
            scene_Texture = texture;
            scene_Position = position;
            scene_Size = size;
            scene_Origin = new Vector2(
                (int)scene_Size.X / 2,
                (int)scene_Size.Y / 2);
            scene_Scale = scale;
            scene_IsMenu = isMenu;
            UpdateBounds();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D drawTexture = null)
        {
            Texture2D texture = drawTexture;
            if (texture == null) texture = scene_Texture;
            if (texture == null) throw new Exception("No texture!");

            Rectangle srcRect = new Rectangle(
                                0,
                                0,
                                (int)(scene_Size.X),
                                (int)(scene_Size.Y));

            spriteBatch.Draw(
                texture,
                scene_Position,
                srcRect,
                Color.White,
                0,
                scene_Origin,
                scene_Scale,
                0,
                0);
        }

        protected virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            BoxCollision = new SquareCollision(scene_Position, scene_Size * scene_Scale);
        }
        protected bool SquareCollisionCheck(PlayerClass pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
        }
        protected bool SquareCollisionCheck(RocketClass pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
        }
        protected bool SquareCollisionCheck(ScrapMetal pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
        }
    }
}
