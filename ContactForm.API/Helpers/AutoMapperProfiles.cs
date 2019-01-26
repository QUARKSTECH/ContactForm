using System.Collections.Generic;
using AutoMapper;
using ContactForm.API.Dtos;
using ContactForm.API.Models;

namespace ContactForm.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDetailDto>();
            CreateMap<UserDetailDto, User>();

            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();

            CreateMap<Enquiry, EnquiryDto>();
            CreateMap<EnquiryDto, Enquiry>()
            .ForMember(m => m.ExtraProperties, opt => opt
                    .MapFrom(u => u.ExtraProps.SerializeDictionary()))
            ;
        }
    }
}