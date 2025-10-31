using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Entity.Entities;

namespace Blog.Service.AutoMapper.Articles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<ArticleDto, Article>().ReverseMap();
        }
    }
}
