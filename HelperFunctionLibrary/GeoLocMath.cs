using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperFunctionLibrary
{
    public class GeoLocMath
    {
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

    }
}
