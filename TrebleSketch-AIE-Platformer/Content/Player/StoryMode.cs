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
    class StoryMode
    {
        /// <summary>
        /// Funds (Pieces)
        /// Reputation (Respect)
        /// Science (Brains)
        /// </summary>

        // Player Backend Data
        protected float PiecesToAdd;
        protected float PiecesToRemove;

        protected float RespectToAdd;
        protected float RespectToRemove;

        protected float BrainsToAdd;
        protected float BrainsToRemove;


        // Player Frontend Data
        public string Name;
        string slugName;

        public float Pieces;
        public float Respect;
        public float Brains;
    }
}
