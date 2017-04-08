using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Emotions;

namespace MotusSystem.FFSM
{
    /// <summary>
    /// Base class for the fuzzy finite-state machines used by the emotion pairs
    /// </summary>
    internal class FuzzyFSM
    {

        internal enum e_State { NEGATIVE = -1, NEUTRAL = 0, POSITIVE };

        public float Value = 0;
        protected float PositiveBoundary = 0.5f;
        protected float NegativeBoundary = -0.5f;

        public e_EmotionsState CurrentEmotionalState;
        public e_State CurrentState = e_State.NEUTRAL;

        internal Emotion PositiveExtreme;
        internal Emotion NeutralEmotion;
        internal Emotion NegativeExtreme;

        public FuzzyFSM()
        {

        }

        public FuzzyFSM(e_EmotionsState p_PositiveExtreme, e_EmotionsState p_NeutralEmotion, e_EmotionsState p_NegativeExtreme)
        {
            PositiveExtreme = new Emotion(p_PositiveExtreme);
            NeutralEmotion = new Emotion(p_NeutralEmotion);
            NegativeExtreme = new Emotion(p_NeutralEmotion);

            CurrentEmotionalState = p_NeutralEmotion;
        }

        public void ReceiveSensation(Sensation p_Sensation)
        {
            Value += p_Sensation.Strength;
            Value = Utilities.MathUtilities.Clamp(Value, 1.0f, -1.0f);
            SetState();
        }

        public void SetState()
        {
            if (Value >= PositiveBoundary)
            {
                CurrentState = e_State.POSITIVE;
                CurrentEmotionalState = PositiveExtreme.EmotionID;
                float l_PositiveBoundaryDifference = 1.0f - PositiveBoundary;                
                PositiveExtreme.Strength = (Value - PositiveBoundary) / l_PositiveBoundaryDifference;
            }
            else if (Value <= NegativeBoundary)
            {
                CurrentState = e_State.NEGATIVE;
                CurrentEmotionalState = NegativeExtreme.EmotionID;
                float l_NegativeBoundaryDifference = 1.0f + NegativeBoundary;
                NegativeExtreme.Strength = (Value - NegativeBoundary) / l_NegativeBoundaryDifference;
            }
            else
            {
                CurrentState = e_State.NEUTRAL;
                CurrentEmotionalState = NeutralEmotion.EmotionID;
                float l_NeutralBoundaryDifference = PositiveBoundary - NegativeBoundary;
                NeutralEmotion.Strength = (l_NeutralBoundaryDifference - (Math.Abs(Value)*2) )/ l_NeutralBoundaryDifference;

            }
        }

        public float GetCurrentEmotionStrength()
        {
            switch(CurrentState)
            {
                case e_State.POSITIVE:
                    return PositiveExtreme.Strength;
                case e_State.NEGATIVE:
                    return NegativeExtreme.Strength;
                default:
                    return NeutralEmotion.Strength;                    
            }
        }
    }
}
