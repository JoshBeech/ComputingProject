using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Anticipation : Mood
    {
        public Anticipation()
        {
            MoodID = e_EmotionsState.ANTICIPATION;

            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State("Awaiting"));
            MoodStates.Add(e_EmotionsState.ANGER, new State("Stubborn"));
            MoodStates.Add(e_EmotionsState.DISGUST, new State("Cynical"));
            MoodStates.Add(e_EmotionsState.FEAR, new State("Cautious"));
            MoodStates.Add(e_EmotionsState.JOY, new State("Optimistic"));

            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
