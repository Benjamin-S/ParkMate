using System;
using AutoMapper;
using MongoDB.Driver.GeoJsonObjectModel;
using ParkMate.ApplicationCore.ValueObjects;

namespace ApplicationServices.Config
{
    public class LocationConverter
        : ITypeConverter<Point, GeoJsonPoint<GeoJson2DGeographicCoordinates>>
    {
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Convert(
            Point source,
            GeoJsonPoint<GeoJson2DGeographicCoordinates> destination, ResolutionContext context)
        {
            return new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                new GeoJson2DGeographicCoordinates(source.Longitude, source.Latitude));
        }
    }
}
