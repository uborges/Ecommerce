using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models; 
using Ecommerce.Repositories; 

namespace Ecommerce.Controllers 
{
    public class ProductsController : Controller
    {
        
        private static readonly ProductRepository _repository = new ProductRepository();

        public IActionResult Index(string searchCategory)
        {
            List<Product> products;

            if (!string.IsNullOrEmpty(searchCategory))
            {
                products = _repository.GetByCategory(searchCategory);
            }
            else
            {
                products = _repository.GetAll();
            }

            ViewBag.CurrentFilter = searchCategory;

            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Price,Stock,Category")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Add(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Price,Stock,Category")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(product);
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(product);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}