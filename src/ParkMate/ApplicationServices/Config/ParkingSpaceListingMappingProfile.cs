using System;
using AutoMapper;
using MongoDB.Driver.GeoJsonObjectModel;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.DTOs;

namespace ApplicationServices.Config
{
    public class ParkingSpaceListingMappingProfile : Profile
    {
        public ParkingSpaceListingMappingProfile()
        {
            CreateMap<Point, GeoJsonPoint<GeoJson2DGeographicCoordinates>>()
                .ConvertUsing<LocationConverter>();

            CreateMap<ParkingSpace, ParkingSpaceListingDTO>()
                .ForMember(d => d.Title, s => s.MapFrom(src => src.Description.Title))
                .ForMember(d => d.Description, s => s.MapFrom(src => src.Description.Description))
                .ForMember(d => d.ImageURL, s => s.MapFrom(src => src.Description.ImageURL))
                .ForMember(d => d.HourlyRate, s => s.MapFrom(src => src.BookingRate.HourlyRate.Value))
                .ForMember(d => d.DailyRate, s => s.MapFrom(src => src.BookingRate.DailyRate.Value))
                .ForMember(d => d.ParkingSpaceId, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Location, s => s.MapFrom(src => src.Address.Location))
                .ForMember(d => d.IsListed, s => s.MapFrom(src => src.Availability.IsVisible));
        }
    }
}
