using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Emotions
{
    /// <summary>
    /// Base class for all individual emotions, 
    /// will contain a state machine (created in each emotion) that decides the actions of the AI
    /// </summary>
    public class Emotion
    {
        public float Strength;
        public e_EmotionsState EmotionID;

        public Emotion(e_EmotionsState p_EmotionName, float p_StartStrength = 0.0f)
        {
            EmotionID = p_EmotionName;
            Strength = p_StartStrength;
        }
    }
}
