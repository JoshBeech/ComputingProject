using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Joy : Mood
    {
        //internal State Joyful = new State(e_MoodStates.JOYFUL);
        //internal State Pride = new State(e_MoodStates.PRIDE);
        //internal State Delight = new State(e_MoodStates.DELIGHT);
        //internal State Couragous = new State(e_MoodStates.COURAGE);
        //internal State Love = new State(e_MoodStates.LOVE);

        public Joy()
        {
            MoodID = e_EmotionsState.JOY;

            MoodStates.Add(e_EmotionsState.JOY, new State("Joyful"));
            MoodStates.Add(e_EmotionsState.ANGER, new State("Pride"));
            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State("Couragous"));
            MoodStates.Add(e_EmotionsState.SURPRISE, new State("Delight"));
            MoodStates.Add(e_EmotionsState.TRUST, new State("Friendly"));
        }

        //protected override void SetBlendedMood()
        //{
        //    // All but Fear and Disgust
        //    switch (SecondaryMoodID)
        //    {
        //        case e_EmotionsState.ANGER:
        //            CurrentState = Pride;
        //            break;
        //        case e_EmotionsState.ANTICIPATION:
        //            CurrentState = Couragous;
        //            break;
        //        case e_EmotionsState.SURPRISE:
        //            CurrentState = Delight;
        //            break;
        //        case e_EmotionsState.TRUST:
        //            CurrentState = Love;
        //            break;
        //        default:
        //            CurrentState = Joyful;
        //            break;
        //    }
        //}
    }
}
