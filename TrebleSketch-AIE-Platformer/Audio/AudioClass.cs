using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
                        Debug.WriteToFile(Bright_DJStartchAttack.Name.ToString() + " just played for the first time", true);
                    }
                } else {
                    if (last > new TimeSpan(0, 0, 0, 5, 0))
                    {
                        if (MediaPlayer.State != MediaState.Playing)
                        {
                            MediaPlayer.Play(Bright_DJStartchAttack); // PLAY DIS
                        }

                        /*if (MediaPlayer.State == MediaState.Playing)
                        {
                            MediaPlayer.Pause();
                            Console.WriteLine(Bright_DJStartchAttack.Name.ToString() + " just paused.");
                        } else if (MediaPlayer.State != MediaState.Playing && MediaPlayer.State == MediaState.Paused && MediaPlayer.State != MediaState.Stopped)
                        {
                            MediaPlayer.Resume();
                            Console.WriteLine(Bright_DJStartchAttack.Name.ToString() + " just resumed.");
                        }
                        if (MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused)
                        {
                            MediaPlayer.Play(Bright_DJStartchAttack); // PLAY DIS
                            Console.WriteLine(Bright_DJStartchAttack.Name.ToString() + " just played for second time.");
                        }*/
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
                Debug.WriteToFile(Bright_DJStartchAttack.Name.ToString() + " just stopped", true);
            }
        }

        //public void CurrentSong(SpriteBatch spriteBatch, Color colours)
        //{
        //    if (MediaPlayer.State == MediaState.Playing)
        //    {
        //        spriteBatch.DrawString(Debug.InformationFont, "Current Song: " + Bright_DJStartchAttack.Name.ToString(), new Vector2(10, 5), colours);
        //    } else
        //    {
        //        spriteBatch.DrawString(Debug.InformationFont, "Current Song: ", new Vector2(10, 5), colours);
        //    }

        //    if (MediaPlayer.IsRepeating)
        //    {
        //        spriteBatch.DrawString(Debug.InformationFont, "Repeating: " + MediaPlayer.IsRepeating.ToString(), new Vector2(10, 70), colours);
        //    } else if (!MediaPlayer.IsRepeating)
        //    {
        //        spriteBatch.DrawString(Debug.InformationFont, "Repeating: " + MediaPlayer.IsRepeating.ToString(), new Vector2(10, 70), colours);
        //    }
        //}
    }
}
