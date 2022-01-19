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
        public List<SAMPLE> mDestList { private get; set; } 
        public int mStartTimeMarker { get; set; }
        public int mEndTimeMarket { get; set; }
        public int mVideoTime { get; set; }
        public List<SAMPLE> mTempList { get; private set; } = new List<SAMPLE>();
        private int mSegmentTime { get; set; } = -1;




        private int pointsToAdd = -1;
        private int pointsToRemove = -1;
        private int pointsInterval = -1;
        private int startCnt = -1;

        //CTOR
        public CreateListForSegment(List<SAMPLE> sourceList, List<SAMPLE> destList, int startTimeMarker, int endTimeMarker, int videoTime)
        {
            this.mSourceList = sourceList ?? throw new ArgumentNullException(nameof(mSourceList));
            this.mStartTimeMarker = startTimeMarker;
            this.mEndTimeMarket = endTimeMarker;
            this.mTempList.Clear();
            this.mDestList = destList;
            this.mVideoTime = videoTime + 1; // Account for the 0 second
            CreateSubListOfSamples(); // Create a sublist to add or remove points, based on the inpute values

            //First record starts with 0 second.
            if (mDestList.Count() == 0 )
            {
                startCnt = 0;
            } else
            {
                startCnt = 1;
            }


        }

        /// <summary>
        /// Append items from the tempList to the destination list
        /// </summary>
        public void AppendToDestList(List<SAMPLE> destList)
        {
            foreach (SAMPLE sample in mTempList)
            {
                //Check is fample exists
                destList.Add(sample);
            }
            // Write debug info
            ListToDebugTxt();
        }

        /// <summary>
        /// Select which add process to use. 
        /// If we need to add more than half of the points we have may we need multiple passes
        /// Otherwise we need to calculate the interval to add points
        /// </summary>
        public void AddPointsToListProcess()
        {
            pointsToAdd = mVideoTime - mSegmentTime;
            if (pointsToAdd > (mSegmentTime / 2) ) {
                AddMoreThanHalfPoints();
            } else
            {
                AddLessThanHalfPoints();
            }
        }

        /// <summary>
        /// Select the process to use based on the segment size and the # of points to remove
        /// </summary>
        public void RemovePointsFromListProcess()
        {
            pointsToRemove = Math.Abs(mVideoTime - mSegmentTime);
            pointsInterval = CalculateRomovePointsInterval();

            if (mVideoTime > (mSegmentTime / 2))
            {
                NonConsecutivePointsRemoveProcess();
            } else
            {
                ConsecutivePointsRemoveProcess();
            }
        }


        /// <summary>
        /// Process to remove more than half the point of the list
        /// Remove every other point and repeat until all points are removed
        /// </summary>
        private void ConsecutivePointsRemoveProcess()
        {
            int endCnt = mSegmentTime; 
            int pointsRemoved = 0;
            int cnt = 0;

            int dropCount = (endCnt - startCnt) / (mVideoTime - 1);
            if (dropCount * (mVideoTime - 1) > endCnt - startCnt) dropCount--;
            if (dropCount <= 1)
            {
                throw new Exception($"*** ERROR *** Consecutive Points Remove Process drop count = { dropCount } at possition { mTempList[0].SECS-1 }");
            }

            while (startCnt <= mTempList.Count - 1) // We don't want to remove the last point because it's a marker
            {
                if (pointsRemoved < pointsToRemove && cnt == dropCount)
                {
                    mTempList.RemoveAt(startCnt - 1);
                    cnt = 0;
                    pointsRemoved++;
                }
                cnt++;
                startCnt++;

                // The list is smaller than our position but if we didn't remove all the points we needed, start again.
                if (pointsRemoved < pointsToRemove && startCnt > mTempList.Count - 1)
                {
                    startCnt = 1;
                    //endCnt = mTempList.Count;
                    SortMyList();
                }

            }

        }

        /// <summary>
        /// Process to remove less than half the point in a list
        /// </summary>
        private void NonConsecutivePointsRemoveProcess()
        {
            //startCnt = 1;
            int pointsRemoved = 0;
            int endCnt = mSegmentTime - 1; // Last point is already there
            int cnt = 1;

            while (startCnt <= endCnt)
            {
                if (pointsRemoved < pointsToRemove && cnt == pointsInterval)
                {
                    mTempList.RemoveAt(startCnt-1);
                    cnt = 1;
                    pointsRemoved++;
                }
                cnt++;
                startCnt++;
            }
        }

        private int CalculateRomovePointsInterval()
        {
            int _pointsInterval = -99;
            if (pointsToRemove == 1)
            {
                _pointsInterval = (int)Math.Round(mSegmentTime / 2.0);
            }
            else
            {
                _pointsInterval = (int)Math.Round(Math.Abs(mSegmentTime / (pointsToRemove + 1.0)));
            }

            if ((_pointsInterval * pointsToRemove) >= mSegmentTime - 1) _pointsInterval--;

            //if (pointsInterval < 1) pointsInterval = 1;

            return _pointsInterval;
        }


        /// <summary>
        /// Adding less that half of the segment length. Need to calculate the insert inteval
        /// </summary>
        private void AddLessThanHalfPoints()
        {
            pointsInterval = CalculateAddPointsInterval();

            //startCnt = 1; 
            int pointsAdded = 0;
            int endCnt = mSegmentTime - 1; // Last point is already there
            int cnt = 1;

            while (startCnt <= endCnt)
            {
                if (pointsAdded < pointsToAdd && cnt == pointsInterval)
                {
                    SAMPLE newSample = BuildNewSample(startCnt);
                    // Insert new point in ride
                    mTempList.Add(newSample);
                    pointsAdded++;
                    cnt = 1;
                    //if (pointsAdded == pointsToAdd) done = true;
                }
                startCnt++;
                cnt++;
            }
            SortMyList();
        }

        /// <summary>
        /// Calculate the interval for adding new point to the segment
        /// </summary>
        /// <returns></returns>
        private int CalculateAddPointsInterval()
        {
            int _pointsInterval = -99;

            if (pointsToAdd <= (mSegmentTime / 2))
            {
                _pointsInterval = mSegmentTime / pointsToAdd;
            }

            if (pointsToAdd > (mSegmentTime - 1))
            {
                _pointsInterval = 2;
            }

            if (pointsToAdd == 1)
            {
                _pointsInterval = mSegmentTime / 2;
            }

            if ((_pointsInterval * pointsToAdd) >= mSegmentTime - 1) _pointsInterval--;

            if (_pointsInterval <= 1 || _pointsInterval == -99)
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " -- Cannot ADD points at file time: " + (mStartTimeMarker-1) + ", pointsInterval = " + _pointsInterval.ToString());
            }
            return _pointsInterval;
        }

        /// <summary>
        /// Adding more than half of the points we have may need multiple passes
        /// </summary>
        private void AddMoreThanHalfPoints()
        {
            int startingPoint = (mSegmentTime - pointsToAdd) / 2;
            if (startingPoint < 1) startingPoint = 1; 
            //startCnt = 0;
            int pointsAdded = 0;
            int endCnt = mSegmentTime - 1; // Last point is already there

            while (startCnt <= endCnt)
            {
                if (pointsAdded < pointsToAdd && startCnt >= startingPoint)
                {
                    SAMPLE newSample = BuildNewSample(startCnt);
                    // Insert new point in ride


                    // *** Should not add at the end of the list  ***
                    if (startCnt != endCnt)
                    {
                        mTempList.Add(newSample);
                        pointsAdded++; 
                    }


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
        }


        /// <summary>
        /// Create and return a new SAMPLE based on the index in the mTempList
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private SAMPLE BuildNewSample(int index)
        {
            SAMPLE newSample = new SAMPLE();
            // Build new sample
            GeoLoc point1 = new GeoLoc(mTempList[index - 1].LAT, mTempList[index - 1].LON);
            GeoLoc point2 = new GeoLoc(mTempList[index].LAT, mTempList[index].LON);
            double newPointSlope = (mTempList[index - 1].SLOPE + mTempList[index].SLOPE) / 2;
            double newPointSpeed = (mTempList[index - 1].KPH + mTempList[index].KPH) / 2;
            GeoLocMath geoLocM1 = new GeoLocMath(point1, point2);
            GeoLoc newPoint = geoLocM1.CalcMidPoint();
            geoLocM1.point1 = newPoint;
            geoLocM1.point2 = point1;
            double newdistanceTravelled = geoLocM1.CalcDistanceBetweenGeoLocations() + mTempList[index - 1].KM;
            // Create new sample and add data from above
            newSample = new SAMPLE()
            {
                SECS = 999999, //insertPosition,
                KM = newdistanceTravelled,
                KPH = newPointSpeed,
                ALT = (mTempList[index].ALT + mTempList[index - 1].ALT) / 2,
                LAT = newPoint.Latitude,
                LON = newPoint.Longitude,
                SLOPE = newPointSlope
            };
            return newSample;
        }

        /// <summary>
        /// Sort the contents of the temp list
        /// </summary>
        private void SortMyList() {
            DataTableOperations dto = new DataTableOperations();
            DataTable dt = dto.LoadTableFromList(mTempList);
            dt.DefaultView.Sort = "KM asc";
            dt = dt.DefaultView.ToTable();
            mTempList.Clear();
            mTempList = dto.LoadListFromTable(dt);
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
                foreach (SAMPLE item in mTempList)
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
        /// Create a sublist of the source list based on starting point and items to copy
        /// </summary>
        private void CreateSubListOfSamples ()
        {
            mSegmentTime = mEndTimeMarket - mStartTimeMarker + 1;

            if (mSegmentTime <= 1)
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + $"File time: { mStartTimeMarker }. Items to copy in the list is less than 1.");
            }

            if (mStartTimeMarker > 0) mStartTimeMarker++;

            for (int i = mStartTimeMarker; i <= mEndTimeMarket; i++) // Skip point already there
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
                mTempList.Add(newSample);
            }
            if (mTempList == null)
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + $"Created list is empty.");
            }
        }
    }
}
