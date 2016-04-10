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
    class AudioClass
    {
        public Song Bright_DJStartchAttack;
        TimeSpan lastChange;

        public void ToggleMusic(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                if (MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.Play(Bright_DJStartchAttack); // PLAY DIS
                    MediaPlayer.Volume -= 0.75f;
                }
            }
            TimeSpan last = gameTime.TotalGameTime - lastChange;
            if (Keyboard.GetState().IsKeyDown(Keys.L) && last > new TimeSpan(0, 0, 0, 2, 0))
            {
                MediaPlayer.IsRepeating = !MediaPlayer.IsRepeating;
                lastChange = gameTime.TotalGameTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                MediaPlayer.Stop();
            }
        }
    }
}
