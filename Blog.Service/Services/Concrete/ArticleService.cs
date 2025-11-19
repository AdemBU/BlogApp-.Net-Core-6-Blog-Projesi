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
            var imageId = Guid.Parse("487F2D55-22DE-46DD-BA51-44920C3BEC7A");

            var article = new Article(articleAddDto.Title, articleAddDto.Content, userId, articleAddDto.CategoryId, imageId);

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

        public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
        {
            //Veritabanındaki silinmemiş tüm makaleleri, kategori bilgisiyle birlikte liste olarak getir.
            var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category);
            var map = mapper.Map<ArticleDto>(article);  //bu listeyi ArticleDto tipine dönüştürür.
            return map;
        }

        public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
        {
            var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category);

            article.Title = articleUpdateDto.Title;
            article.Content = articleUpdateDto.Content;
            article.CategoryId = articleUpdateDto.CategoryId;

            await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await _unitOfWork.SaveChangesAsync();

            return article.Title;
        }

        public async Task<string> SafeDeletedArticleAsync(Guid articleId)
        {
            var article = await _unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);

            article.IsDeleted = true;
            article.DeletedDate = DateTime.Now;

            await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await _unitOfWork.SaveChangesAsync();

            return article.Title;
        }
    }
}
