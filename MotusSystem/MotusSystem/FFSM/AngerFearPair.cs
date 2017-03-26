using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Emotions;

namespace MotusSystem.FFSM
{
    internal class AngerFearPair : FuzzyFSM
    {   
        public AngerFearPair()
        {
            PositiveExtreme = new Emotion(e_EmotionsState.ANGER);
            NeutralEmotion = new Emotion(e_EmotionsState.CALM);
            NegativeExtreme = new Emotion(e_EmotionsState.FEAR);

            CurrentEmotionalState = e_EmotionsState.CALM;
        }
    }
}
