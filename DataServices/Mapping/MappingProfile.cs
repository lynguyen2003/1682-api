using AutoMapper;
using Models.DTO.Request.Auth;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Source, Destination>();

            CreateMap<UserRegistrationDTO, Users>()
                    .ForMember(dest => dest.id, opt => opt.Ignore())
                    .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
