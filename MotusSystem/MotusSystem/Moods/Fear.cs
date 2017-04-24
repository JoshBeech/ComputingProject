using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Fear : Mood
    {
        private State Fearful = new State(e_MoodStates.FEARFUL);
        private State Guilty = new State(e_MoodStates.GUILTY);
        private State Submissive = new State(e_MoodStates.SUBMISSIVE);
        private State Cowardess = new State(e_MoodStates.COWARDESS);

        public Fear()
        {
            MoodID = e_EmotionsState.FEAR;
        }

        protected override void SetBlendedMood()
        {
            switch(SecondaryMoodID)
            {
                case e_EmotionsState.ANTICIPATION:
                    CurrentState = Cowardess;
                    break;
                case e_EmotionsState.JOY:
                    CurrentState = Guilty;
                    break;
                case e_EmotionsState.TRUST:
                    CurrentState = Submissive;
                    break;
                default:
                    CurrentState = Fearful;
                    break;
            }
        }
    }
}
