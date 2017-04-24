using System;
using System.Collections;
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
        private Mood CurrentMood = new Mood();
        private Dictionary<e_EmotionsState, Mood> m_Moods = new Dictionary<e_EmotionsState, Mood>();
        internal List<FuzzyFSM> SortedEmotions = new List<FuzzyFSM>();

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
            m_Moods.Add(e_EmotionsState.NEUTRAL, new Neutral());

            CurrentMood = m_Moods[e_EmotionsState.NEUTRAL];
        }

        public Mood GetCurrentMood()
        {
            return CurrentMood;
        }

        // Takes/gathers input from all FFSMs - better name?
        public void UpdateCurrentMood(List<FuzzyFSM> p_FuzzyEmotions)
        {
            // Compare the values of all FFSM - turn into lambda and pass to mood to reuse?
            //FuzzyFSM l_StrongestEmotion = new FuzzyFSM();
            //float l_StrongestEmotionValue = 0.0f;            
            //int l_NeutralEmotionCount = 0;

            // Check all emotions that aren't neutral and compare which has the strongest value
            // and set the current mood to be the strongest emotion

            SortedEmotions.Clear();

            foreach (FuzzyFSM l_FuzzyEmotion in p_FuzzyEmotions)
            {
                if (l_FuzzyEmotion.CurrentState != FuzzyFSM.e_State.NEUTRAL)
                {

                    SortedEmotions.Add(l_FuzzyEmotion);
                    //if (l_FuzzyEmotion.GetCurrentEmotionStrength() > l_StrongestEmotionValue)
                    //{
                    //    l_StrongestEmotion = l_FuzzyEmotion;
                    //    l_StrongestEmotionValue = l_FuzzyEmotion.GetCurrentEmotionStrength();
                    //}
                }
            }
            // Set highest vaule to mood

            

            if(SortedEmotions.Count == 0)
            {
                // Set to neutral
                CurrentMood = m_Moods[e_EmotionsState.NEUTRAL];
                CurrentMood.MoodStrength = 1.0f;
            }
            else 
            {
                SortedEmotions.Sort((x, y) => y.GetCurrentEmotionStrength().CompareTo(x.GetCurrentEmotionStrength()));
                CurrentMood = m_Moods[SortedEmotions[0].CurrentEmotionalState];
                CurrentMood.MoodStrength =SortedEmotions[0].GetCurrentEmotionStrength();
                // Alter mood based off 2nd highest vaule depending on vaules
                CurrentMood.BlendMood(SortedEmotions);
            }

        }
    }
}
