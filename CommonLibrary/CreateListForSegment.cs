using HelperFunctionLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class CreateListForSegment
    {
        public List<SAMPLE> mSourceList { get; set; }
        public List<SAMPLE> mCreatedList { get; set; } = new List<SAMPLE>();
        public int mStartTimeMarker { get; set; }
        public int mEndTimeMarket { get; set; }
        public int mVideoTime { get; set; }
        public List<SAMPLE> tempList { get; private set; } = new List<SAMPLE>();
        public List<SAMPLE> mDestList { get; private set; } = new List<SAMPLE>();

        private int mSegmentTime { get; set; } = -1;
        private int pointsToAdd = -1;

        //CTOR
        public CreateListForSegment(List<SAMPLE> sourceList, List<SAMPLE> destList, int startTimeMarker, int endTimeMarker, int videoTime)
        {
            this.mSourceList = sourceList ?? throw new ArgumentNullException(nameof(mSourceList));
            this.mStartTimeMarker = startTimeMarker;
            this.mEndTimeMarket = endTimeMarker;
            this.tempList.Clear();
            
            //this.mNumOfItemsToCopy = numOfItemsToCopy;

            this.mVideoTime = videoTime + 1; // Account for the 0 second

            CreateSubListOfSamples();
            //BuildTempList();
            AddPointsToListProcess();

            // Copy contents of temp list to Dest list. -- Check if points exist
            AppendToDestList();
        }



        /// <summary>
        /// Append items from the tempList to the destination list
        /// </summary>
        public void AppendToDestList()
        {
            foreach (SAMPLE sample in tempList)
            {
                //Check is fample exists
                mDestList.Add(sample);
            }
        }

        /// <summary>
        /// Select which add process to use. 
        /// If we need to add more than half of the points we have may we need multiple passes
        /// Otherwise we need to calculate the interval to add points
        /// </summary>
        private void AddPointsToListProcess()
        {
            pointsToAdd = mVideoTime - mSegmentTime;
            if (pointsToAdd > (mSegmentTime / 2) ) {
                AddMoreThanHalfPoints();
            }
        }

        /// <summary>
        /// Adding more than half of the points we have may need multiple passes
        /// </summary>
        private void AddMoreThanHalfPoints()
        {
            int startingPoint = (mSegmentTime - pointsToAdd) / 2;
            if (startingPoint < 1) startingPoint = 1; // Skip the first point
            int startCnt = 0;
            int pointsAdded = 0;
            int endCnt = mSegmentTime - 1; // Last point is added manually

            while (startCnt <= endCnt)
            {
                if (pointsAdded < pointsToAdd && startCnt >= startingPoint)
                {
                    // Build new sample
                    GeoLoc point1 = new GeoLoc(tempList[startCnt - 1].LAT, tempList[startCnt - 1].LON);
                    GeoLoc point2 = new GeoLoc(tempList[startCnt].LAT, tempList[startCnt].LON);
                    double newPointSlope = (tempList[startCnt - 1].SLOPE + tempList[startCnt].SLOPE) / 2;
                    double newPointSpeed = (tempList[startCnt - 1].KPH + tempList[startCnt].KPH) / 2;
                    GeoLocMath geoLocM1 = new GeoLocMath(point1, point2);
                    GeoLoc newPoint = geoLocM1.CalcMidPoint();
                    geoLocM1.point1 = newPoint;
                    geoLocM1.point2 = point1;
                    double newdistanceTravelled = geoLocM1.CalcDistanceBetweenGeoLocations() + tempList[startCnt - 1].KM;
                    // Create new sample and add data from above
                    SAMPLE newSample = new SAMPLE()
                    {
                        SECS = 999, //insertPosition,
                        KM = newdistanceTravelled,
                        KPH = newPointSpeed,
                        ALT = (tempList[startCnt].ALT + tempList[startCnt - 1].ALT) / 2,
                        LAT = newPoint.Latitude,
                        LON = newPoint.Longitude,
                        SLOPE = newPointSlope
                    };

                    // Insert new point in ride
                    tempList.Add(newSample);
                    pointsAdded++;
                }
                startCnt++;
                // We got to the end of the list but if we didn't add all the points we needed, start again.
                if (pointsAdded < pointsToAdd && startCnt >= startingPoint && startCnt > endCnt) 
                { 
                    startCnt = 1;
                    SortMyList();
                }
            }
            SortMyList();
            ListToDebugTxt();
        }

        /// <summary>
        /// Sort the contents of the temp list
        /// </summary>
        private void SortMyList() {

            DataTableOperations dto = new DataTableOperations();
            DataTable dt = dto.LoadTableFromList(tempList);
            dt.DefaultView.Sort = "KM asc";
            dt = dt.DefaultView.ToTable();
            tempList.Clear();
            tempList = dto.LoadListFromTable(dt);
        }

        /// <summary>
        /// Print the contents of the tempList
        /// </summary>
        private void ListToDebugTxt()
        {
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("debug" + mStartTimeMarker.ToString() + "-" + mEndTimeMarket.ToString() + ".csv", "debug");
            if (File.Exists(fullPath)) File.Delete(fullPath);
            using (TextWriter tw = new StreamWriter(fullPath, true)) //Append mode
            {
                foreach (SAMPLE item in tempList)
                {

                    tw.Write(" -- SECS:" + item.SECS.ToString());
                    tw.Write(", KM:" + item.KM.ToString());
                    tw.Write(", KPH:" + item.KPH.ToString());
                    tw.Write(", LAT:" + item.LAT.ToString());
                    tw.Write(", LON:" + item.LON.ToString());
                    tw.WriteLine(", SLOPE:" + item.SLOPE.ToString());
                    tw.Flush();
                }
            }
        }

        /// <summary>
        /// Build a new temp list from the created sublist so we can add points
        /// </summary>
        //private void BuildTempList()
        //{
        //    if (mCreatedList != null)
        //    {
        //        foreach (SAMPLE sample in mCreatedList)
        //        {
        //            tempList.Add(sample);
        //        } 
        //    } else
        //    {
        //        throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + $"Created list is empty ");
        //    }
        //}

        /// <summary>
        /// Create a sublist of the source list based on starting point and items to copy
        /// </summary>
        private void CreateSubListOfSamples ()
        {
            mSegmentTime = mEndTimeMarket - mStartTimeMarker + 1;

            if (mSegmentTime <= 1)
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + $"Items to copy in the list is less than 1.");
            }

            if (mStartTimeMarker > 0) mStartTimeMarker++;

            for (int i = mStartTimeMarker; i <= mEndTimeMarket; i++)
            {
                SAMPLE newSample = new SAMPLE()
                {
                    SECS = mSourceList[i].SECS, //insertPosition,
                    KM = mSourceList[i].KM,
                    KPH = mSourceList[i].KPH,
                    ALT = mSourceList[i].ALT,
                    LAT = mSourceList[i].LAT,
                    LON = mSourceList[i].LON,
                    SLOPE = mSourceList[i].SLOPE
                };
                tempList.Add(newSample);
            }
            if (tempList == null)
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + $"Created list is empty.");
            }
        }
    }
}
