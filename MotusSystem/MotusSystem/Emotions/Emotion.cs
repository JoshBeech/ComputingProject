using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Emotions
{
    /// <summary>
    /// Reflects the value of an emotion based upon the ID set on construction.
    /// Used in combination with fuzzy emotion to create the emotion pairs
    /// </summary>
    public class Emotion
    {
        public float Strength;
        private e_EmotionsState EmotionID;

        public Emotion(e_EmotionsState p_EmotionName, float p_StartStrength = 0.0f)
        {
            EmotionID = p_EmotionName;
            Strength = p_StartStrength;
        }

        public e_EmotionsState GetEmotionID()
        {
            return EmotionID;
        }
    }
}
