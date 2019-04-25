using System;
using AutoMapper;
using MongoDB.Driver.GeoJsonObjectModel;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.DTOs;

namespace ApplicationServices.Config
{
    public class ParkingSpaceMappingProfile : Profile
    {
        public ParkingSpaceMappingProfile()
        {
            CreateMap<Point, GeoJsonPoint<GeoJson2DGeographicCoordinates>>()
                .ConvertUsing<LocationConverter>();

            CreateMap<ParkingSpace, ParkingSpaceViewModel>()
                .ForMember(d => d.ParkingSpaceId, s => s.MapFrom(p => p.Id))
                .ForMember(d => d.OwnerId, s => s.MapFrom(p => p.OwnerId))
                .ForMember(d => d.Title, s => s.MapFrom(p => p.Description.Title))
                .ForMember(d => d.Description, s => s.MapFrom(p => p.Description.Description))
                .ForMember(d => d.ImageURL, s => s.MapFrom(p => p.Description.ImageURL))
                .ForMember(d => d.Street, s => s.MapFrom(p => p.Address.Street))
                .ForMember(d => d.City, s => s.MapFrom(p => p.Address.City))
                .ForMember(d => d.State, s => s.MapFrom(p => p.Address.State))
                .ForMember(d => d.Zip, s => s.MapFrom(p => p.Address.Zip))
                .ForMember(d => d.Location, s => s.MapFrom(src => src.Address.Location))
                .ForMember(d => d.HourlyRate, s => s.MapFrom(p => p.BookingRate.HourlyRate))
                .ForMember(d => d.DailyRate, s => s.MapFrom(p => p.BookingRate.DailyRate))
                .ForMember(d => d.IsVisible, s => s.MapFrom(p => p.Availability.IsVisible))
                .ForMember(d => d.Monday, s => s.MapFrom(p => p.Availability.Monday))
                .ForMember(d => d.Tuesday, s => s.MapFrom(p => p.Availability.Tuesday))
                .ForMember(d => d.Wednesday, s => s.MapFrom(p => p.Availability.Wednesday))
                .ForMember(d => d.Thursday, s => s.MapFrom(p => p.Availability.Thursday))
                .ForMember(d => d.Friday, s => s.MapFrom(p => p.Availability.Friday))
                .ForMember(d => d.Saturday, s => s.MapFrom(p => p.Availability.Saturday))
                .ForMember(d => d.Sunday, s => s.MapFrom(p => p.Availability.Sunday));
        }
    }
}
