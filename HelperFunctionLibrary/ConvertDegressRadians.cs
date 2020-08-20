using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperFunctionLibrary
{
    public class ConvertDegressRadians
    {
        /// <summary>
        /// Helper function to convert degrees to radians
        /// </summary>
        /// <param name="deg"></param>
        /// <returns>The Radians of the parameter deg</returns>
        public double DegToRad(double deg)
        {
            return (deg * Math.PI / 180);
        }

        // Helper function to convert radians to degrees
        /// <summary>
        /// Helper function to convert radians to degrees
        /// </summary>
        /// <param name="rad"></param>
        /// <returns>The Degrees of the parameter rad</returns>
        public double RadToDeg(double rad)
        {
            return (rad * 180 / Math.PI);
        }
    }
}
