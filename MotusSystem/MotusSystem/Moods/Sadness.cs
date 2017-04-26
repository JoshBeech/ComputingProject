using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Sadness : Mood
    {
        //private State Sad = new State(e_MoodStates.SAD);
        //private State Resigned = new State(e_MoodStates.RESIGNATION);
        //private State Pessimistic = new State(e_MoodStates.PESSIMISTIC);
        //private State Dispaired = new State(e_MoodStates.DISPAIR);
        //private State Miserable = new State(e_MoodStates.MISERY);
        //private State Disappointed = new State(e_MoodStates.DISAPPOINTMENT);

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

        //protected override void SetBlendedMood()
        //{
        //    switch (SecondaryMoodID)
        //    {
        //        case e_EmotionsState.ANTICIPATION:
        //            CurrentState = Pessimistic;
        //            break;
        //        case e_EmotionsState.DISGUST:
        //            CurrentState = Miserable;
        //            break;
        //        case e_EmotionsState.FEAR:
        //            CurrentState = Dispaired;
        //            break;
        //        case e_EmotionsState.SURPRISE:
        //            CurrentState = Disappointed;
        //            break;
        //        case e_EmotionsState.TRUST:
        //            CurrentState = Resigned;
        //            break;
        //        default:
        //            CurrentState = Sad;
        //            break;
        //    }

        //}
    }
}
