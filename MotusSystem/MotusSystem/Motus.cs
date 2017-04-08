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

        internal FuzzyFSM JoySadnessPair;
        internal FuzzyFSM AnticipationSurprisePair;
        internal FuzzyFSM AngerFearPair;
        internal FuzzyFSM TrustDisgustPair;

        // Sprint 2 TODO: Setup emotions, change emotion based on stimuli, act on the emotion
        public Motus()
        {
            CreatePairs();
        }

        public void CreatePairs()
        {
            JoySadnessPair = new FuzzyFSM(e_EmotionsState.JOY, e_EmotionsState.CONTENT, e_EmotionsState.SADNESS);
            AnticipationSurprisePair = new FuzzyFSM(e_EmotionsState.ANTICIPATION, e_EmotionsState.RELAX, e_EmotionsState.SURPRISE);
            AngerFearPair = new FuzzyFSM(e_EmotionsState.ANGER, e_EmotionsState.CALM, e_EmotionsState.FEAR);
            TrustDisgustPair = new FuzzyFSM(e_EmotionsState.TRUST, e_EmotionsState.DISTANT, e_EmotionsState.DISGUST);
        }

        public string[] GetCurrentEmotionStates()
        {
            string[] l_EmotionStates = new string[4];
            l_EmotionStates[0] = JoySadnessPair.CurrentEmotionalState.ToString();
            l_EmotionStates[1] = AnticipationSurprisePair.CurrentEmotionalState.ToString();
            l_EmotionStates[2] = AngerFearPair.CurrentEmotionalState.ToString();
            l_EmotionStates[3] = TrustDisgustPair.CurrentEmotionalState.ToString();

            return l_EmotionStates;

        }

        public void CreateSensation(e_EmotionsState p_EmotionTarget)
        {
            Sensation l_NewSensation;
            switch(p_EmotionTarget)
            {
                case e_EmotionsState.JOY:
                    l_NewSensation = new Sensation(0.3f);
                    JoySadnessPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.SADNESS:
                    l_NewSensation = new Sensation(-0.3f);
                    JoySadnessPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.ANTICIPATION:
                    l_NewSensation = new Sensation(0.3f);
                    AnticipationSurprisePair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.SURPRISE:
                    l_NewSensation = new Sensation(-0.3f);
                    AnticipationSurprisePair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.ANGER:
                    l_NewSensation = new Sensation(0.3f);
                    AngerFearPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.FEAR:
                    l_NewSensation = new Sensation(-0.3f);
                    AngerFearPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.TRUST:
                    l_NewSensation = new Sensation(0.3f);
                    TrustDisgustPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.DISGUST:
                    l_NewSensation = new Sensation(-0.3f);
                    TrustDisgustPair.ReceiveSensation(l_NewSensation);
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
