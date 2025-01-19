using MachineTest.Models;
using MachineTest.Service;
using Microsoft.AspNetCore.Mvc;

namespace MachineTest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

       
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (category.CategoryName!=null)
            {
                _categoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

    
        public ActionResult Edit(int id)
        {
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return View("Error");
            }
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (category.CategoryName != null)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        
        public ActionResult Delete(int id)
        {
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return View("Error");
            }
            return View(category);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return View("Error");
            }
            return View(category);
        }
    }

}
