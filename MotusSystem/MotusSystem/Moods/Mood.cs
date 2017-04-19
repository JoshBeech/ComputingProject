using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.FFSM;

namespace MotusSystem.Moods
{
    /// <summary>
    /// Collection of states - combinations of emotions 
    /// </summary>
    internal class Mood
    {
        internal e_EmotionsState MoodID;
        internal float MoodStrength;
        internal e_EmotionsState SecondaryEmotion;
        internal State CurrentState;

        /// <summary>
        /// Compare value of other emotions to strength of the mood
        /// If highest is within a threshold use to plan to another state
        /// Otherwise state will be whatever the mood is
        /// </summary>
        /// <param name="p_RemainingEmotions"></param>
        public void BlendMood(List<FuzzyFSM> p_RemainingEmotions)
        {
            float l_MoodDifference = 1.0f;
            foreach (FuzzyFSM l_FuzzyEmotion in p_RemainingEmotions)
            {
                if (l_FuzzyEmotion.CurrentState != FuzzyFSM.e_State.NEUTRAL)
                {
                    float l_NewMoodDifference = MoodStrength - l_FuzzyEmotion.GetCurrentEmotionStrength();

                    if (l_NewMoodDifference < 0.5f && l_NewMoodDifference < l_MoodDifference)
                    {
                        SecondaryEmotion = l_FuzzyEmotion.CurrentEmotionalState;
                        l_MoodDifference = l_NewMoodDifference;
                        MoodStrength = l_FuzzyEmotion.GetCurrentEmotionStrength();
                    }
                }
            }

            SetBlendedMood();
        }

        protected virtual void SetBlendedMood()
        {

        }
    }
}
