using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Anticipation : Mood
    {
        private State Awaiting = new State(e_MoodStates.AWAITING);
        private State Optimistic = new State(e_MoodStates.OPTIMISTIC);
        private State Stubborn = new State(e_MoodStates.STUBBORN);
        private State Cautious = new State(e_MoodStates.CAUTIOUS);
        private State Cynical = new State(e_MoodStates.CYNICISM);

        public Anticipation()
        {
            MoodID = e_EmotionsState.ANTICIPATION;
        }

        protected override void SetBlendedMood()
        {
            switch(SecondaryEmotion)
            {
                case e_EmotionsState.ANGER:
                    CurrentState = Stubborn;
                    break;
                case e_EmotionsState.DISGUST:
                    CurrentState = Cynical;
                    break;
                case e_EmotionsState.FEAR:
                    CurrentState = Cautious;
                    break;
                case e_EmotionsState.JOY:
                    CurrentState = Optimistic;
                    break;
                default:
                    CurrentState = Awaiting;
                    break;
            }
        }
    }
}
