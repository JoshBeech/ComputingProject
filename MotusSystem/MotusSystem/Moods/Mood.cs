﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Emotions;

namespace MotusSystem.Moods
{
    /// <summary>
    /// Base class for all moods.
    /// Has all shared mood functionality:
    /// Blending primary and secondary emotions
    /// Setting new secondary emotion if required and blended strength
    /// Calling enter and exit functionality for states.
    /// </summary>
    internal class Mood
    {
        internal e_EmotionsState MoodID;
        internal e_EmotionsState SecondaryMoodID;
        internal float MoodStrength = 0.0f;
        internal float BlendedStrength = 0.0f;

        internal Dictionary<e_EmotionsState, State> MoodStates = new Dictionary<e_EmotionsState, State>();
        internal State CurrentMoodState;
        
        /// <summary>
        /// Compare value of other emotions to strength of the mood
        /// If highest is within a threshold use to plan to another state
        /// Otherwise state will be whatever the mood is.
        /// </summary>
        /// <param name="p_RemainingEmotions"></param>
        /// <param name="p_CurrentAgentState">Reference to current mood state, 
        /// required to perform accurate comparision between new/current mood state and state of mood controller</param>
        public void BlendMood(List<FuzzyEmotion> p_RemainingEmotions, ref State p_CurrentAgentState)
        {
            if (p_RemainingEmotions.Count <= 1)
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

            SetBlendedMood(ref p_CurrentAgentState);

            if (SecondaryMoodID != MoodID)
            {
                BlendedStrength = (MoodStrength + p_RemainingEmotions[1].GetCurrentEmotionStrength()) / 2;
            }
            else
                BlendedStrength = MoodStrength;
        }

        protected void SetBlendedMood(ref State p_CurrentAgentState)
        {
            State l_State = new State();
            if (MoodStates.TryGetValue(SecondaryMoodID, out l_State))
            {
                ChangeState(l_State, ref p_CurrentAgentState);
            }
            else
                ChangeState(MoodStates[MoodID], ref p_CurrentAgentState);
        }

        protected void ChangeState(State p_TargetState, ref State p_CurrentAgentState)
        {
            if(p_CurrentAgentState != null && p_TargetState != null && p_TargetState.StateName == p_CurrentAgentState.StateName)
            {
                return;
            }

            p_CurrentAgentState.PerformAction("Exit");
            CurrentMoodState = p_TargetState;
            CurrentMoodState.PerformAction("Entry");
            p_CurrentAgentState = CurrentMoodState;
        }
    }
}
