using MachineTest.Models;
using MachineTest.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        // Constructor with dependency injection
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Product

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var totalCount = _productService.GetProductCount();  
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            var products = _productService.GetProducts(page, pageSize); // Get paginated products

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(products);
        }


        public ActionResult Create()
        {
            var categories = _categoryService.GetCategories();

            if (categories == null || !categories.Any())
            {
                ViewBag.Categories = new SelectList(Enumerable.Empty<SelectListItem>(), "CategoryId", "CategoryName");
            }
            else
            {
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            }

            return View();
        
    }


        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (product.ProductName!=null && product.CategoryId!=null)
            {
                _productService.CreateProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);
            if (product == null)
            {
                return View("Error");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (product.ProductName != null && product.CategoryId != null)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = _productService.GetProductById(id);
            if (product == null)
            {
                return View("Error");
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }

}
