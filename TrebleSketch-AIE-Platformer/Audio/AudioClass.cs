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
using MonoGame.Extended.BitmapFonts;

namespace TrebleSketch_AIE_Platformer
{
    class AudioClass
    {
        public DevLogging Debug;

        public Song Bright_DJStartchAttack;
        TimeSpan lastRepeatChange;
        TimeSpan lastAudioChange;
        bool playedOnce;

        public void ToggleMusic(GameTime gameTime)
        {
            TimeSpan last = gameTime.TotalGameTime - lastAudioChange;
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                if (!playedOnce)
                {
                    if (MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(Bright_DJStartchAttack); // PLAY DIS
                        MediaPlayer.Volume -= 0.75f;
                        playedOnce = true;
                    }
                } else {
                    if (last > new TimeSpan(0, 0, 0, 5, 0))
                    {
                        if (MediaPlayer.State != MediaState.Playing)
                        {
                            MediaPlayer.Play(Bright_DJStartchAttack); // PLAY DIS
                        }
                    }
                    lastAudioChange = gameTime.TotalGameTime;
                }
            }
            TimeSpan lastRepeat = gameTime.TotalGameTime - lastRepeatChange;
            if (Keyboard.GetState().IsKeyDown(Keys.L) && lastRepeat > new TimeSpan(0, 0, 0, 2, 0))
            {
                MediaPlayer.IsRepeating = !MediaPlayer.IsRepeating;
                lastRepeatChange = gameTime.TotalGameTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                MediaPlayer.Stop();
            }
        }

        public void CurrentSong(SpriteBatch spriteBatch)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                spriteBatch.DrawString(Debug.InformationFont, "Current Song: " + Bright_DJStartchAttack.Name.ToString(), new Vector2(10, 5), Color.Black);
            } else
            {
                spriteBatch.DrawString(Debug.InformationFont, "Current Song: ", new Vector2(10, 5), Color.Black);
            }

            if (MediaPlayer.IsRepeating)
            {
                spriteBatch.DrawString(Debug.InformationFont, "Repeating: " + MediaPlayer.IsRepeating.ToString(), new Vector2(10, 70), Color.Black);
            } else if (!MediaPlayer.IsRepeating)
            {
                spriteBatch.DrawString(Debug.InformationFont, "Repeating: " + MediaPlayer.IsRepeating.ToString(), new Vector2(10, 70), Color.Black);
            }
        }
    }
}
