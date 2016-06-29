using System;
using System.IO;
using System.Diagnostics;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework.Graphics;

namespace TrebleSketch_AIE_Platformer
{
    class DevLogging // Version 6
    {
        public BitmapFont DebugFont;
        public BitmapFont InformationFont;
        public SpriteFont scoreText;

        public bool SwitchOnce;

        public void ShowDebug()
        {
            Debug.WriteLine("[INFO] LOL WAT MATE!");
            SwitchOnce = false;
        }

        public string GetCurrentDirectory()
        // http://stackoverflow.com/questions/26432887/c-sharp-access-to-path-denied
        // http://stackoverflow.com/questions/21726088/how-to-get-current-working-directory-path-c
        // http://stackoverflow.com/questions/17118537/an-unhandled-exception-of-type-system-unauthorizedaccessexception-occurred-in
        // And more SO code...
        {
            string currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Platformer");
            string currentFile = Path.ChangeExtension(currentDirectory, ".log");
            return currentFile;
        }

        /// <summary>
        /// All debugging shall be written to this
        /// </summary>
        /// <param name="text"></param>
        public void WriteToFile(string text, bool toConsole, bool warning)
        {
            if (toConsole || !toConsole && !warning)
            {
                Console.WriteLine("[INFO] " + text);
            }
            else if (toConsole || !toConsole && warning)
            {
                Console.WriteLine("[WARNING] " + text);
            }
            else
            {
                Console.WriteLine("[WARNING] Debugging Error. Unknown cause of crash");
                throw new System.InvalidOperationException("[WARNING] Debugging Error");
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(GetCurrentDirectory(), true))
                {
                    if (!toConsole && !warning)
                    {
                        writer.WriteLine("[DEBUG] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + text);
                        writer.Close();
                    }
                    else if (toConsole && !warning)
                    {
                        writer.WriteLine("[INFO] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + text);
                        writer.Close();
                    }
                    else if (toConsole || !toConsole && warning)
                    {
                        writer.WriteLine("[WARNING] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + text);
                        writer.Close();
                    }
                    else
                    {
                        writer.WriteLine("[WARNING] Debugging Error. Unknonw cause of crash.");
                        writer.Close();
                        throw new System.InvalidOperationException("[WARNING] Debugging Error");
                    }
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                WriteToFile("[DEBUG] SHIT, not again. Another Unauthorized Access Exception at StreamWriter, I thought I fixed it!", true, false);
            }
            catch (Exception)
            {
                WriteToFile("[DEBUG] StreamWriter encountered an exception", true, false);
            }
        }
    }
}
