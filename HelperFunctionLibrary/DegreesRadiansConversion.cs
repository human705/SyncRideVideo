using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperFunctionLibrary
{
    public class DegreesRadiansConversion
    {
        public double Degrees { get; set; }
        public double Radians { get; set; }

        public DegreesRadiansConversion(double deg, double rad)
        {
            Degrees = deg;
            Radians = rad;
        }

        // Helper function to convert degrees to radians
        public double DegToRad(double deg)
        {
            return (deg * Math.PI / 180);
        }

        // Helper function to convert radians to degrees
        public double RadToDeg(double rad)
        {
            return (rad * 180 / Math.PI);
        }

    }
}
