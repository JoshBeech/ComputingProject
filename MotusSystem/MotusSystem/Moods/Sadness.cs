using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Sadness : Mood
    {
        public Sadness()
        {
            MoodID = e_EmotionsState.SADNESS;

            MoodStates.Add(e_EmotionsState.SADNESS, new State("Sad"));
            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State("Pessimistic"));
            MoodStates.Add(e_EmotionsState.DISGUST, new State("Miserable"));
            MoodStates.Add(e_EmotionsState.FEAR, new State("Dispaired"));
            MoodStates.Add(e_EmotionsState.SURPRISE, new State("Disappointed"));
            MoodStates.Add(e_EmotionsState.TRUST, new State("Resigned"));

            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
