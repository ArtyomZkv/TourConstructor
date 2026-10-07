using System;
using System.Collections.Generic;
using System.Text;

namespace TourConstructor.Domain.ValueObjects
{
    public record Coordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Coordinates(double latitude, double longitude)
        {
            if (double.IsNaN(latitude) || latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude), "Широта должна быть в диапазоне от -90 до 90");
            if (double.IsNaN(longitude) || longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude), "Долгота должна быть в диапазоне от -180 до 180");
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
