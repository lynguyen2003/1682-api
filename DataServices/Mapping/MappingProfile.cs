using AutoMapper;
using Models.DTO.Auth;
using Models.DTO.Video;
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

            CreateMap<Videos, VideoDTO>();
            CreateMap<VideoCreateDTO, Videos>()
                    .ForMember(dest => dest.id, opt => opt.Ignore())
                    .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<VideoUpdateDTO, Videos>()
                    .ForMember(dest => dest.id, opt => opt.Ignore())
                    .ForMember(dest => dest.user_id, opt => opt.Ignore())
                    .ForMember(dest => dest.Timestamp, opt => opt.Ignore())
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserRegistrationDTO, Users>()
                    .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.phone_number, opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForMember(dest => dest.password_hash, opt => opt.MapFrom(src => src.Password));


        }
    }
}
