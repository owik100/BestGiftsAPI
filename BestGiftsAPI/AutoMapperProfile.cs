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
                .ForPath(dest => dest.CommentsDTO.Items, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.CategoriesDTO, opt => opt.Ignore())
                .ForMember(dest => dest.ImageContentB64, opt => opt.MapFrom(src=> src.ImageContent != null ? Convert.ToBase64String(src.ImageContent) : null));

            CreateMap<Category, CategoryDTO>()
                 .ForMember(dest => dest.GiftIdeaCategoryDTO, opt => opt.MapFrom(src => src.GiftIdeaCategory));

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.GiftIdeaDTO, opt => opt.MapFrom(src => src.GiftIdea));

            CreateMap<GiftIdeaCategory, CategoryDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));



            CreateMap<GiftIdeaDTO, GiftIdea>()
              .ForPath(dest => dest.Comments, opt => opt.MapFrom(src => src.CommentsDTO.Items))
              .ForMember(dest => dest.GiftIdeaCategory, opt => opt.Ignore())
              .ForMember(dest => dest.ImageContent, opt => opt.MapFrom(src => src.ImageContentB64 != null ? Convert.FromBase64String(src.ImageContentB64) : null));

            CreateMap<CommentDTO, Comment>()
               .ForMember(dest => dest.GiftIdea, opt => opt.MapFrom(src => src.GiftIdeaDTO));


        }
    }
}
