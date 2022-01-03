using AutoMapper;
using BestGiftsAPI.Entities;
using BestGiftsAPI.Models_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GiftIdea, GiftIdeaDTO>()
                .ForMember(dest => dest.CommentsDTO, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.GiftIdeaCategoryDTO, opt => opt.MapFrom(src => src.GiftIdeaCategory))
                .ForMember(dest => dest.ImageContentB64, opt => opt.MapFrom(src=> src.ImageContent != null ? Convert.ToBase64String(src.ImageContent) : null));

            CreateMap<Category, CategoryDTO>()
                 .ForMember(dest => dest.GiftIdeaCategoryDTO, opt => opt.MapFrom(src => src.GiftIdeaCategory));

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.GiftIdeaDTO, opt => opt.MapFrom(src => src.GiftIdea));

            CreateMap<GiftIdeaCategory, GiftIdeaCategoryDTO>()
                .ForMember(dest => dest.GiftIdeaDTO, opt => opt.MapFrom(src => src.GiftIdea))
                .ForMember(dest => dest.CategoryDTO, opt => opt.MapFrom(src => src.Category));

        }
    }
}
