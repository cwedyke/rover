using System;
using System.Collections.Generic;

namespace Rover.Models
{
    public class RoverModel
    {
        public int GridSize { get; set; }
        public Coordinate RoverPosition { get; set; }
        public IEnumerable<Coordinate> Components { get; set; }

        public struct Coordinate
        {
            private readonly int latitude;
            private readonly int longitude;

            public int Latitude { get { return latitude; } }
            public int Longitude { get { return longitude; } }

            public Coordinate(int longitude, int latitude)
            {
                this.longitude = longitude;
                this.latitude = latitude;
            }

            public override string ToString()
            {
                return string.Format("{0},{1}", Longitude, Latitude);
            }

            public bool Equals(Coordinate other)
            {
                return Latitude == other.Latitude && Longitude == other.Longitude;
            }
        }

        /// <summary>
        /// Convert string (ex. "1,1") to a coordinate.
        /// </summary>
        public static Coordinate ToCoordinate(string coord)
        {
            var vals = coord.Split(',');

            if (vals == null || vals.Length != 2) //todo validate integer 
                throw new Exception("Invalid string format.");

            return new Coordinate(Convert.ToInt32(vals[0]), Convert.ToInt32(vals[1]));
        }
    }
}
