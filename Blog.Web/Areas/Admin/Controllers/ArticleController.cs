using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
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
            await _articleService.CreateArticleAsync(articleAddDto);
            RedirectToAction("Index", "Article", new { area = "Admin" });

            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddDto { Categories = categories });
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
            await _articleService.UpdateArticleAsync(articleUpdateDto);

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
