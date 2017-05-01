using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Surprise : Mood
    {
        public Surprise()
        {
            MoodID = e_EmotionsState.SURPRISE;

            MoodStates.Add(e_EmotionsState.SURPRISE, new State("Surprised"));
            MoodStates.Add(e_EmotionsState.DISGUST, new State("Shocked"));
            MoodStates.Add(e_EmotionsState.FEAR, new State("Alarmed"));
            MoodStates.Add(e_EmotionsState.SADNESS, new State("Embarressed"));

            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
