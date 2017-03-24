using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem
{
    // This class is what will be on NPCs
    // It needs to contain fuzzy emotions, moods, 
    // know about feelings (past events) and mannerims? 
    public enum e_EmotionsState
    {
        JOY = 0, CONTEMPT, SADNESS,
        ANTICIPATION, MINDFUL, SURPRISE,
        ANGER, PEACEFUL, FEAR,
        TRUST, INDIFFERENT, DISGUST
    };
    public class Motus
    {
        public JoySadnessPair JSP; 

        // Sprint 2 TODO: Setup emotions, change emotion based on stimuli, act on the emotion
        public Motus()
        {
            CreatePair();
        }

        public void CreatePair()
        {
            JSP = new JoySadnessPair();
        }

        public string GetCurrentEmotionState()
        {
            return JSP.CurrentState.ToString();
        }

        public void CreateSensation(e_EmotionsState p_EmotionTarget)
        {
            switch(p_EmotionTarget)
            {
                case e_EmotionsState.JOY:
                    Sensation l_NewJoySensation = new Sensation(0.3f);
                    JSP.ReceiveSensation(l_NewJoySensation);
                    break;
                case e_EmotionsState.SADNESS:
                    Sensation l_NewSadnessSensation = new Sensation(-0.3f);
                    JSP.ReceiveSensation(l_NewSadnessSensation);
                    break;
                default:
                    break;
            }
        }
    }
}
