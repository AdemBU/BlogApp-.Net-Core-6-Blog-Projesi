using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Article;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
        {
            var userId = Guid.Parse("0DD20017-ED70-471F-9701-926A8F764EF2");

            var article = new Article
            {
                Title = articleAddDto.Title,
                Content = articleAddDto.Content,
                CategoryId = articleAddDto.CategoryId,
                UserId = userId
            };

            await _unitOfWork.GetRepository<Article>().AddAsync(article);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync()
        {
            //Veritabanındaki silinmemiş tüm makaleleri, kategori bilgisiyle birlikte liste olarak getir.
            var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted, x => x.Category);
            var map = mapper.Map<List<ArticleDto>>(articles);  //bu listeyi ArticleDto tipine dönüştürür.
            return map;
        }
    }
}
