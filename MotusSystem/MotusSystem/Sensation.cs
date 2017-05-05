using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotusSystem.Utilities;

namespace MotusSystem
{
    /// <summary>
    /// Triggered by stimuli from the game world to alter AI states
    /// </summary>
    public class Sensation
    {
        public float Strength = 0;

        public Sensation(float p_Strength)
        {
            Strength = p_Strength;
        }
    }
}
