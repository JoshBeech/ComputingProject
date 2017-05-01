using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    class Neutral : Mood
    {
        public Neutral()
        {
            MoodID = e_EmotionsState.NEUTRAL;
            SecondaryMoodID = e_EmotionsState.NEUTRAL;

            MoodStates.Add(e_EmotionsState.NEUTRAL, new State("Neutral"));
            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
