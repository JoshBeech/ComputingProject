﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    internal class Trust : Mood
    {
        private State Trusting = new State(e_MoodStates.TRUSTING);
        private State Friendly = new State(e_MoodStates.FRIENDLY);
        private State Fatalistic = new State(e_MoodStates.FATALISTIC);

        public Trust()
        {
            MoodID = e_EmotionsState.TRUST;
        }

        protected override void SetBlendedMood()
        {
            switch(SecondaryEmotion)
            {
                case e_EmotionsState.ANTICIPATION:
                    CurrentState = Fatalistic;
                    break;
                case e_EmotionsState.JOY:
                    CurrentState = Friendly;
                    break;
                default:
                    CurrentState = Trusting;
                    break;
            }
        }
    }
}