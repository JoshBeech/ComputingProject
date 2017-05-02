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
        TRUST, DISTANT, DISGUST,
        NEUTRAL
    };

    public class Motus
    {
        internal List<FuzzyFSM> FuzzyEmotions = new List<FuzzyFSM>();

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

        private void CreatePairs()
        {
            JoySadnessPair = new FuzzyFSM(e_EmotionsState.JOY, e_EmotionsState.CONTENT, e_EmotionsState.SADNESS);
            AnticipationSurprisePair = new FuzzyFSM(e_EmotionsState.ANTICIPATION, e_EmotionsState.RELAX, e_EmotionsState.SURPRISE);
            AngerFearPair = new FuzzyFSM(e_EmotionsState.ANGER, e_EmotionsState.CALM, e_EmotionsState.FEAR);
            TrustDisgustPair = new FuzzyFSM(e_EmotionsState.TRUST, e_EmotionsState.DISTANT, e_EmotionsState.DISGUST);

            FuzzyEmotions.Add(JoySadnessPair);
            FuzzyEmotions.Add(AnticipationSurprisePair);
            FuzzyEmotions.Add(AngerFearPair);
            FuzzyEmotions.Add(TrustDisgustPair);
        }

        public string[] GetCurrentMood()
        {
            string[] l_Mood = new string[3];
            l_Mood[0] = MoodManager.GetCurrentMood().MoodID.ToString();
            l_Mood[1] = MoodManager.GetCurrentMood().SecondaryMoodID.ToString();
            l_Mood[2] = MoodManager.GetCurrentState().StateName;

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

        public float[] GetCurrentEmotionValues()
        {
            float[] l_EmotionValues = new float[4];
            l_EmotionValues[0] = JoySadnessPair.GetCurrentEmotionStrength();
            l_EmotionValues[1] = AnticipationSurprisePair.GetCurrentEmotionStrength();
            l_EmotionValues[2] = AngerFearPair.GetCurrentEmotionStrength();
            l_EmotionValues[3] = TrustDisgustPair.GetCurrentEmotionStrength();

            return l_EmotionValues;
        }

        public void CreateSensation(e_EmotionsState p_EmotionTarget, float p_Strength)
        {
            Sensation l_NewSensation;

            if(p_EmotionTarget == e_EmotionsState.SADNESS || p_EmotionTarget == e_EmotionsState.SURPRISE
                || p_EmotionTarget == e_EmotionsState.FEAR || p_EmotionTarget == e_EmotionsState.DISGUST)
            {
                l_NewSensation = new Sensation(-p_Strength);
            }
            else
                l_NewSensation = new Sensation(p_Strength);
            
            switch (p_EmotionTarget)
            {
                case e_EmotionsState.JOY:
                    JoySadnessPair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(JoySadnessPair);
                    break;
                case e_EmotionsState.SADNESS:
                    JoySadnessPair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(JoySadnessPair);
                    break;
                case e_EmotionsState.ANTICIPATION:
                    AnticipationSurprisePair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(AnticipationSurprisePair);
                    break;
                case e_EmotionsState.SURPRISE:
                    AnticipationSurprisePair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(AnticipationSurprisePair);
                    break;
                case e_EmotionsState.ANGER:
                    AngerFearPair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(AngerFearPair);
                    break;
                case e_EmotionsState.FEAR:
                    AngerFearPair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(AngerFearPair);
                    break;
                case e_EmotionsState.TRUST:
                    TrustDisgustPair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(AngerFearPair);
                    break;
                case e_EmotionsState.DISGUST:
                    TrustDisgustPair.ReceiveSensation(l_NewSensation);
                    DeteriorateEmotions(TrustDisgustPair);
                    break;
                default:
                    DeteriorateEmotions();
                    break;
            }

            MoodManager.UpdateCurrentMood(FuzzyEmotions);
        }

        /// <summary>
        /// Adjust emotion values slightly, 
        /// used after receiving a sensation to highlight changes 
        /// </summary>
        /// <param name="p_Emotions"></param>
        private void DeteriorateEmotions(FuzzyFSM p_AlteredEmotion = null)
        {
            foreach(FuzzyFSM l_Emotion in FuzzyEmotions)
            {
                if (l_Emotion != p_AlteredEmotion)
                {
                    if (l_Emotion.Value > 0)
                    {
                        l_Emotion.Value -= 0.1f;
                    }
                    else if (l_Emotion.Value < 0)
                    {
                        l_Emotion.Value += 0.1f;
                    }
                    l_Emotion.Value = Utilities.MathUtilities.Clamp(l_Emotion.Value, 1.0f, -1.0f); 
                    l_Emotion.SetState();
                }
            }
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


        // TODO: Overload to take just one emotion allowing to give same action to all states of one mood
        public void SetAction(e_EmotionsState p_PrimaryEmotion, e_EmotionsState p_SecondaryEmotion, string p_FunctionKey, Action p_Function)
        {
            //Find the target state using the primary and secondary emotions
            Mood l_TargetMood = MoodManager.GetMood(p_PrimaryEmotion);
            State l_TargetState = l_TargetMood.MoodStates[p_SecondaryEmotion];

            l_TargetState.AddAction(p_FunctionKey, p_Function);
        }
    }
}
