using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class GoldenCheetahRide
    {
        public RIDE RIDE { get; set; }
    }

    public class RIDE
    {
        public string STARTTIME { get; set; }
        public int RECINTSECS { get; set; }
        public string DEVICETYPE { get; set; }
        public string IDENTIFIER { get; set; }
        public TAGS TAGS { get; set; }
        public List<INTERVAL> INTERVALS { get; set; }
        public List<SAMPLE> SAMPLES { get; set; }
    }

    public class TAGS
    {
        public string AerobicTISS { get; set; }
        public string AerobicTrainingEffect { get; set; }
        public string AnaerobicTISS { get; set; }
        public string AnaerobicTrainingEffect { get; set; }
        public string Athlete { get; set; }
        public string AverageCadence { get; set; }
        public string AverageHeartRate { get; set; }
        public string AveragePower { get; set; }
        public string AverageSpeed { get; set; }
        public string BikeScore { get; set; }
        public string BikeStress { get; set; }
        public string CP { get; set; }
        public string ChangeHistory { get; set; }
        public string DanielsEqP { get; set; }
        public string DanielsPoints { get; set; }
        public string Data { get; set; }
        public string Device { get; set; }
        public string DeviceInfo { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public string ElevationGain { get; set; }
        public string FileFormat { get; set; }
        public string Filename { get; set; }
        public string GOVSS { get; set; }
        public string Keywords { get; set; }
        public string LTHRdetected { get; set; }
        public string LTSdetected { get; set; }
        public string Month { get; set; }
        public string Notes { get; set; }
        public string Objective { get; set; }
        public string PerformanceCondition { get; set; }
        public string PoolLength { get; set; }
        public string RPE { get; set; }
        public string RecoveryTime { get; set; }
        public string Route { get; set; }
        public string SourceFilename { get; set; }
        public string Sport { get; set; }
        public string SubSport { get; set; }
        public string SwimScore { get; set; }
        public string TimeMoving { get; set; }
        public string VO2maxdetected { get; set; }
        public string Weekday { get; set; }
        public string Weight { get; set; }
        public string Work { get; set; }
        public string WorkoutCode { get; set; }
        public string Year { get; set; }
        public string xPower { get; set; }
    }

    public class INTERVAL
    {
        public string NAME { get; set; }
        public int START { get; set; }
        public int STOP { get; set; }
        public string COLOR { get; set; }
        public string PTEST { get; set; }
    }

    public class SAMPLE
    {
        public int SECS { get; set; }
        public double KM { get; set; }
        public double CAD { get; set; }
        public double KPH { get; set; }
        public double HR { get; set; }
        public double ALT { get; set; }
        public double LAT { get; set; }
        public double LON { get; set; }
        public double SLOPE { get; set; }
    }
}
