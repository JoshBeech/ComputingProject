using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Emotions;

namespace MotusSystem.FFSM
{
    internal class TrustDisgustPair : FuzzyFSM
    {
        public TrustDisgustPair()
        {
            PositiveExtreme = new Emotion(e_EmotionsState.TRUST);
            NeutralEmotion = new Emotion(e_EmotionsState.DISTANT);
            NegativeExtreme = new Emotion(e_EmotionsState.DISGUST);

            CurrentEmotionalState = e_EmotionsState.DISTANT;
        }
    }
}
