using System;
using System.Collections;
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
        internal float MoodStrength = 0.0f;
        internal e_EmotionsState SecondaryMoodID;
        internal State CurrentState;
        
        /// <summary>
        /// Compare value of other emotions to strength of the mood
        /// If highest is within a threshold use to plan to another state
        /// Otherwise state will be whatever the mood is
        /// </summary>
        /// <param name="p_RemainingEmotions"></param>
        public void BlendMood(List<FuzzyFSM> p_RemainingEmotions)
        {
            if (p_RemainingEmotions.Count == 1)
            {
                SecondaryMoodID = MoodID;
            }
            else
            {
                float l_NewMoodDifference = MoodStrength - p_RemainingEmotions[1].GetCurrentEmotionStrength();
                if (l_NewMoodDifference <= 0.5f)
                {
                    SecondaryMoodID = p_RemainingEmotions[1].CurrentEmotionalState;
                }
                else
                {
                    SecondaryMoodID = MoodID;
                }
            }

            SetBlendedMood();
        }

        protected virtual void SetBlendedMood() {}

        protected virtual void ChangeState(State p_TargetState)
        {
            if (p_TargetState.StateID == CurrentState.StateID)
                return;

            CurrentState.PerformAction("Exit");
            CurrentState = p_TargetState;
            CurrentState.PerformAction("Entry");
        }
    }
}
