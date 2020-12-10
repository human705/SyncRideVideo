using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class BuildAddRideSegment
    {
        public int mVideoTime { get; set; }
        public int mStartMarkerTime { get; set; }
        public int mEndMarkerTime { get; set; }
        public GoldenCheetahRide mOldRide { get; set; }
        public List<SAMPLE> mNewSegment { get; set; }
        public List<SAMPLE> mOldSegment { get; set; }

    }
}
