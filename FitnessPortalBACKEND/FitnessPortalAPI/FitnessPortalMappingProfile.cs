using AutoMapper;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Articles;

namespace FitnessPortalAPI
{
    public class FitnessPortalMappingProfile : Profile
    {
        public FitnessPortalMappingProfile()
        {
            CreateMap<CreateArticleDto, Article>()
                .ForMember(dest => dest.DateOfPublication, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Article, ArticleDto>();
            CreateMap<UpdateArticleDto, Article>();
        }
    }
}
