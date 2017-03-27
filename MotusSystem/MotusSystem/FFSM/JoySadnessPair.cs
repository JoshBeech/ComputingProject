using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Emotions;

namespace MotusSystem.FFSM
{
    internal class JoySadnessPair : FuzzyFSM
    {
        public JoySadnessPair()
        {
            PositiveExtreme = new Emotion(e_EmotionsState.JOY);
            NeutralEmotion = new Emotion(e_EmotionsState.CONTENT);
            NegativeExtreme = new Emotion(e_EmotionsState.SADNESS);

            CurrentEmotionalState = e_EmotionsState.CONTENT;
        }
    }
}
