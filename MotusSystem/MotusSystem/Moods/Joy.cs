using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Joy : Mood
    {
        public Joy()
        {
            MoodID = e_EmotionsState.JOY;

            MoodStates.Add(e_EmotionsState.JOY, new State("Joyful"));
            MoodStates.Add(e_EmotionsState.ANGER, new State("Pride"));
            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State("Couragous"));
            MoodStates.Add(e_EmotionsState.SURPRISE, new State("Delight"));
            MoodStates.Add(e_EmotionsState.TRUST, new State("Friendly"));

            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
