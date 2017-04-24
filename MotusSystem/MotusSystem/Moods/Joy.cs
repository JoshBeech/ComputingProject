using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Joy : Mood
    {
        private State Joyful = new State(e_MoodStates.JOYFUL);
        private State Pride = new State(e_MoodStates.PRIDE);
        private State Delight = new State(e_MoodStates.DELIGHT);
        private State Couragous = new State(e_MoodStates.COURAGE);
        private State Love = new State(e_MoodStates.LOVE);

        public Joy()
        {
            MoodID = e_EmotionsState.JOY;
        }

        protected override void SetBlendedMood()
        {
            // All but Fear and Disgust
            switch (SecondaryMoodID)
            {
                case e_EmotionsState.ANGER:
                    CurrentState = Pride;
                    break;
                case e_EmotionsState.ANTICIPATION:
                    CurrentState = Couragous;
                    break;
                case e_EmotionsState.SURPRISE:
                    CurrentState = Delight;
                    break;
                case e_EmotionsState.TRUST:
                    CurrentState = Love;
                    break;
                default:
                    CurrentState = Joyful;
                    break;
            }
        }
    }
}
