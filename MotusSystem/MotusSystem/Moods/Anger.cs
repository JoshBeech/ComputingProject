using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Anger : Mood
    {
        public Anger()
        {
            MoodID = e_EmotionsState.ANGER;

            MoodStates.Add(e_EmotionsState.ANGER, new State("Angry"));
            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State("Aggressive"));
            MoodStates.Add(e_EmotionsState.DISGUST, new State("Hateful"));
            MoodStates.Add(e_EmotionsState.SADNESS, new State("Envious"));
            MoodStates.Add(e_EmotionsState.SURPRISE, new State("Outraged"));
            MoodStates.Add(e_EmotionsState.TRUST, new State("Dominant"));

            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
