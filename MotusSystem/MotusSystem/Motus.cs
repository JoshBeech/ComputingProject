using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.FFSM;

namespace MotusSystem
{
    // This class is what will be on NPCs
    // It needs to contain fuzzy emotions, moods, 
    // know about feelings (past events) and mannerims? 
    public enum e_EmotionsState
    {
        JOY = 0, CONTENT, SADNESS,
        ANTICIPATION, RELAX, SURPRISE,
        ANGER, CALM, FEAR,
        TRUST, DISTANT, DISGUST
    };
    public class Motus
    {
        internal JoySadnessPair JSPair;
        internal AnticipationSurprisePair ASPair;
        internal AngerFearPair AFPair;
        internal TrustDisgustPair TDPair;

        // Sprint 2 TODO: Setup emotions, change emotion based on stimuli, act on the emotion
        public Motus()
        {
            CreatePairs();
        }

        public void CreatePairs()
        {
            JSPair = new JoySadnessPair();
            ASPair = new AnticipationSurprisePair();
            AFPair = new AngerFearPair();
            TDPair = new TrustDisgustPair();
        }

        public string[] GetCurrentEmotionStates()
        {
            string[] l_EmotionStates = new string[4];
            l_EmotionStates[0] = JSPair.CurrentEmotionalState.ToString();
            l_EmotionStates[1] = ASPair.CurrentEmotionalState.ToString();
            l_EmotionStates[2] = AFPair.CurrentEmotionalState.ToString();
            l_EmotionStates[3] = TDPair.CurrentEmotionalState.ToString();

            return l_EmotionStates;

        }

        public void CreateSensation(e_EmotionsState p_EmotionTarget)
        {
            Sensation l_NewSensation;
            switch(p_EmotionTarget)
            {
                case e_EmotionsState.JOY:
                    l_NewSensation = new Sensation(0.3f);
                    JSPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.SADNESS:
                    l_NewSensation = new Sensation(-0.3f);
                    JSPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.ANTICIPATION:
                    l_NewSensation = new Sensation(0.3f);
                    ASPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.SURPRISE:
                    l_NewSensation = new Sensation(-0.3f);
                    ASPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.ANGER:
                    l_NewSensation = new Sensation(0.3f);
                    AFPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.FEAR:
                    l_NewSensation = new Sensation(-0.3f);
                    AFPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.TRUST:
                    l_NewSensation = new Sensation(0.3f);
                    TDPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.DISGUST:
                    l_NewSensation = new Sensation(-0.3f);
                    TDPair.ReceiveSensation(l_NewSensation);
                    break;
                default:
                    break;
            }
        }

        public void SetAction(Action p_Action)
        {
           //do the thing
        }
    }
}
