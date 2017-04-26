﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Anger : Mood
    {
        //private State Angry = new State(e_MoodStates.ANGRY);
        //private State Envious = new State(e_MoodStates.ENVIOUS);
        //private State Aggresive = new State(e_MoodStates.AGRESSIVE);
        //private State Outraged = new State(e_MoodStates.OUTRAGED);
        //private State Dominant = new State(e_MoodStates.DOMINANT);
        //private State Hateful = new State(e_MoodStates.HATEFUL);

        public Anger()
        {
            MoodID = e_EmotionsState.ANGER;

            MoodStates.Add(e_EmotionsState.ANGER, new State());
            MoodStates.Add(e_EmotionsState.DISGUST, new State());
            MoodStates.Add(e_EmotionsState.SADNESS, new State());
            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State());
            MoodStates.Add(e_EmotionsState.SURPRISE, new State());
            MoodStates.Add(e_EmotionsState.TRUST, new State());
        }

        //protected override void SetBlendedMood()
        //{
        //    switch(SecondaryMoodID)
        //    {
        //        case e_EmotionsState.ANTICIPATION:
        //            CurrentState = Aggresive;
        //            break;
        //        case e_EmotionsState.DISGUST:
        //            CurrentState = Hateful;
        //            break;
        //        case e_EmotionsState.SADNESS:
        //            CurrentState = Envious;
        //            break;
        //        case e_EmotionsState.SURPRISE:
        //            CurrentState = Outraged;
        //            break;
        //        case e_EmotionsState.TRUST:
        //            CurrentState = Dominant;
        //            break;
        //        default:
        //            CurrentState = Angry;
        //            break;
        //    }
        //}
    }
}
