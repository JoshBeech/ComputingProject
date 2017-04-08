using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem
{
    /// <summary>
    /// Collection of states - combinations of emotions 
    /// </summary>
    internal class Mood
    {
        internal e_EmotionsState MoodID;
        public Mood()
        {

        }

        public Mood(e_EmotionsState p_ID)
        {
            MoodID = p_ID;
        }
    }
}
