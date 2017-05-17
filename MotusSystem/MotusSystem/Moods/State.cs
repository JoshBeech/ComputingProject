using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Moods
{
    /// <summary>
    /// Contains functionality from the game and is stored in moods.
    /// Functionality can be called at any point using PerformAction
    /// </summary>
    internal class State
    {
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


        /// <summary>
        /// Searchs StateAction dictionary to find Actionkey.
        /// If successful performs action
        /// </summary>
        /// <param name="p_ActionKey"></param>
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
