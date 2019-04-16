using System;
using AutoMapper;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.DTOs;

namespace ApplicationServices.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ParkingSpace, ParkingSpaceListingDTO>();
        }
    }
}
