using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.FFSM;

namespace MotusSystem.Moods
{
    /// <summary>
    /// Used to set the current mood based off emotion values from the Fuzzy state machines
    /// </summary>
    internal class MoodController
    {
        internal Mood CurrentMood = new Mood();
        private Dictionary<e_EmotionsState, Mood> m_Moods = new Dictionary<e_EmotionsState, Mood>(); 

        public MoodController()
        {
            m_Moods.Add(e_EmotionsState.JOY, new Joy());
            m_Moods.Add(e_EmotionsState.ANTICIPATION, new Anticipation());
            m_Moods.Add(e_EmotionsState.ANGER, new Anger());
            m_Moods.Add(e_EmotionsState.TRUST, new Trust());
            m_Moods.Add(e_EmotionsState.SADNESS, new Sadness());
            m_Moods.Add(e_EmotionsState.SURPRISE, new Surprise());
            m_Moods.Add(e_EmotionsState.FEAR, new Fear());
            m_Moods.Add(e_EmotionsState.DISGUST, new Disgust());
        }

        public e_EmotionsState GetCurrentMood()
        {
            return CurrentMood.MoodID;
        }

        // Takes/gathers input from all FFSMs - better name?
        public void UpdateCurrentMood(List<FuzzyFSM> p_FuzzyEmotions)
        {
            // Compare the values of all FFSM - turn into lambda and pass to mood to reuse
            FuzzyFSM l_StrongestEmotion = new FuzzyFSM();
            float l_StrongestEmotionValue = 0.0f;
            foreach(FuzzyFSM l_FuzzyEmotion in p_FuzzyEmotions)
            {
                if (l_FuzzyEmotion.CurrentState != FuzzyFSM.e_State.NEUTRAL)
                {
                    if (l_FuzzyEmotion.GetCurrentEmotionStrength() > l_StrongestEmotionValue)
                    {
                        l_StrongestEmotion = l_FuzzyEmotion;
                        l_StrongestEmotionValue = l_FuzzyEmotion.GetCurrentEmotionStrength();
                    }
                }
            }
            // Set highest vaule to mood
            CurrentMood = m_Moods[l_StrongestEmotion.CurrentEmotionalState];
            p_FuzzyEmotions.Remove(l_StrongestEmotion);
            // Alter mood based off 2nd highest vaule depending on vaules
            CurrentMood.BlendMood(p_FuzzyEmotions);
        }
    }
}
