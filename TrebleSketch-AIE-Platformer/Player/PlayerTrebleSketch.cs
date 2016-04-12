using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using TrebleSketch_AIE_Platformer.Player;

namespace TrebleSketch_AIE_Platformer
{
    class PlayerTrebleSketch
    {
        PlayerClass Player;

        // Player Values
        //public Vector2 SpawnPosition;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Acceleration;
        //public Vector2 Size;
        public float Rotation;
        public float RotationDelta;

        public Texture2D FaceRight;
        public Texture2D FaceLeft;

        public void InitializeTrebleSketch(GraphicsDeviceManager graphics)
        {
            Player.PlayerFacingRight = true;
            Player.PlayerFacingRight = false;

            // Player.SpawnPosition = Player.Position;
            Position = new Vector2(graphics.PreferredBackBufferWidth / 2
                    , graphics.PreferredBackBufferHeight / 2);
            Velocity = new Vector2(0, 0);
            Acceleration = Velocity.X;
            Rotation = 0;
            // Player.Size = new Vector2(85.0f, 85.0f);
        }
    }
}
