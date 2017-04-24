using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    class Neutral : Mood
    {
        private State m_Neutral = new State(e_MoodStates.NEUTRAL);

        public Neutral()
        {
            MoodID = e_EmotionsState.NEUTRAL;
            SecondaryMoodID = e_EmotionsState.NEUTRAL;
            CurrentState = m_Neutral;
        }

        protected override void SetBlendedMood()
        {
            CurrentState = m_Neutral;
        }
    }
}
