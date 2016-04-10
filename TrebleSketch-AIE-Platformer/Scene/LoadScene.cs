using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
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
    class LoadScene : SceneClass
    {
        public void SceneLoader()
        {
            SceneID = 0;
            switch(SceneID)
            {
                case 0:
                    SceneName = "Test Map";
                    break;
                case 1:
                    SceneName = "Main Menu";
                    break;
                case 2:
                    SceneName = "Settings Menu";
                    break;
                case 3:
                    SceneName = "Front Lawns";
                    break;
                default:
                    SceneName = "Test Map";
                    break;
            }

            Console.WriteLine("[INFO] Loaded " + SceneName + ".");
        }
    }
}
