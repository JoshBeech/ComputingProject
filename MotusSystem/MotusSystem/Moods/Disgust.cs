using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Disgust : Mood
    {
        public Disgust()
        {
            MoodID = e_EmotionsState.DISGUST;

            MoodStates.Add(e_EmotionsState.DISGUST, new State("Disgusted"));
            MoodStates.Add(e_EmotionsState.JOY, new State("Morbid"));
            MoodStates.Add(e_EmotionsState.SADNESS, new State("Remorseful"));
            MoodStates.Add(e_EmotionsState.SURPRISE, new State("Mortified"));

            CurrentMoodState = MoodStates[MoodID];
        }
    }
}
