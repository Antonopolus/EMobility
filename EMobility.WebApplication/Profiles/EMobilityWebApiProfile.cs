using AutoMapper;
using EMobility.Data;
using EMobility.WebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApi.Profiles
{
    public class EMobilityWebApiProfile : Profile
    {

        public EMobilityWebApiProfile()
        {
            CreateMap<ChargingPoint, ChargingPointReadDto>();
            CreateMap<ChargingPointCreateDto, ChargingPoint>();
        }

    }
}
