using System;
using AutoMapper;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.DTOs;

namespace ApplicationServices.Config
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(d => d.CustomerId, s => s.MapFrom(c => c.IdentityId))
                .ForMember(d => d.PhoneNumber, s => s.MapFrom(c => c.PhoneNumber))
                .ForMember(d => d.Email, s => s.MapFrom(c => c.Email))
                .ForMember(d => d.Name, s => s.MapFrom(c => c.Name))
                .ForMember(d => d.Vehicles, s => s.MapFrom(c => c.Vehicles))
                .ForMember(d => d.ParkingSpaces, s => s.MapFrom(c => c.ParkingSpaces))
                .ForMember(d => d.Bookings, s => s.MapFrom(c => c.Bookings));
        }
    }
}