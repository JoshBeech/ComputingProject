using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Anticipation : Mood
    {
        //private State Awaiting = new State(e_MoodStates.AWAITING);
        //private State Optimistic = new State(e_MoodStates.OPTIMISTIC);
        //private State Stubborn = new State(e_MoodStates.STUBBORN);
        //private State Cautious = new State(e_MoodStates.CAUTIOUS);
        //private State Cynical = new State(e_MoodStates.CYNICISM);

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

        //protected override void SetBlendedMood()
        //{
        //    switch(SecondaryMoodID)
        //    {
        //        case e_EmotionsState.ANGER:
        //            CurrentState = Stubborn;
        //            break;
        //        case e_EmotionsState.DISGUST:
        //            CurrentState = Cynical;
        //            break;
        //        case e_EmotionsState.FEAR:
        //            CurrentState = Cautious;
        //            break;
        //        case e_EmotionsState.JOY:
        //            CurrentState = Optimistic;
        //            break;
        //        default:
        //            CurrentState = Awaiting;
        //            break;
        //    }
        //}
    }
}
