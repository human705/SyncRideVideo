using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperFunctionLibrary
{
    public class GeoLocMath
    {
        //private GeoLoc point1 { get; set; }
        //private GeoLoc point2 { get; set; }

        private GeoLoc _point1;
        public GeoLoc point1
        {
            get { return _point1; }
            set { _point1 = value; }
        }

        private GeoLoc _point2;

        public GeoLoc point2
        {
            get { return _point2; }
            set { _point2 = value; }
        }
        //public GeoLocMath()
        //{
        //}
        public GeoLocMath(GeoLoc p1, GeoLoc p2)
        {
            this._point1 = p1;
            this._point2 = p2;
        }
        
        static readonly double radius = 6371; // earth's mean radius in km
        static public ConvertDegressRadians convertDegressRadians = new ConvertDegressRadians();

        /// <summary>
        /// Calculate the distance between two Geo Locations.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>Returns the distance in kilometers</returns>
        public double CalculateDistanceBetweenGeoLocations(GeoLoc startPoint, GeoLoc endPoint)
        {
            double lat1 = convertDegressRadians.DegToRad(startPoint.Latitude);
            double lon1 = convertDegressRadians.DegToRad(startPoint.Longitude);
            double lat2 = convertDegressRadians.DegToRad(endPoint.Latitude);
            double lon2 = convertDegressRadians.DegToRad(endPoint.Longitude);
            double deltaLat = lat2 - lat1;
            double deltaLon = lon2 - lon1;
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (radius * c);
        }

        public GeoLoc CalculateMidPoint(GeoLoc startPoint, GeoLoc endPoint)
        {
            double lat1 = convertDegressRadians.DegToRad(startPoint.Latitude);
            double lon1 = convertDegressRadians.DegToRad(startPoint.Longitude);

            double lat2 = convertDegressRadians.DegToRad(endPoint.Latitude);
            double lon2 = convertDegressRadians.DegToRad(endPoint.Longitude);

            double deltaLat = lat2 - lat1;
            double deltaLon = lon2 - lon1;

            double bx = Math.Cos(lat2) * Math.Cos(deltaLon);
            double by = Math.Cos(lat2) * Math.Sin(deltaLon);

            double lat3 = Math.Atan2(Math.Sin(lat1) + Math.Sin(lat2), Math.Sqrt((Math.Cos(lat1) + bx) * (Math.Cos(lat1) + bx) + by * by));
            double lon3 = lon1 + Math.Atan2(by, Math.Cos(lat1) + bx);

            GeoLoc myLoc = new GeoLoc(convertDegressRadians.RadToDeg(lat3), convertDegressRadians.RadToDeg(lon3));
            return myLoc;
        }

        public GeoLoc CalcMidPoint()
        {
            double lat1 = convertDegressRadians.DegToRad(_point1.Latitude);
            double lon1 = convertDegressRadians.DegToRad(_point1.Longitude);

            double lat2 = convertDegressRadians.DegToRad(_point2.Latitude);
            double lon2 = convertDegressRadians.DegToRad(_point2.Longitude);

            double deltaLat = lat2 - lat1;
            double deltaLon = lon2 - lon1;

            double bx = Math.Cos(lat2) * Math.Cos(deltaLon);
            double by = Math.Cos(lat2) * Math.Sin(deltaLon);

            double lat3 = Math.Atan2(Math.Sin(lat1) + Math.Sin(lat2), Math.Sqrt((Math.Cos(lat1) + bx) * (Math.Cos(lat1) + bx) + by * by));
            double lon3 = lon1 + Math.Atan2(by, Math.Cos(lat1) + bx);

            GeoLoc myLoc = new GeoLoc(convertDegressRadians.RadToDeg(lat3), convertDegressRadians.RadToDeg(lon3));
            return myLoc;
        }

        /// <summary>
        /// Calculate the distance between two Geo Locations.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>Returns the distance in kilometers</returns>
        public double CalcDistanceBetweenGeoLocations()
        {
            double lat1 = convertDegressRadians.DegToRad(_point1.Latitude);
            double lon1 = convertDegressRadians.DegToRad(_point1.Longitude);
            double lat2 = convertDegressRadians.DegToRad(_point2.Latitude);
            double lon2 = convertDegressRadians.DegToRad(_point2.Longitude);
            double deltaLat = lat2 - lat1;
            double deltaLon = lon2 - lon1;
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (radius * c);
        }

    }
}
