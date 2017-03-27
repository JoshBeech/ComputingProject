using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotusSystem.Utilities
{
    class MathUtilities
    {
        public static float Clamp(float p_FloatToClamp, float p_FloatMax, float p_FloatMin)
        {
            if (p_FloatToClamp > p_FloatMax)
                p_FloatToClamp = p_FloatMax;
            else if (p_FloatToClamp < p_FloatMin)
                p_FloatToClamp = p_FloatMin;

            return p_FloatToClamp;               
        }
    }
}
