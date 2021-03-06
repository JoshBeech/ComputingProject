﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Emotions
{
    /// <summary>
    /// Fuzzy finite-state machine.
    /// Contains 3 emotions and blends between them using 1 - -1 value
    /// </summary>
    internal class FuzzyEmotion
    {

        internal enum e_State { NEGATIVE = -1, NEUTRAL = 0, POSITIVE };

        public float Value = 0;
        internal float PositiveBoundary = 0.2f;
        internal float NegativeBoundary = -0.2f;

        public e_EmotionsState CurrentEmotionalState;
        public e_State CurrentState = e_State.NEUTRAL;

        internal Emotion PositiveExtreme;
        internal Emotion NeutralEmotion;
        internal Emotion NegativeExtreme;

        public FuzzyEmotion(e_EmotionsState p_PositiveExtreme, e_EmotionsState p_NeutralEmotion, e_EmotionsState p_NegativeExtreme)
        {
            PositiveExtreme = new Emotion(p_PositiveExtreme);
            NeutralEmotion = new Emotion(p_NeutralEmotion);
            NegativeExtreme = new Emotion(p_NegativeExtreme);

            CurrentEmotionalState = p_NeutralEmotion;
        }

        public float GetCurrentEmotionStrength()
        {
            switch (CurrentState)
            {
                case e_State.POSITIVE:
                    return PositiveExtreme.Strength;
                case e_State.NEGATIVE:
                    return NegativeExtreme.Strength;
                default:
                    return NeutralEmotion.Strength;
            }
        }

        public void ReceiveSensation(Sensation p_Sensation)
        {
            Value += p_Sensation.Strength;
            Value = Utilities.MathUtilities.Clamp(Value, 1.0f, -1.0f);
            SetState();
        }

        /// <summary>
        /// Sets the current state and emotion of FuzzyEmotion.
        /// Sets emotion strength based of percentage over the emotion threshold
        /// </summary>
        public void SetState()
        {
            if (Value >= PositiveBoundary)
            {
                CurrentState = e_State.POSITIVE;
                CurrentEmotionalState = PositiveExtreme.GetEmotionID();
                float l_PositiveBoundaryDifference = 1.0f - PositiveBoundary;                
                PositiveExtreme.Strength = (Value - PositiveBoundary) / l_PositiveBoundaryDifference;
            }
            else if (Value <= NegativeBoundary)
            {
                CurrentState = e_State.NEGATIVE;
                CurrentEmotionalState = NegativeExtreme.GetEmotionID();
                float l_NegativeBoundaryDifference = 1.0f + NegativeBoundary;
                NegativeExtreme.Strength = Math.Abs((Value - NegativeBoundary) / l_NegativeBoundaryDifference);
            }
            else
            {
                // Neutral boundaries are between the threshoolds where a fuzzy value of 0 is the strongest emotion
                // Since boundaries are mirrored the calcuation double the fuzzy value and subracts from the differnce between thresholds
                CurrentState = e_State.NEUTRAL;
                CurrentEmotionalState = NeutralEmotion.GetEmotionID();
                float l_NeutralBoundaryDifference = PositiveBoundary - NegativeBoundary;
                NeutralEmotion.Strength = (l_NeutralBoundaryDifference - (Math.Abs(Value)*2) )/ l_NeutralBoundaryDifference;
            }
        }


    }
}
