using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Emotions;

namespace MotusSystem.Moods
{
    /// <summary>
    /// Used to set the current mood based off emotion values from the Fuzzy state machines
    /// </summary>
    internal class MoodController
    {
        private Mood CurrentMood = new Mood();
        private State CurrentState = new State();
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
            m_Moods.Add(e_EmotionsState.NEUTRAL, new Neutral());

            CurrentMood = m_Moods[e_EmotionsState.NEUTRAL];
            CurrentState = CurrentMood.CurrentMoodState;
        }

        public Mood GetCurrentMood()
        {
            return CurrentMood;
        }

        public State GetCurrentState()
        {
            return CurrentState;
        }

        public Mood GetMood(e_EmotionsState p_Emotion)
        {
            return m_Moods[p_Emotion];
        }

        /// <summary>
        /// Updates the current mood based of the fuzzy emotions.
        /// Selects the strongest non-neutral emotion as primary mood,
        /// passes remaining emotions to primary mood to blend
        /// </summary>
        /// <param name="p_FuzzyEmotions"></param>
        public void UpdateCurrentMood(List<FuzzyEmotion> p_FuzzyEmotions)
        {
            List<FuzzyEmotion> SortedEmotions = new List<FuzzyEmotion>();

            foreach (FuzzyEmotion l_FuzzyEmotion in p_FuzzyEmotions)
            {
                if (l_FuzzyEmotion.CurrentState != FuzzyEmotion.e_State.NEUTRAL)
                {
                    SortedEmotions.Add(l_FuzzyEmotion);
                }
            }
            // Set highest vaule to mood            

            if(SortedEmotions.Count == 0)
            {
                // Set to neutral
                CurrentMood = m_Moods[e_EmotionsState.NEUTRAL];
                CurrentMood.BlendMood(SortedEmotions, ref CurrentState);
                CurrentMood.MoodStrength = 1.0f;
            }
            else 
            {
                SortedEmotions.Sort((x, y) => y.GetCurrentEmotionStrength().CompareTo(x.GetCurrentEmotionStrength()));
                CurrentMood = m_Moods[SortedEmotions[0].CurrentEmotionalState];
                CurrentMood.MoodStrength = SortedEmotions[0].GetCurrentEmotionStrength();
                // Alter mood based off 2nd highest vaule depending on vaules
                CurrentMood.BlendMood(SortedEmotions, ref CurrentState);
            }

        }
    }
}
