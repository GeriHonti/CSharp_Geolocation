using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honti.Geolocation
{
    public interface ICalculateBearing
    {
        double CalculateBearing(Position position1, Position position2);
    }

    public interface ICalculateDistance
    {
        double CalculateDistance(Position position1, Position position2, DistanceType distanceType1);
    }

    public interface ICalculateRhumbBearing
    {
        double CalculateRhumbBearing(Position position1, Position position2);
    }

    public interface ICalculateRhumbDistance
    {
        double CalculateRhumbDistance(Position position1, Position position2, DistanceType distanceType);
    }
}
