using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem
{
    /// <summary>
    /// Base class for the fuzzy finite-state machines used by the emotion pairs
    /// </summary>
    public class FuzzyFSM
    {
        public float Value = 0;
        public e_EmotionsState CurrentState;
    }
}
