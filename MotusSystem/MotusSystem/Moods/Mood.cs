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
        //internal FuzzyFSM PrimaryEmotion;
        //internal FuzzyFSM SecondaryEmotion;
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
                //float l_MoodDifference = 1.0f;
                //bool l_SecondaryEmotionSet = false;


                float l_NewMoodDifference = MoodStrength - p_RemainingEmotions[1].GetCurrentEmotionStrength();
                if (l_NewMoodDifference <= 0.5f)
                {
                    SecondaryMoodID = p_RemainingEmotions[1].CurrentEmotionalState;
                }
                else
                {
                    SecondaryMoodID = MoodID;
                }

                //foreach (FuzzyFSM l_FuzzyEmotion in p_RemainingEmotions)
                //{
                //    // If the current emotion is not in a neutral state or equal to the current mood proceed with calculation
                //    if (l_FuzzyEmotion.CurrentState != FuzzyFSM.e_State.NEUTRAL || l_FuzzyEmotion.CurrentEmotionalState != MoodID)
                //    {
                //        float l_NewMoodDifference = MoodStrength - l_FuzzyEmotion.GetCurrentEmotionStrength();
                        
                //        // Criteria for secondary emotion:
                //        // Must have a value less than 0.5f from current mood strength
                //        // Must have a smaller value than current mood strength 
                //        if (l_NewMoodDifference < 0.5f && l_NewMoodDifference < l_MoodDifference)
                //        {
                //            SecondaryMoodID = l_FuzzyEmotion.CurrentEmotionalState;
                //            l_MoodDifference = l_NewMoodDifference;                        
                //            l_SecondaryEmotionSet = true;
                //        }
                //    }
                //}

                //// If no secondary emotion has been set, set secondary to primary
                //if (l_SecondaryEmotionSet == false)
                //{
                //    SecondaryMoodID = MoodID;
                //}
            }

            SetBlendedMood();
        }

        protected virtual void SetBlendedMood() {}
    }
}
