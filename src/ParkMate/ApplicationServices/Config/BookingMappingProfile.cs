using System;
using AutoMapper;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.DTOs;

namespace ApplicationServices.Config
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<Booking, BookingViewModel>()
                .ForMember(d => d.BookingId, s => s.MapFrom(b => b.Id))
                .ForMember(d => d.CustomerId, s => s.MapFrom(b => b.CustomerId))
                .ForMember(d => d.ParkingSpace, s => s.MapFrom(b => b.ParkingSpace))
                .ForMember(d => d.Vehicle, s => s.MapFrom(b => b.Vehicle))
                .ForMember(d => d.Start, s => s.MapFrom(b => b.BookingInfo.Start))
                .ForMember(d => d.End, s => s.MapFrom(b => b.BookingInfo.End))
                .ForMember(d => d.Total, s => s.MapFrom(b => b.BookingInfo.Total))
                .ForMember(d => d.Rate, s => s.MapFrom(b => b.BookingInfo.Rate))
                .ForMember(d => d.BillingUnit, s => s.MapFrom(b => b.BookingInfo.BillingUnit.ToString()))
                .ForMember(d => d.Status, s => s.MapFrom(b => b.Status.ToString()))
                .ForMember(d => d.BookingUnits, s => s.MapFrom(b => b.BookingInfo.BookingUnits))
                .ForMember(d => d.BookingTime, s => s.MapFrom(b => b.BookingTime));
        }
    }
}
