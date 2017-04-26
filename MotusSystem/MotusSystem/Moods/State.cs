using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    //internal enum e_MoodStates
    //{
    //    JOYFUL, PRIDE, DELIGHT, COURAGE, LOVE,
    //    RESIGNATION, PESSIMISTIC, SAD, DISPAIR, MISERY, DISAPPOINTMENT,
    //    AWAITING, OPTIMISTIC, STUBBORN, CAUTIOUS, CYNICISM,
    //    SURPRISED, SHOCKED, EMBARRASSED, ALARMED,
    //    ANGRY, ENVIOUS, AGRESSIVE, OUTRAGED, DOMINANT, HATEFUL,
    //    FEARFUL, GUILTY, COWARDESS, SUBMISSIVE,
    //    TRUSTING, FRIENDLY, FATALISTIC, 
    //    DISGUSTED, REMORSEFUL, MORBID, MORTIFIED,
    //    NEUTRAL
    //};

    internal class State
    {
        //internal e_MoodStates StateID;
        internal string StateName = "";
        private Dictionary<string, Action> StateActions = new Dictionary<string, Action>(); 

        internal State(string p_Name = "")
        {
            StateName = p_Name;
        }

        internal void AddAction(string p_Actionkey, Action p_Action)
        {
            StateActions.Add(p_Actionkey, p_Action);
        }

        internal void PerformAction(string p_ActionKey)
        {
            Action l_Action;

            if(StateActions.TryGetValue(p_ActionKey, out l_Action))
            {
                l_Action();
            }
        }
    }
}
