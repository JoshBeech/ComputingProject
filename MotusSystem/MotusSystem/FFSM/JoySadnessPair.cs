using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem
{
    public class JoySadnessPair : FuzzyFSM
    {
        public Joy JoyState;
        public Contempt ContemptState;
        public Sadness SadnessState;

        public JoySadnessPair()
        {
            JoyState = new Joy();
            ContemptState = new Contempt();
            SadnessState = new Sadness();

            CurrentState = e_EmotionsState.CONTEMPT;
        }

        public void ReceiveSensation(Sensation p_Sensation)
        {
            Value += p_Sensation.Strength;
            Value = Utilities.MathUtilities.Clamp(Value, 1.0f, -1.0f);
            SetState();
        }

        private void SetState()
        {
            if(Value >= 0.5f)
            {
                CurrentState = e_EmotionsState.JOY; 
            }
            else if(Value <= -0.5f)
            {
                CurrentState = e_EmotionsState.SADNESS;
            }
            else
            {
                CurrentState = e_EmotionsState.CONTEMPT;
            }
        }
    }
}
