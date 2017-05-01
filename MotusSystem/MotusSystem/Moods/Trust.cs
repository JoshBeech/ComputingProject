using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Trust : Mood
    {
        public Trust()
        {
            MoodID = e_EmotionsState.TRUST;

            MoodStates.Add(e_EmotionsState.TRUST, new State("Trusting"));
            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State("Fatalistic"));
            MoodStates.Add(e_EmotionsState.JOY, new State("Love"));

            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
