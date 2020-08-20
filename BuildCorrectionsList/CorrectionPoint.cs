using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildCorrectionsList
{
    class CorrectionPoint
    {
        public int FileTimeInSecs { get; set; } = -1;
        public int VideoTimeInSecs { get; set; } = -1;
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
        public double DistanceFromStart { get; set; } = 0.0;
        public double DistanceFromPrevious { get; set; } = 0.0;
    }
}
