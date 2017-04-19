using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.FFSM;
using MotusSystem.Moods;

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

        internal MoodController MoodManager;

        public Motus()
        {
            CreatePairs();
            MoodManager = new MoodController();
        }

        public void CreatePairs()
        {
            JoySadnessPair = new FuzzyFSM(e_EmotionsState.JOY, e_EmotionsState.CONTENT, e_EmotionsState.SADNESS);
            AnticipationSurprisePair = new FuzzyFSM(e_EmotionsState.ANTICIPATION, e_EmotionsState.RELAX, e_EmotionsState.SURPRISE);
            AngerFearPair = new FuzzyFSM(e_EmotionsState.ANGER, e_EmotionsState.CALM, e_EmotionsState.FEAR);
            TrustDisgustPair = new FuzzyFSM(e_EmotionsState.TRUST, e_EmotionsState.DISTANT, e_EmotionsState.DISGUST);
        }


        public string[] GetCurrentMood()
        {
            string[] l_Mood = new string[3];
            l_Mood[0] = MoodManager.CurrentMood.MoodID.ToString();
            l_Mood[1] = MoodManager.CurrentMood.SecondaryEmotion.ToString();
            l_Mood[2] = MoodManager.CurrentMood.CurrentState.StateID.ToString();

            return l_Mood;
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

        public void CreateSensation(e_EmotionsState p_EmotionTarget, float p_Strength)
        {
            Sensation l_NewSensation =  new Sensation(p_Strength);
            switch(p_EmotionTarget)
            {
                case e_EmotionsState.JOY:
                    JoySadnessPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.SADNESS:
                    JoySadnessPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.ANTICIPATION:
                    AnticipationSurprisePair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.SURPRISE:
                    AnticipationSurprisePair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.ANGER:
                    AngerFearPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.FEAR:
                    AngerFearPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.TRUST:
                    TrustDisgustPair.ReceiveSensation(l_NewSensation);
                    break;
                case e_EmotionsState.DISGUST:
                    TrustDisgustPair.ReceiveSensation(l_NewSensation);
                    break;
                default:
                    break;
            }

            MoodManager.UpdateCurrentMood(new List<FuzzyFSM> { JoySadnessPair, AnticipationSurprisePair, AngerFearPair, TrustDisgustPair });
        }

        public void SetMood(e_EmotionsState p_MoodEmotion)
        {
            switch (p_MoodEmotion)
            {
                case e_EmotionsState.JOY:
                    JoySadnessPair.Value = 1.0f;
                    JoySadnessPair.SetState();
                    break;
                case e_EmotionsState.SADNESS:
                    JoySadnessPair.Value = -1.0f;
                    JoySadnessPair.SetState();
                    break;
                case e_EmotionsState.ANTICIPATION:
                    AnticipationSurprisePair.Value = 1.0f;
                    AnticipationSurprisePair.SetState();
                    break;
                case e_EmotionsState.SURPRISE:
                    AnticipationSurprisePair.Value = -1.0f;
                    AnticipationSurprisePair.SetState();
                    break;
                case e_EmotionsState.ANGER:
                    AngerFearPair.Value = 1.0f;
                    AngerFearPair.SetState();
                    break;
                case e_EmotionsState.FEAR:
                    AngerFearPair.Value = -1.0f;
                    AngerFearPair.SetState();
                    break;
                case e_EmotionsState.TRUST:
                    TrustDisgustPair.Value = 1.0f;
                    TrustDisgustPair.SetState();
                    break;
                case e_EmotionsState.DISGUST:
                    TrustDisgustPair.Value = -1.0f;
                    TrustDisgustPair.SetState();
                    break;
                default:
                    break;
            }

            MoodManager.UpdateCurrentMood(new List<FuzzyFSM> { JoySadnessPair, AnticipationSurprisePair, AngerFearPair, TrustDisgustPair });
        }

        public void SetAction(Action p_Action)
        {
            //do the thing
        }
    }
}
