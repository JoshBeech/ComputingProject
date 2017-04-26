using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    class Neutral : Mood
    {
        //internal State m_Neutral = new State("Neutral");

        public Neutral()
        {
            MoodID = e_EmotionsState.NEUTRAL;
            SecondaryMoodID = e_EmotionsState.NEUTRAL;

            MoodStates.Add(e_EmotionsState.NEUTRAL, new State("Neutral"));
            CurrentMoodState = MoodStates[MoodID];
        }

        //protected override void SetBlendedMood()
        //{
        //    CurrentState = m_Neutral;
        //}
    }
}
