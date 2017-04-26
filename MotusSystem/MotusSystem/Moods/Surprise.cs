using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Surprise : Mood
    {
        //private State Surprised = new State(e_MoodStates.SURPRISED);
        //private State Shocked = new State(e_MoodStates.SHOCKED);
        //private State Embarrassed  = new State(e_MoodStates.EMBARRASSED);
        //private State Alarmed = new State(e_MoodStates.ALARMED);

        public Surprise()
        {
            MoodID = e_EmotionsState.SURPRISE;

            MoodStates.Add(e_EmotionsState.SURPRISE, new State("Surprised"));
            MoodStates.Add(e_EmotionsState.DISGUST, new State("Shocked"));
            MoodStates.Add(e_EmotionsState.FEAR, new State("Alarmed"));
            MoodStates.Add(e_EmotionsState.SADNESS, new State("Embarressed"));

            CurrentMoodState = MoodStates[MoodID];
        }

        //protected override void SetBlendedMood()
        //{
        //    switch(SecondaryMoodID)
        //    {
        //        case e_EmotionsState.DISGUST:
        //            CurrentState = Shocked;
        //            break;
        //        case e_EmotionsState.FEAR:
        //            CurrentState = Alarmed;
        //            break;
        //        case e_EmotionsState.SADNESS:
        //            CurrentState = Embarrassed;
        //            break;
        //        default:
        //            CurrentState = Surprised;
        //            break;
        //    }
        //}
    }
}
