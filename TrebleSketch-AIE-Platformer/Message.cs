using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TrebleSketch_AIE_Platformer
{
    /// <summary>
    /// For more info: http://gamedev.stackexchange.com/questions/28532/timer-for-pop-up-text-in-xna
    /// </summary>
    class Message
    {
        public List<Message> messages;
        public TimeSpan messageDisappear = new TimeSpan(0, 0, 0, 0, 125);
        public TimeSpan MaxAgeMessage = new TimeSpan(0, 0, 0, 5, 0);

        public TimeSpan timeNow = new TimeSpan(0, 0, 0, 0, 0);

        public Vector2 MessagePosition;

        public string Text { get; set; }
        public TimeSpan Appeared { get; set; }
        public Vector2 Position { get; set; }
    }
}
