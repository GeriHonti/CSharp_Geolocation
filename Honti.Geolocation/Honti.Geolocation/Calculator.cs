using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honti.Geolocation
{
    public enum DistanceType
    {
        Miles = 0,
        Kilometers = 1
    }

    public class PositionCalculator : ICalculateBearing, ICalculateDistance, ICalculateRhumbBearing,ICalculateRhumbDistance
    {
        private readonly AngleConverter _angleConverter;

        public PositionCalculator()
        {
            _angleConverter = new AngleConverter();
        }

        public static double EarthRadiusInKilometers
        {
            get { return 6367.0; }
        }

        public static double EarthRadiusInMiles
        {
            get { return 3956.0; }
        }

        public double CalculateBearing(Position position1, Position position2)
        {
            var lat1 = _angleConverter.ConvertDegreesToRadians(position1.Latitude);
            var lat2 = _angleConverter.ConvertDegreesToRadians(position2.Latitude);
            var dLon = _angleConverter.ConvertDegreesToRadians(position2.Longitude) -
                          _angleConverter.ConvertDegreesToRadians(position1.Longitude);

            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            var brng = Math.Atan2(y, x);

            return (_angleConverter.ConvertRadiansToDegrees(brng) + 360) % 360;
        }

        /// <summary>
        /// Calculating distance between 2 points on the map. 
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <param name="distanceType"></param>
        /// <returns></returns>
        public double CalculateDistance(Position position1, Position position2, DistanceType distanceType)
        {
            var R = (distanceType == DistanceType.Miles) ? EarthRadiusInMiles : EarthRadiusInKilometers;
            var dLat = _angleConverter.ConvertDegreesToRadians(position2.Latitude) -
                          _angleConverter.ConvertDegreesToRadians(position1.Latitude);
            var dLon = _angleConverter.ConvertDegreesToRadians(position2.Longitude) -
                          _angleConverter.ConvertDegreesToRadians(position1.Longitude);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(_angleConverter.ConvertDegreesToRadians(position1.Latitude)) *
                       Math.Cos(_angleConverter.ConvertDegreesToRadians(position2.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = c * R;
            return Math.Round(distance, 2);
        }

        public double CalculateRhumbBearing(Position position1, Position position2)
        {
            var lat1 = _angleConverter.ConvertDegreesToRadians(position1.Latitude);
            var lat2 = _angleConverter.ConvertDegreesToRadians(position2.Latitude);
            var dLon = _angleConverter.ConvertDegreesToRadians(position2.Longitude - position1.Longitude);

            var dPhi = Math.Log(Math.Tan(lat2 / 2 + Math.PI / 4) / Math.Tan(lat1 / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI) dLon = (dLon > 0) ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            var brng = Math.Atan2(dLon, dPhi);

            return (_angleConverter.ConvertRadiansToDegrees(brng) + 360) % 360;
        }

        public double CalculateRhumbDistance(Position position1, Position position2, DistanceType distanceType)
        {
            var r = (distanceType == DistanceType.Miles) ? EarthRadiusInMiles : EarthRadiusInKilometers;
            var lat1 = _angleConverter.ConvertDegreesToRadians(position1.Latitude);
            var lat2 = _angleConverter.ConvertDegreesToRadians(position2.Latitude);
            var dLat = _angleConverter.ConvertDegreesToRadians(position2.Latitude - position1.Latitude);
            var dLon = _angleConverter.ConvertDegreesToRadians(Math.Abs(position2.Longitude - position1.Longitude));

            var dPhi = Math.Log(Math.Tan(lat2 / 2 + Math.PI / 4) / Math.Tan(lat1 / 2 + Math.PI / 4));
            var q = Math.Cos(lat1);
            if (dPhi != 0) q = dLat / dPhi; // E-W line gives dPhi=0
            // if dLon over 180° take shorter rhumb across 180° meridian:
            if (dLon > Math.PI) dLon = 2 * Math.PI - dLon;
            var dist = Math.Sqrt(dLat * dLat + q * q * dLon * dLon) * r;

            return dist;
        }
    }
}
