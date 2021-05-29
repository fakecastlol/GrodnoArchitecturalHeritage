using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace Heritage.Services.Interfaces.Models.Construction.Location
{
    public class MyPoint : Point
    {
        [JsonConstructor]
        public MyPoint(double latitude, double longitude, int srid)
            : base(new NetTopologySuite.Geometries.Coordinate(longitude, latitude))
        {
            SRID = srid;
        }
    }
}
