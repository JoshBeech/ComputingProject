using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Fear : Mood
    {
        public Fear()
        {
            MoodID = e_EmotionsState.FEAR;

            MoodStates.Add(e_EmotionsState.FEAR, new State("Fearful"));
            MoodStates.Add(e_EmotionsState.ANTICIPATION, new State("Cowardice"));
            MoodStates.Add(e_EmotionsState.JOY, new State("Guilty"));
            MoodStates.Add(e_EmotionsState.TRUST, new State("Submissive"));
        }
    }
}
