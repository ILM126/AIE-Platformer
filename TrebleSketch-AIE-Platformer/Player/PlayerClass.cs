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

namespace TrebleSketch_AIE_Platformer
{
    class PlayerClass
    {
        DevLogging Debug;

        // Player Scene Controls
        public int InScene;
        public bool PlayerFacingRight;
        public bool BothSidesPressed;
        public bool IsJumping;

        public PlayerTrebleSketch TrebleSketch;

        public void PlayerMovement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A)) // Press A
            {
                TrebleSketch.Velocity = new Vector2(-2.5f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) // Press D
            {
                TrebleSketch.Velocity = new Vector2(2.5f, 0);
            }

            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
            {
                TrebleSketch.Velocity = new Vector2(0, 0);
                BothSidesPressed = false;
            } else if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                TrebleSketch.Velocity = new Vector2(0, 0);
                BothSidesPressed = true;
            }

            TrebleSketch.Position += TrebleSketch.Velocity;
        }

        public PlayerClass()
        {
            TrebleSketch = new PlayerTrebleSketch();
        }
    }
}
