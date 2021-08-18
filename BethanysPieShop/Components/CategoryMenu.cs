using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BethanysPieShop.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRespository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRespository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRespository.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
