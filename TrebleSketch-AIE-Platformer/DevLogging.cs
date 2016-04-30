using System;
using System.IO;
using System.Diagnostics;
using MonoGame.Extended.BitmapFonts;

namespace TrebleSketch_AIE_Platformer
{
    class DevLogging
    {
        public BitmapFont DebugFont;
        public BitmapFont InformationFont;

        public void ShowDebug()
        {
            Debug.WriteLine("[INFO] LOL WAT MATE!");
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
        public void WriteToFile(string text, bool toConsole)
        {
            if (toConsole)
            {
                Console.WriteLine(text);
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(GetCurrentDirectory(), true))
                {
                    if (!toConsole)
                    {
                        writer.WriteLine("[DEBUG] " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt ") + text);
                        writer.Close();
                    }
                    else
                    {
                        writer.WriteLine("[INFO] " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt ") + text);
                        writer.Close();
                    }
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                WriteToFile("[DEBUG] SHIT, not again. Another Unauthorized Access Exception at StreamWriter, I thought I fixed it!", true);
            }
            catch (Exception)
            {
                WriteToFile("[DEBUG] StreamWriter encountered an exception", true);
            }
        }
    }
}
