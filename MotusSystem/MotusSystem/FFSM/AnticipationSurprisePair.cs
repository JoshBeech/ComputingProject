using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Emotions;

namespace MotusSystem.FFSM
{
    internal class AnticipationSurprisePair : FuzzyFSM
    {
        public AnticipationSurprisePair()
        {
            PositiveExtreme = new Emotion(e_EmotionsState.ANTICIPATION);
            NeutralEmotion = new Emotion(e_EmotionsState.RELAX);
            NegativeExtreme = new Emotion(e_EmotionsState.SURPRISE);

            CurrentEmotionalState = e_EmotionsState.PEACEFUL;
        }
    }
}
