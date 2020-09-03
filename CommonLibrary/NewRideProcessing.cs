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
        public int convertDistanceToSeconds(double s, ref GoldenCheetahRide thisOldRide)
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

                    //Debug.WriteLine("Previous position is better match: " +
                    //    pos.ToString() + " current = " + thisOldRide.RIDE.SAMPLES[pos].KM.ToString() +
                    //    " previous = " + thisOldRide.RIDE.SAMPLES[pos - 1].KM.ToString() +
                    //    ". s = " + s.ToString());
                    pos--;
                }
            }

            if (pos > thisOldRide.RIDE.SAMPLES.Count || pos < 0) pos = -1;

            return pos;
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

            // Init vars
            int pointsToAdd = -1;
            int startCnt = 0;
            int endCnt = 0;
            int segmentTime = endTime - startTime + 1;
            // If this is not the first record we need to add a second to the times
            if (startTime > 0)
            {
                //endCnt = (endTime - startTime) + 1;
                videoTime += 1;
                endCnt = videoTime;
            }
            else
            {
                endCnt = endTime;
            }

            //logwriter.WriteLine("Start marker = {0}, {4}, -- end marker = {1}, {5}, -- video time = {2}, segment time = {3}", startTime, endTime, videoTime, segmentTime, fromKM, toKM);

            //Calculate the number of points to remove and the remove interval
            pointsToAdd = videoTime - segmentTime;
            int pointsInterval = -1;
            if (pointsToAdd == 1)
            { pointsInterval = segmentTime / 2; }
            else
            { pointsInterval = segmentTime / pointsToAdd; }

            if (pointsInterval < 1) pointsInterval = 1;

            //logwriter.WriteLine("Removing " + pointsToRemove.ToString() + " points from the ride.");
            //logwriter.WriteLine("ADDING --- Points interval:" + pointsInterval.ToString() + " points to ADD: " + pointsToAdd.ToString());

            //Calculate distance and avg speed for the full segment
            GeoLoc startpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[startTime].LAT, thisOldRide.RIDE.SAMPLES[startTime].LON);
            GeoLoc endpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[endTime].LAT, thisOldRide.RIDE.SAMPLES[endTime].LON);
            GeoLocMath geoLocMath = new GeoLocMath();
            double distanceTravelled = geoLocMath.CalculateDistanceBetweenGeoLocations(startpoint, endpoint);
            // Need to use video time to derive video speed
            double videoSpeed = ((distanceTravelled * 1000) / videoTime) * 3.6; //kph

            if (thisNewRide.RIDE.SAMPLES.Count() == 0)
            {
                // This is the first record.
                // Move initial point from old route to new route
                //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                //logwriter.WriteLine("Moving FIRST (INIT) record: " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KM);
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                startCnt++;
            }
            else
            {   //Check if point exists in new route before adding it
                if (Math.Abs(thisNewRide.RIDE.SAMPLES[thisNewRideCnt - 1].KM - thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM) > 0.01)
                {
                    // Move initial point from old route to new route
                    //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    //logwriter.WriteLine("Moving FIRST record: " + oldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[oldRideCnt].KM);
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                }
                else
                {
                    //We moved the first point manually
                    startCnt++;
                    endCnt--;
                    //logwriter.WriteLine("FIRST record EXISTS, increamenting startCnt: " + startCnt.ToString() + " decrimenting endCnt: " + endCnt.ToString());
                }

            }

            // Do we need an extra  drop?
            int lastSample = -1;
            if ((pointsInterval * pointsToAdd) == endCnt) lastSample = endCnt - 1;

            //Loop and remove points
            int cnt = 1;
            //logwriter.WriteLine("Removing SAMPLE loop from " + "0" + " to " + newEndTime);
            //for (int i = newStartTime; i <= newEndTime; i++)
            while (startCnt < endCnt)
            {
                if (cnt == pointsInterval || startCnt == lastSample)
                {
                    // Add new sample
                    // Find midpoint between 2 points
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
                    //tempSamples.Add(newSample);
                    thisNewRide.RIDE.SAMPLES.Add(newSample);
                    //logwriter.WriteLine("Adding = " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " (SECS), at KM " + newdistanceTravelled.ToString());
                    thisNewRideCnt += 1;
                    cnt = 1;
                }
                else
                {
                    //if (startCnt != lastSample)
                    //{
                    // Move sample to new ride
                    //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    //logwriter.WriteLine("CNT: " + startCnt.ToString() + " Moving record: " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[oldRideCnt].KM);
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                    cnt++;
                }

                startCnt++;
            } // END of removing for loop
            // Move last point from old route to new route
            //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
            CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
            //logwriter.WriteLine("Moving LAST record: " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[oldRideCnt].KM);
            thisNewRideCnt += 1;
            thisOldRideCnt++;
        }
        // ***** addPointsToNewRide ENDS

        public void RemovePointsFromNewRide(int videoTime, int startTime, int endTime, ref int thisOldRideCnt, ref int thisNewRideCnt,GoldenCheetahRide thisOldRide, ref GoldenCheetahRide thisNewRide )
        {
            // Running total of new video in seconds
            //videoIndex += videoTime;

            // Init vars
            int pointsToRemove = -1;
            int startCnt = 0;
            int endCnt = 0;

            // If this is not the first record we need to add a second to the times
            if (startTime > 0)
            {
                endCnt = (endTime - startTime) + 1;
                videoTime += 1;
            }
            else
            {
                endCnt = endTime;
            }

            //logwriter.WriteLine("Start marker = {0}, {4}, -- end marker = {1}, {5}, -- video time = {2}, ride time = {3}", startTime, endTime, videoTime, endCnt, fromKM, toKM);

            //Calculate the number of points to remove and the remove interval
            pointsToRemove = Math.Abs(videoTime - endCnt);
            int pointsInterval = -99;
            if (pointsToRemove == 1)
            {
                pointsInterval = (int)Math.Round(endCnt / 2.0);
            }
            else
            {
                pointsInterval = (int)Math.Round(Math.Abs(endCnt / (pointsToRemove + 1.0)));
            }
            if (pointsInterval < 1) pointsInterval = 1;

            if (pointsToRemove == 0)
            {
                pointsToRemove = 1;
                pointsInterval = endCnt / 2;
            }

            ////logwriter.WriteLine("Removing " + pointsToRemove.ToString() + " points from the ride.");
            //logwriter.WriteLine("REMOVING --- Points interval:" + pointsInterval.ToString() + " points to REMOVE: " + pointsToRemove.ToString());

            //Calculate distance and avg speed for the full segment
            GeoLoc startpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[startTime].LAT, thisOldRide.RIDE.SAMPLES[startTime].LON);
            GeoLoc endpoint = new GeoLoc(thisOldRide.RIDE.SAMPLES[endTime].LAT, thisOldRide.RIDE.SAMPLES[endTime].LON);
            GeoLocMath geoLocMath = new GeoLocMath();
            double distanceTravelled = geoLocMath.CalculateDistanceBetweenGeoLocations(startpoint, endpoint);
            // Need to use video time to derive video speed
            double videoSpeed = ((distanceTravelled * 1000) / videoTime) * 3.6; //kph

            if (thisNewRide.RIDE.SAMPLES.Count() == 0)
            {
                // This is the first record.
                // Move initial point from old route to new route
                //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
                CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                //logwriter.WriteLine("Moving FIRST (INIT) record: " + thisOldRideCnt.ToString() + " to " + newRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KM);
                thisNewRideCnt += 1;
                thisOldRideCnt++;
                startCnt++;
            }
            else
            {   //Check if point exists in new route before adding it
                if (Math.Abs(thisNewRide.RIDE.SAMPLES[thisNewRideCnt - 1].KM - thisOldRide.RIDE.SAMPLES[thisOldRideCnt - 1].KM) > 0.01)
                {
                    // Move initial point from old route to new route
                    //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    //logwriter.WriteLine("Moving FIRST record: " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KM);
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                }
                else
                {
                    //We moved the first point manually
                    startCnt++;
                    endCnt--;
                    //logwriter.WriteLine("FIRST record EXISTS, increamenting startCnt: " + startCnt.ToString() + " decrimenting endCnt: " + endCnt.ToString());
                }

            }

            // Do we need an extra  drop?
            int lastSample = -1;
            if ((pointsInterval * pointsToRemove) == endCnt) lastSample = endCnt - 1;
            int pointsLeftToRemove = pointsToRemove;
            //Loop and remove points
            int cnt = 1;
            while (startCnt < endCnt)
            {
                if (cnt == pointsInterval || startCnt == lastSample)
                {
                    // Skip sample to simulate removing extra sample

                    // Don't remove more than pointsToRemove
                    if (pointsLeftToRemove > 0)
                    {
                        //logwriter.WriteLine("CNT: " + startCnt.ToString() + " Removing original sample: " + thisOldRideCnt.ToString());
                        thisOldRideCnt += 1;
                        cnt = 1;
                        pointsLeftToRemove--;
                    }
                    else
                    {
                        //logwriter.WriteLine("CNT: " + startCnt.ToString() + " SKIPPING REMOVE and MOVING");
                        // Move sample to new ride
                        //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
                        CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                        //logwriter.WriteLine("CNT: " + startCnt.ToString() + " Moving record: " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KM);
                        thisNewRideCnt += 1;
                        thisOldRideCnt++;
                        cnt++;
                    }
                    //thisOldRideCnt += 1;
                }
                else
                {
                    // Move sample to new ride
                    //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
                    CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
                    //logwriter.WriteLine("CNT: " + startCnt.ToString() + " Moving record: " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KM);
                    thisNewRideCnt += 1;
                    thisOldRideCnt++;
                    cnt++;
                }

                startCnt++;
            } // END of removing for loop
            // Move last point from old route to new route
            //thisNewRide.RIDE.SAMPLES.Add(thisOldRide.RIDE.SAMPLES[thisOldRideCnt]);
            CopySampleToNewlist(thisOldRide.RIDE.SAMPLES[thisOldRideCnt], ref thisNewRide);
            //logwriter.WriteLine("Moving LAST record: " + thisOldRideCnt.ToString() + " to " + thisNewRideCnt.ToString() + " at KM " + thisOldRide.RIDE.SAMPLES[thisOldRideCnt].KM);
            thisNewRideCnt += 1;
            thisOldRideCnt++;
        }

        //private void AddSampleToNewList (SAMPLE srcSample, int index, ref GoldenCheetahRide destination, double distanceTravelled, double speed)
        //{
        //    SAMPLE newSample = new SAMPLE()
        //    {
        //        SECS = 999, //insertPosition,
        //        KM = distanceTravelled,
        //        KPH = speed,
        //        ALT = (destination.RIDE.SAMPLES[index].ALT + destination.RIDE.SAMPLES[index - 1].ALT) / 2,
        //        LAT = srcSample.LAT,
        //        LON = srcSample.LON,
        //        SLOPE = -99
        //    };
        //    // Insert new point in ride
        //    destination.RIDE.SAMPLES.Add(newSample);
        //}

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
    }
}
