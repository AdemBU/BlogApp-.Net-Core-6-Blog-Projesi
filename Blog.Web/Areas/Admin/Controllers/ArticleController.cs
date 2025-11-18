using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<Article> _validator;

        public IValidator<Article> Validator { get; }

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var article = await _articleService.GetAllArticleWithCategoryNonDeletedAsync();
            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddDto { Categories = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            var map = _mapper.Map<Article>(articleAddDto);
            var result = await _validator.ValidateAsync(map);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                var categories = await _categoryService.GetAllCategoriesNonDeleted();
                return View(new ArticleAddDto { Categories = categories });
            }
            else
            {
                await _articleService.CreateArticleAsync(articleAddDto);
                return RedirectToAction("Index", "Article", new { area = "Admin" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid articleId)
        {
            var article = await _articleService.GetArticleWithCategoryNonDeletedAsync(articleId);
            var categories = await _categoryService.GetAllCategoriesNonDeleted();

            var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
        {
            var map = _mapper.Map<Article>(articleUpdateDto);
            var result = await _validator.ValidateAsync(map);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
            }
            else
            {
                await _articleService.UpdateArticleAsync(articleUpdateDto);
            }

            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);
        }

        public async Task<IActionResult> Delete(Guid articleId)
        {
            await _articleService.SafeDeletedArticleAsync(articleId);
            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
    }
}
