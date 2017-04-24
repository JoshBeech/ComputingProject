using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Disgust : Mood
    {
        private State Disgusted = new State(e_MoodStates.DISGUSTED);
        private State Remorseful = new State(e_MoodStates.REMORSEFUL);
        private State Morbid = new State(e_MoodStates.MORBID);
        private State Mortified = new State(e_MoodStates.MORTIFIED);

        public Disgust()
        {
            MoodID = e_EmotionsState.DISGUST;
        }

        protected override void SetBlendedMood()
        {
            switch(SecondaryMoodID)
            {
                case e_EmotionsState.JOY:
                    CurrentState = Morbid;
                    break;
                case e_EmotionsState.SADNESS:
                    CurrentState = Remorseful;
                    break;
                case e_EmotionsState.SURPRISE:
                    CurrentState = Mortified;
                    break;
                default:
                    CurrentState = Disgusted;
                    break;
            }
        }
    }
}
