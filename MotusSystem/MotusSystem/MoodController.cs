using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem
{
    /// <summary>
    /// Used to set the current mood based off emotion values from the Fuzzy state machines
    /// </summary>
    internal class MoodController
    {
        private Dictionary<e_EmotionsState, Mood> m_Moods = new Dictionary<e_EmotionsState, Mood>(); 

        public MoodController()
        {
            m_Moods.Add(e_EmotionsState.JOY, new Mood());
            m_Moods.Add(e_EmotionsState.ANTICIPATION, new Mood());
            m_Moods.Add(e_EmotionsState.ANGER, new Mood());
            m_Moods.Add(e_EmotionsState.TRUST, new Mood());
            m_Moods.Add(e_EmotionsState.SADNESS, new Mood());
            m_Moods.Add(e_EmotionsState.SURPRISE, new Mood());
            m_Moods.Add(e_EmotionsState.FEAR, new Mood());
            m_Moods.Add(e_EmotionsState.DISGUST, new Mood());
        }

        public void GetCurrentMood()
        {
            // new enum?
        }

        // Takes/gathers input from all FFSMs - better name?
        public void Update()
        {

        }

        private void ChangeMood()
        {

        }


    }
}
