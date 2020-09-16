using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperFunctionLibrary;


namespace CommonLibrary
{
    public class NewRideProcessing
    {
        public int FindSecondsForDistance(double s, ref GoldenCheetahRide thisOldRide)
        {
            int pos = 0;
            while (thisOldRide.RIDE.SAMPLES[pos].KM < s && pos < thisOldRide.RIDE.SAMPLES.Count - 1)
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


        /// <summary>
        /// Add points to the ride for video sync.
        /// videoTime = The number of seconds the video needs to travel the segment
        /// rideTime = The # of SECS at the end of the segment
        /// startTime = The # of SECS at the start of the segment
        /// endTime = the # of SECS at the end of the segment
        /// </summary>
        /// <returns></returns>
        public void AddPointsToNewRide(int videoTime, int startTime, int endTime, ref int thisOldRideCnt, ref int thisNewRideCnt, GoldenCheetahRide thisOldRide, ref GoldenCheetahRide thisNewRide)
        {
            // Running total of new video in seconds
            //videoIndex += videoTime;
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
            //if (startTime > 0)
            //{
            //    videoTime += 1;
            //    segmentTime = endTime - startTime;
            //    endCnt = segmentTime - 1; // Last point is added manually
            //    pointsToAdd = videoTime - segmentTime;
            //}
            //else
            //{
            //    endCnt = endTime;
            //    segmentTime = endTime - startTime + 1;
            //    pointsToAdd = videoTime + 1 - segmentTime;
            //}


            //Calculate the number of points to add and the interval for adding them
            int pointsInterval = -1;
            if (pointsToAdd == 1)
            { pointsInterval = segmentTime / 2; }
            else
            { pointsInterval = segmentTime / pointsToAdd; }
            if (pointsInterval < 1) pointsInterval = 1;

            //Calculate distance and avg speed for the full segment
            GeoLoc startpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[startTime].LAT, thisOldRide.RIDE.SAMPLES[startTime].LON);
            GeoLoc endpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[endTime].LAT, thisOldRide.RIDE.SAMPLES[endTime].LON);
            GeoLocMath geoLocMath = new GeoLocMath();
            double distanceTravelled = geoLocMath.CalculateDistanceBetweenGeoLocations(startpoint, endpoint);
            // Calculate the video speed for the new point
            double videoSpeed = ((distanceTravelled * 1000) / videoTime) * 3.6; //kph

            if (thisNewRide.RIDE.SAMPLES.Count() == 0)
            {
                // This is the first record.
                // Move initial point from old route to new route
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
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

            //Loop and add points
            int cnt = 1;
            int loopCnt = 0;
            while (startCnt <= endCnt) // THIS
            {
                if (cnt == pointsInterval || startCnt == lastSample)  // We only add points at pointsInterval
                {
                    if (pointsAdded < pointsToAdd) // Stop adding points because pointsInterval is converted to integer and not accurate  
                    {
                        // Add new sample
                        GeoLoc point1 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].LON);
                        GeoLoc point2 = new GeoLoc(thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LAT, thisOldRide.RIDE.SAMPLES[thisOldRideCnt].LON);
                        GeoLocMath geoLocMath1 = new GeoLocMath();
                        GeoLoc newPoint = geoLocMath1.CalculateMidPoint(point1, point2);
                        //Get distance between the new point and the one before it
                        double newdistanceTravelled = geoLocMath.CalculateDistanceBetweenGeoLocations(newPoint, point1) + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM;
                        // Create new sample and add data from above
                        SAMPLE newSample = new SAMPLE()
                        {
                            SECS = 999, //insertPosition,
                            KM = newdistanceTravelled,
                            KPH = videoSpeed,
                            ALT = (thisOldRide.RIDE.SAMPLES[thisOldRideCnt].ALT + thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].ALT) / 2,
                            LAT = newPoint.Latitude,
                            LON = newPoint.Longitude,
                            SLOPE = -99
                        };
                        // Insert new point in ride
                        thisNewRide.RIDE.SAMPLES.Add(newSample);
                        thisNewRideCnt += 1;
                        cnt = 1;
                        pointsAdded++;
                    }
                }
                // Move sample to new ride
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                cnt++;
                pointsMoved++;
                loopCnt++;
                startCnt++;
            } // END of addong for loop
            // Move last point from old route to new route
            CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
            thisNewRideCnt += 1;
            thisOldRideCnt++;
        }        // ***** addPointsToNewRide ENDS

        public void RemovePointsFromNewRide(int videoTime, int startTime, int endTime, ref int thisOldRideCnt, ref int thisNewRideCnt,GoldenCheetahRide thisOldRide, ref GoldenCheetahRide thisNewRide )
        {
            // Running total of new video in seconds
            //videoIndex += videoTime;
            int pointsMoved = 0;
            int pointsSkipped = 0;
            // Init vars
            int pointsToRemove = -1;


            //int startCnt = 0;
            //int endCnt = 0;
            int startCnt = startTime;
            int endCnt = endTime;

            int segmentTime = endTime - startTime + 1;

            //endCnt = segmentTime - 1; 
            videoTime++;  // Add 1 second to the video that is lost during subtraction
            //Calculate the number of points to remove and the remove interval
            pointsToRemove = Math.Abs(videoTime - segmentTime);
            int pointsInterval = CalculateRomovePointsInterval(pointsToRemove, segmentTime);

            if (thisNewRide.RIDE.SAMPLES.Count() == 0) // This is the first record so we can't subtract 1 from the counters
            {
                
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                startCnt++;
            }
            else
            {   //Check if point exists in new route before adding it
                if (Math.Abs(thisNewRide.RIDE.SAMPLES[thisNewRideCnt - 1].KM - thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM) > 0.01)
                {
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                    pointsMoved++;
                }
                else // Based on distance point already exists
                {
                    startCnt++;
                    pointsSkipped++;
                }

            }

            // THIS NEEDS A METHOD ---Do we need an extra  drop? 
            int lastSample = -1;
            if ((pointsInterval * pointsToRemove) == segmentTime) lastSample = segmentTime - 1;
            int pointsLeftToRemove = pointsToRemove;


            // if the videoTimeSegment is > than (routeSegmentTime/2) we need to remove more point than we keep.
            // 
            if (videoTime > (segmentTime/2))
            {
                NonConsecutivePointsRemoveProcess(ref startCnt,
                                   ref endCnt,
                                   ref pointsInterval,
                                   ref lastSample,
                                   ref pointsLeftToRemove,
                                   ref thisOldRideCnt,
                                   ref pointsSkipped,
                                   ref thisNewRideCnt,
                                   ref pointsMoved,
                                   ref thisOldRide,
                                   ref thisNewRide);
            } else
            {
                ConsecutivePointsRemoveProcess(ref startCnt,
                                   ref endCnt,
                                   ref pointsInterval,
                                   ref lastSample,
                                   ref pointsLeftToRemove,
                                   ref thisOldRideCnt,
                                   ref pointsSkipped,
                                   ref thisNewRideCnt,
                                   ref pointsMoved,
                                   ref thisOldRide,
                                   ref thisNewRide,
                                   ref videoTime);
            }

            // Get last point from old route
            CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
            thisNewRideCnt += 1;
            thisOldRideCnt++;
        }


        private void ConsecutivePointsRemoveProcess(    ref int startCnt,
                                                        ref int endCnt,
                                                        ref int pointsInterval,
                                                        ref int lastSample,
                                                        ref int pointsLeftToRemove,
                                                        ref int thisOldRideCnt,
                                                        ref int pointsSkipped,
                                                        ref int thisNewRideCnt,
                                                        ref int pointsMoved,
                                                        ref GoldenCheetahRide thisOldRide,
                                                        ref GoldenCheetahRide thisNewRide,
                                                        ref int videoTime)
        {
            int dropCount = (endCnt - startCnt) / videoTime;
            while (startCnt < endCnt)
            {
                thisOldRideCnt += dropCount;
                pointsSkipped += dropCount;
                if (thisOldRideCnt < thisOldRide.RIDE.SAMPLES.Count - 2) // Guard aginst EOF
                {
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    thisNewRideCnt++;
                    thisOldRideCnt++;
                    pointsMoved++;
                    startCnt++;
                }
                startCnt += dropCount;
            }

        }
        private void NonConsecutivePointsRemoveProcess( ref int startCnt,
                                                        ref int endCnt,
                                                        ref int pointsInterval,
                                                        ref int lastSample,
                                                        ref int pointsLeftToRemove,
                                                        ref int thisOldRideCnt,
                                                        ref int pointsSkipped,
                                                        ref int thisNewRideCnt,
                                                        ref int pointsMoved,
                                                        ref GoldenCheetahRide thisOldRide,
                                                        ref GoldenCheetahRide thisNewRide)
        {
            //Loop and remove points
            int cnt = 1;
            while (startCnt < endCnt)
            {
                if (cnt == pointsInterval || startCnt == lastSample) // Skip sample to simulate removing sample in new ride
                {   // Don't remove more than pointsToRemove
                    if (pointsLeftToRemove > 0)
                    {
                        cnt = 1;
                        thisOldRideCnt++;
                        pointsLeftToRemove--;
                        pointsSkipped++;
                        startCnt++;
                    }
                }
                if (thisOldRideCnt < thisOldRide.RIDE.SAMPLES.Count - 2) // Guard aginst EOF
                {
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
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
