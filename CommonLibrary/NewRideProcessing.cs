#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using HelperFunctionLibrary;
using System.Diagnostics;

namespace CommonLibrary
{
    public class NewRideProcessing
    {
        public static List<SAMPLE> testSegment = new List<SAMPLE>();
        public int FindTimeForDistance(double s, ref GoldenCheetahRide thisOldRide)
        {
            int pos = 0;
            while (thisOldRide.RIDE.SAMPLES[pos].KM < s && pos <= thisOldRide.RIDE.SAMPLES.Count - 1)
            {
                pos += 1;
            }
            if (pos > 0)
            {
                if (Math.Abs(thisOldRide.RIDE.SAMPLES[pos].KM - s) > Math.Abs(thisOldRide.RIDE.SAMPLES[pos - 1].KM - s))
                {
                    //Previous position is better match
                    pos--;
                }
            }
            if (pos > thisOldRide.RIDE.SAMPLES.Count || pos < 0) pos = -1;
            return pos;
        }


        public void PointsToAdd (int videoTime, int startTime, int endTime, ref int thisOldRideCnt, ref int thisNewRideCnt, GoldenCheetahRide thisOldRide, ref GoldenCheetahRide thisNewRide)
        {
            int pointsMoved = 0;
            for (int i = startTime; i <= endTime; i++)
            {
                if (thisNewRide.RIDE.SAMPLES.Count() == 0) // New ride is empty
                {
                    // This is the first record. Move initial point from old route to new route
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    thisNewRideCnt++;
                    thisOldRideCnt++;
                    pointsMoved++;
                }
                else // New ride is not empty
                {   //Check if point exists in new route before adding it
                    if (Math.Abs(thisNewRide.RIDE.SAMPLES[thisNewRideCnt - 1].KM - thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM) > 0.01)
                    {
                        CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                        thisNewRideCnt++;
                        thisOldRideCnt++;
                    }
                }
            }
        }

        private int CalculateAddPointsInterval(int _pointsToAdd, int _segmentTime, int _endCnt, int _videoTime)
        {
            int _pointsInterval = -99;
            //Calculate the number of points to add and the interval for adding them

            //if (_pointsToAdd == 1)
            //{ _pointsInterval = _segmentTime / 2; }
            //else
            //{ _pointsInterval = _segmentTime / _pointsToAdd; }

            if (_pointsToAdd == 1)
            {
                _pointsInterval = _segmentTime / 2;
                return _pointsInterval;
            }

            if (_pointsToAdd <= (_segmentTime / 2))
            {
                _pointsInterval = _segmentTime / _pointsToAdd;
                //return _pointsInterval;
            }


            // _pointsInterval must have a positive value here.

            if ((_pointsInterval * _pointsToAdd) >= _endCnt && _pointsInterval > 2) 
            {
                _pointsInterval--;
                //return _pointsInterval;
            }
            
            if (_pointsInterval < 1)
            {
                _pointsInterval = 1;
                return _pointsInterval;
            }

            //if (_pointsToAdd > (_segmentTime /2) && _pointsToAdd < _segmentTime)
            //{
            //    //ADD every other point starting with (_segmentTime - _pointsToAdd) / 2
            //}

            if (_pointsInterval == 1)
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " -- Cannot ADD points, pointsInterval = 1");
             }
            return _pointsInterval;
        }

        // This method will add points to a  route segment if 
        // the # of points to be added < half of the size of the segment
        private void LessThanHalfPointsAddProcess(ref int startCnt,
                                                    ref int endCnt,
                                                    ref int pointsInterval,
                                                    //ref int lastSample,
                                                    ref int pointsToAdd,
                                                    ref int pointsAdded,
                                                    ref int thisOldRideCnt,
                                                    ref int thisNewRideCnt,
                                                    ref int pointsMoved,
                                                    //ref double videoSpeed,
                                                    ref GoldenCheetahRide thisOldRide,
                                                    ref GoldenCheetahRide thisNewRide,
                                                    ref string fullPath)
        {
            //Loop and add points
            int cnt = 1;
            int loopCnt = 0;
            while (startCnt <= endCnt) // THIS
            {
                if (cnt == pointsInterval)// || startCnt == lastSample)  // We only add points at pointsInterval
                {
                    if (pointsAdded < pointsToAdd) // Stop adding points because pointsInterval is converted to integer and not accurate  
                    {
                        // Add new sample
                        GeoLoc point1 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LON);
                        GeoLoc point2 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LON);
                        double newPointSlope = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].SLOPE + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].SLOPE) / 2;
                        double newPointSpeed = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KPH + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KPH) / 2;

                        GeoLocMath geoLocM1 = new GeoLocMath(point1, point2);
                        GeoLoc newPoint = geoLocM1.CalcMidPoint();
                        geoLocM1.point1 = newPoint;
                        geoLocM1.point2 = point1;
                        double newdistanceTravelled = geoLocM1.CalcDistanceBetweenGeoLocations() + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM;

                        // Create new sample and add data from above
                        SAMPLE newSample = new SAMPLE()
                        {
                            SECS = 999, //insertPosition,
                            KM = newdistanceTravelled,
                            KPH = newPointSpeed,
                            ALT = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt].ALT + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].ALT) / 2,
                            LAT = newPoint.Latitude,
                            LON = newPoint.Longitude,
                            SLOPE = newPointSlope
                        };
                        // Insert new point in ride
                        thisNewRide.RIDE.SAMPLES.Add(newSample);
                        testSegment.Add(newSample);
                        SaveLineToTxt(newSample, fullPath, -1, thisNewRideCnt);
                        thisNewRideCnt += 1;
                        cnt = 1;
                        pointsAdded++;
                    }
                }
                // Move sample to new ride
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
                SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                cnt++;
                pointsMoved++;
                loopCnt++;
                startCnt++;
            } // END of addong for loop
        }

        // This method will add points to a  route segment if 
        // the # of points to be added > half of the size of the segment
        // and < the size of the segment.
        // ADD every other point starting at pasition (_segmentTime - _pointsToAdd) / 2
        private void MoreThanHalfPointsAddProcess(ref int startCnt,
                                            ref int endCnt,
                                            ref int pointsInterval,
                                            //ref int lastSample,
                                            ref int pointsToAdd,
                                            ref int pointsAdded,
                                            ref int thisOldRideCnt,
                                            ref int thisNewRideCnt,
                                            ref int pointsMoved,
                                            ref int segmentTime,
                                            ref GoldenCheetahRide thisOldRide,
                                            ref GoldenCheetahRide thisNewRide,
                                            ref string fullPath)
        {

            //Loop and add points
            int cnt = 1;
            //int loopCnt = 0;
            int startingPoint = (segmentTime - pointsToAdd) / 2;

            while (startCnt <= endCnt) 
            {
                if (pointsAdded < pointsToAdd && startCnt >= startingPoint)  
                {
                    // Add new sample
                    GeoLoc point1 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LON);
                    GeoLoc point2 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LON);
                    double newPointSlope = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].SLOPE + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].SLOPE) / 2;
                    double newPointSpeed = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KPH + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KPH) / 2;
                    
                    GeoLocMath geoLocM1 = new GeoLocMath(point1, point2);
                    GeoLoc newPoint = geoLocM1.CalcMidPoint();
                    geoLocM1.point1 = newPoint;
                    geoLocM1.point2 = point1;
                    double newdistanceTravelled = geoLocM1.CalcDistanceBetweenGeoLocations() + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM;
                    // Create new sample and add data from above
                    SAMPLE newSample = new SAMPLE()
                    {
                        SECS = 999, //insertPosition,
                        KM = newdistanceTravelled,
                        KPH = newPointSpeed,
                        ALT = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt].ALT + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].ALT) / 2,
                        LAT = newPoint.Latitude,
                        LON = newPoint.Longitude,
                        SLOPE = newPointSlope
                    };
                    // Insert new point in ride
                    thisNewRide.RIDE.SAMPLES.Add(newSample);
                    testSegment.Add(newSample);
                    SaveLineToTxt(newSample, fullPath, -1, thisNewRideCnt);
                    thisNewRideCnt += 1;
                    cnt = 1;
                    pointsAdded++;
                }

                // Move sample to new ride
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
                SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                cnt++;
                pointsMoved++;
                startCnt++;
            } // END of adding for loop

            // Need to check if all points needed were added.
            // If not, loop and add them

            while (pointsAdded < pointsToAdd)
            {

                AddNewPoint( 
                    ref thisOldRideCnt,
                    ref thisNewRideCnt,
                    ref pointsAdded,
                    ref thisOldRide,
                    ref thisNewRide,
                    ref fullPath);

            }

            if (pointsAdded < pointsToAdd)
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().ToString() + 
                    $" -- Only added {pointsAdded} out of {pointsToAdd} ");
            }
        }



        /// <summary>
        /// Add a new point between 2 adjacent ones
        /// </summary>
        private void  AddNewPoint (
                                ref int thisOldRideCnt,
                                ref int thisNewRideCnt,
                                ref int pointsAdded,
                                ref GoldenCheetahRide thisOldRide,
                                ref GoldenCheetahRide thisNewRide,
                                ref string fullPath)
        {


            // Add new sample
            GeoLoc point1 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LON);
            GeoLoc point2 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LON);
            double newPointSlope = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].SLOPE + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].SLOPE) / 2;
            double newPointSpeed = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KPH + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KPH) / 2;

            GeoLocMath geoLocM1 = new GeoLocMath(point1, point2);
            GeoLoc newPoint = geoLocM1.CalcMidPoint();
            geoLocM1.point1 = newPoint;
            geoLocM1.point2 = point1;
            double newdistanceTravelled = geoLocM1.CalcDistanceBetweenGeoLocations() + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM;
            // Create new sample and add data from above
            SAMPLE newSample = new SAMPLE()
            {
                SECS = 999, //insertPosition,
                KM = newdistanceTravelled,
                KPH = newPointSpeed,
                ALT = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt].ALT + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].ALT) / 2,
                LAT = newPoint.Latitude,
                LON = newPoint.Longitude,
                SLOPE = newPointSlope
            };
            // Insert new point in ride
            thisNewRide.RIDE.SAMPLES.Add(newSample);
            testSegment.Add(newSample);
            SaveLineToTxt(newSample, fullPath, -1, thisNewRideCnt);
            thisNewRideCnt += 1;

            pointsAdded++;
        }



        /// <summary>
        /// Add points to the ride for video sync.
        /// videoTime = The number of seconds the video needs to travel the segment
        /// rideTime = The # of SECS at the end of the segment
        /// startTime = The # of SECS at the start of the segment
        /// endTime = the # of SECS at the end of the segment
        /// </summary>
        /// <returns></returns>
        public List<SAMPLE> AddPointsToNewRide(int videoTime, int startTime, int endTime, ref int thisOldRideCnt, ref int thisNewRideCnt, GoldenCheetahRide thisOldRide, ref GoldenCheetahRide thisNewRide)
        {
            testSegment.Clear();

#if (DEBUG)
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("debug" + startTime.ToString() + "-" + endTime.ToString() + ".csv", "debug");

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            } 
#endif

            int pointsAdded = 0;
            int pointsMoved = 0;
            // Init vars
            int pointsToAdd = -1;
            int startCnt = 0;
            int endCnt = 0;
            int segmentTime = 0;
            // If this is not the first record we need to add a second to the video time
            videoTime++;
            segmentTime = endTime - startTime + 1;
            endCnt = segmentTime - 1; // Last point is added manually
            pointsToAdd = videoTime - segmentTime;

            //Calculate the number of points to add and the interval for adding them
            int pointsInterval = CalculateAddPointsInterval(pointsToAdd, segmentTime, endCnt, videoTime);

            //Calculate distance and avg speed for the full segment
            GeoLoc startpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[startTime].LAT, thisOldRide.RIDE.SAMPLES[startTime].LON);
            GeoLoc endpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[endTime].LAT, thisOldRide.RIDE.SAMPLES[endTime].LON);
            GeoLocMath geoLocMath = new GeoLocMath(startpoint, endpoint);
            double distanceTravelled = geoLocMath.CalcDistanceBetweenGeoLocations();

            if (thisNewRide.RIDE.SAMPLES.Count() == 0)
            {
                // This is the first record.
                // Move initial point from old route to new route
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
#if (DEBUG)
                SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt); 
#endif
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                startCnt++;
                pointsMoved++;
                endCnt--; // THIS
            }
            else
            {   //Check if point exists in new route before adding it
                if (Math.Abs(thisNewRide.RIDE.SAMPLES[thisNewRideCnt - 1].KM - thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM) > 0.01)
                {
                    // Move initial point from old route to new route
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
#if (DEBUG)
                    SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
#endif
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                    pointsMoved++;
                }
                else
                {
                    // First point is there already, move to the next one and decriment the number of points to move
                    startCnt++;
                    endCnt--;
                }

            }

            // If the last sample to add equals the segment length, add it right before the end
            int lastSample = -1;
            if ((pointsInterval * pointsToAdd) == endCnt) lastSample = endCnt - 1;

            if (pointsToAdd > (segmentTime / 2) && pointsToAdd < segmentTime)
            {
                //ADD every other point starting with (_segmentTime - _pointsToAdd) / 2
                MoreThanHalfPointsAddProcess(ref startCnt,
                                ref endCnt,
                                ref pointsInterval,
                                //ref lastSample,
                                ref pointsToAdd,
                                ref pointsAdded,
                                ref thisOldRideCnt,
                                ref thisNewRideCnt,
                                ref pointsMoved,
                                ref segmentTime,
                                ref thisOldRide,
                                ref thisNewRide,
                                ref fullPath);
            } else
            {
                LessThanHalfPointsAddProcess(ref startCnt,
                                ref endCnt,
                                ref pointsInterval,
                                //ref lastSample,
                                ref pointsToAdd,
                                ref pointsAdded,
                                ref thisOldRideCnt,
                                ref thisNewRideCnt,
                                ref pointsMoved,
                                ref thisOldRide,
                                ref thisNewRide,
                                ref fullPath);
            }

            // Move last point from old route to new route
            CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
            CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
            SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
            thisNewRideCnt += 1;
            thisOldRideCnt++;

            return testSegment;

        }        // ***** addPointsToNewRide ENDS


        //Video segment and ride segment are equal. Move all points from the old ride to the new one.
        public List<SAMPLE> MoveAllPointsToNewRide (int videoTime, int startTime, int endTime, ref int thisOldRideCnt, 
            ref int thisNewRideCnt, GoldenCheetahRide thisOldRide, ref GoldenCheetahRide thisNewRide)
        {

#if (DEBUG)
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("debug" + startTime.ToString() + "-" + endTime.ToString() + ".csv", "debug");

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

#endif
            testSegment.Clear();
            int startCnt = startTime;
            int endCnt = endTime;

            if (thisNewRide.RIDE.SAMPLES.Count() == 0) // This is the first record so we can't subtract 1 from the counters
            {
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
#if (DEBUG)
                SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt); 
#endif
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                startCnt++;
            }
            else
            {   //Check if point exists in new route before adding it
                if (Math.Abs(thisNewRide.RIDE.SAMPLES[thisNewRideCnt - 1].KM - thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM) > 0.01)
                {
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
#if (DEBUG)
                    SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
#endif
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                    //pointsMoved++;
                }
                else // Based on distance point already exists
                {
                    startCnt++;
                    //pointsSkipped++;
                }
            }

            while (startCnt <= endCnt)
            {
                if (thisOldRideCnt < thisOldRide.RIDE.SAMPLES.Count - 2) // Guard aginst EOF
                {
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
#if (DEBUG)
                    SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
#endif
                    thisNewRideCnt++;
                }
                startCnt++;
                thisOldRideCnt++;
            }
            return testSegment;
        }



        public List<SAMPLE> RemovePointsFromNewRide(int videoTime, int startTime, int endTime, ref int thisOldRideCnt, ref int thisNewRideCnt,GoldenCheetahRide thisOldRide, ref GoldenCheetahRide thisNewRide )
        {
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("debug" + startTime.ToString() + "-" + endTime.ToString() + ".csv", "debug");

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            testSegment.Clear();
            int pointsMoved = 0;
            //int pointsSkipped = 0;
            int pointsToRemove = -1;
            int startCnt = startTime;
            int endCnt = endTime;
            int segmentTime = endTime - startTime + 1;

            videoTime++;  // Add 1 second to the video that is lost during subtraction. 
            // If we only need to remove 1 point the above increment will make pointsToRemove == 0.
            pointsToRemove = Math.Abs(videoTime - segmentTime);
            if (pointsToRemove == 0)
            {
                pointsToRemove = 1;
            }

            int pointsInterval = CalculateRomovePointsInterval(pointsToRemove, segmentTime);

            if (thisNewRide.RIDE.SAMPLES.Count() == 0) // This is the first record so we can't subtract 1 from the counters
            {
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
                SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                startCnt++;
            }
            else
            {   //Check if point exists in new route before adding it
                if (Math.Abs(thisNewRide.RIDE.SAMPLES[thisNewRideCnt - 1].KM - thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM) > 0.01)
                {
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
                    SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                    pointsMoved++;
                }
                else // Based on distance point already exists
                {
                    startCnt++;
                    //pointsSkipped++;
                }
            }

            // THIS NEEDS A METHOD ---Do we need an extra  drop? 
            //int lastSample = -1;
            //if ((pointsInterval * pointsToRemove) == segmentTime) lastSample = segmentTime - 1;




            if ((pointsInterval * pointsToRemove) >= segmentTime - 1) pointsInterval--;
            int pointsLeftToRemove = pointsToRemove;


            // if the videoTimeSegment is > than (routeSegmentTime/2) we need to remove more points than we keep.
            // 
            if (videoTime > (segmentTime/2))
            {
                NonConsecutivePointsRemoveProcess(ref startCnt,
                                   ref endCnt,
                                   ref pointsInterval,
                                   //ref lastSample,
                                   ref pointsLeftToRemove,
                                   ref thisOldRideCnt,
                                   //ref pointsSkipped,
                                   ref thisNewRideCnt,
                                   ref pointsMoved,
                                   ref thisOldRide,
                                   ref thisNewRide,
                                   ref fullPath);
            } else
            {
                ConsecutivePointsRemoveProcess(ref startCnt,
                                   ref endCnt,
                                   ref pointsInterval,
                                   //ref lastSample,
                                   ref pointsLeftToRemove,
                                   ref thisOldRideCnt,
                                   //ref pointsSkipped,
                                   ref thisNewRideCnt,
                                   ref pointsMoved,
                                   ref thisOldRide,
                                   ref thisNewRide,
                                   ref videoTime,
                                   ref fullPath);
            }

            // Get last point from old route
            CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
            CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
            SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
            thisNewRideCnt += 1;
            thisOldRideCnt++;


            //// Save temp list
            //TextConnector tc = new TextConnector();
            //string fullPath = tc.FullFilePath("debug" + startTime.ToString() + "-"  + endTime.ToString() + ".csv", "debug");
            //SaveToTxt(testSegment, fullPath);
            return testSegment;

        }

        // Too many points to drop. Rather than removing points just move some points to the new ride
        private void ConsecutivePointsRemoveProcess(    ref int startCnt,
                                                        ref int endCnt,
                                                        ref int pointsInterval,
                                                        //ref int lastSample,
                                                        ref int pointsLeftToRemove,
                                                        ref int thisOldRideCnt,
                                                        //ref int pointsSkipped,
                                                        ref int thisNewRideCnt,
                                                        ref int pointsMoved,
                                                        ref GoldenCheetahRide thisOldRide,
                                                        ref GoldenCheetahRide thisNewRide,
                                                        ref int videoTime,
                                                        ref string fullPath)
        {
            int dropCount = (endCnt - startCnt) / (videoTime - 1);
            if (dropCount * (videoTime - 1) > endCnt - startCnt) dropCount--;
            if (dropCount <= 1)
            {
                throw new Exception("ConsecutivePointsRemoveProcess -- Cannot remove all points");
            }

            int addedCntr = 1;
            bool done = false;
            while (startCnt < endCnt && !done)
            {
                thisOldRideCnt += dropCount;
                //pointsSkipped += dropCount;
                if (thisOldRideCnt < thisOldRide.RIDE.SAMPLES.Count - 2) // Guard aginst EOF
                {
                    if (addedCntr < videoTime - 1) // Don't add more points than needed
                    {
                        CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                        CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
                        SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
                        thisNewRideCnt++;
                        //thisOldRideCnt++;
                        pointsMoved++;
                        addedCntr++; 
                    } else
                    {
                        done = true;
                        thisOldRideCnt = endCnt;
                    }
                }
                startCnt += dropCount;
            }

        }
        private void NonConsecutivePointsRemoveProcess( ref int startCnt,
                                                        ref int endCnt,
                                                        ref int pointsInterval,
                                                        //ref int lastSample,
                                                        ref int pointsLeftToRemove,
                                                        ref int thisOldRideCnt,
                                                        //ref int pointsSkipped,
                                                        ref int thisNewRideCnt,
                                                        ref int pointsMoved,
                                                        ref GoldenCheetahRide thisOldRide,
                                                        ref GoldenCheetahRide thisNewRide,
                                                        ref string fullPath)
        {
            if (pointsInterval <= 1)
            {
                throw new Exception("ConsecutivePointsRemoveProcess -- Cannot remove all points");
            }

            //Loop and remove points
            int cnt = 1;
            while (startCnt < endCnt)
            {
                if (cnt == pointsInterval)// || startCnt == lastSample) // Skip sample to simulate removing sample in new ride
                {   // Don't remove more than pointsToRemove
                    if (pointsLeftToRemove > 0)
                    {
                        cnt = 1;
                        thisOldRideCnt++;
                        pointsLeftToRemove--;
                        //pointsSkipped++;
                        startCnt++;
                    }
                }
                if (startCnt < endCnt)
                {
                    if (thisOldRideCnt <= thisOldRide.RIDE.SAMPLES.Count - 2) // Guard aginst EOF
                    {
                        CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                        CopySampleToTestlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref testSegment);
                        SaveLineToTxt(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], fullPath, thisOldRideCnt, thisNewRideCnt);
                        thisNewRideCnt++;
                        thisOldRideCnt++;
                        cnt++;
                        pointsMoved++;
                        startCnt++;
                    }
                    else
                    {
                        startCnt = endCnt;
                        thisNewRideCnt++;
                        thisOldRideCnt++;
                    } 
                }
            } // END of removing for loop
        }

        private void CopySampleToNewlist(SAMPLE srcSample, ref GoldenCheetahRide destination)
        {
            SAMPLE newSample = new SAMPLE()
            {
                SECS = srcSample.SECS, //insertPosition,
                KM = srcSample.KM,
                KPH = srcSample.KPH,
                ALT = srcSample.ALT,
                LAT = srcSample.LAT,
                LON = srcSample.LON,
                SLOPE = srcSample.SLOPE
            };
            // Insert new point in ride
            destination.RIDE.SAMPLES.Add(newSample);
        }

        public static void SaveToTxt(List<SAMPLE> myList, string path)
        {
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (var item in myList)
                {
                    tw.Write("SECS:" + item.SECS.ToString());
                    tw.Write(", KM:" + item.KM.ToString());
                    tw.Write(", KPH:" + item.KPH.ToString());
                    tw.Write(", LAT:" + item.LAT.ToString());
                    tw.Write(", LON:" + item.LON.ToString());
                    tw.WriteLine(", SLOPE:" + item.SLOPE.ToString());
                    tw.Flush();

                }
            }
        }

        public static void SaveLineToTxt(SAMPLE item, string path, int origTime, int newTime)
        {
            using (TextWriter tw = new StreamWriter(path, true)) //Append mode
            {
                tw.Write("From sec:" + origTime.ToString());
                tw.Write(" to sec:" + newTime.ToString());
                tw.Write(" -- SECS:" + item.SECS.ToString());
                tw.Write(", KM:" + item.KM.ToString());
                tw.Write(", KPH:" + item.KPH.ToString());
                tw.Write(", LAT:" + item.LAT.ToString());
                tw.Write(", LON:" + item.LON.ToString());
                tw.WriteLine(", SLOPE:" + item.SLOPE.ToString());
                tw.Flush();
            }
        }

        private void CopySampleToTestlist(SAMPLE srcSample, ref List<SAMPLE> destination)
        {
            SAMPLE newSample = new SAMPLE()
            {
                SECS = srcSample.SECS, //insertPosition,
                KM = srcSample.KM,
                KPH = srcSample.KPH,
                ALT = srcSample.ALT,
                LAT = srcSample.LAT,
                LON = srcSample.LON,
                SLOPE = srcSample.SLOPE
            };
            // Insert new point in ride
            destination.Add(newSample);
        }

        private int CalculatePointsDifference(int videoSegmentSecs, int endCounter)
        {
            int pointsDiff = Math.Abs(videoSegmentSecs - endCounter);
            int pointsInterval = -1;
            if (pointsDiff == 1)
            {
                pointsInterval = (int)Math.Round(endCounter / 2.0);
            }
            else
            {
                pointsInterval = (int)Math.Round(Math.Abs(endCounter / (pointsDiff + 1.0)));
            }
            if (pointsInterval < 1) pointsInterval = 1;

            if (pointsDiff < 1)
            {
                pointsDiff = 1;
                pointsInterval = endCounter / 2;
            }

            return pointsInterval;
            //pointsToAdd = videoTime - segmentTime;
            //int pointsInterval = -1;
            //if (pointsToAdd == 1)
            //{ pointsInterval = segmentTime / 2; }
            //else
            //{ pointsInterval = segmentTime / pointsToAdd; }
            //if (pointsInterval < 1) pointsInterval = 1;

        }

        private int CalculateRomovePointsInterval (int pointsToRemove, int segmentTime)
        {
            int pointsInterval = -99;
            if (pointsToRemove == 1)
            {
                pointsInterval = (int)Math.Round(segmentTime / 2.0);
            }
            else
            {
                pointsInterval = (int)Math.Round(Math.Abs(segmentTime / (pointsToRemove + 1.0)));
            }
            if (pointsInterval < 1) pointsInterval = 1;

            return pointsInterval;
        }
    }
}
