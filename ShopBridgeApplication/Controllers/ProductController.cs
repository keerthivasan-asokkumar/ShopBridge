using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopBridgeApplication.Data;
using ShopBridgeApplication.Models;

namespace ShopBridgeApplication.Controllers
{
    public class ProductController : Controller
    {
        private ShopBridgeDbContext _dbContext;
        private readonly string ImageFolderPath = ""; 

        public ProductController(ShopBridgeDbContext dbContext)
        {
            _dbContext = dbContext;
            ImageFolderPath = @"C:\Users\keerthivasanak\source\repos\ShopBridgeApplication\ShopBridgeApplication\wwwroot";
        }

        public IActionResult Index()
        {
            var products = _dbContext.Product.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                string imageFileNameURL = UploadedFile(productViewModel);

                var product = MapProductViewModelToProductView(productViewModel, imageFileNameURL);

                _dbContext.Product.Add(product);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(productViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                string imageFileNameURL = UploadedFile(productViewModel);

                var product = MapProductViewModelToProductViewForEdit(productViewModel, imageFileNameURL);

                _dbContext.Product.Update(product);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(productViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            else if (id != product.ProductId)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Product.Remove(product);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
        }

        private string UploadedFile(ProductViewModel productViewModel)
        {
            string fileName = "";

            if (productViewModel.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(ImageFolderPath, "images");
                fileName = "_" + productViewModel.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productViewModel.ProductImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        private Product MapProductViewModelToProductView(ProductViewModel pvm, string filePath = "")
        {
            var product = new Product
            {
                ProductName = pvm.ProductName,
                Price = pvm.Price,
                Description = pvm.Description,
                UnitsAvailable = pvm.UnitsAvailable,
                ProductImage = string.IsNullOrEmpty(filePath) ? pvm.ProductImage.Name : filePath
            };

            return product;
        }

        private Product MapProductViewModelToProductViewForEdit(ProductViewModel pvm, string filePath = "")
        {
            var product = new Product
            {
                ProductId = pvm.ProductId,
                ProductName = pvm.ProductName,
                Price = pvm.Price,
                Description = pvm.Description,
                UnitsAvailable = pvm.UnitsAvailable,
                ProductImage = string.IsNullOrEmpty(filePath) ? pvm.ProductImage.Name : filePath
            };

            return product;
        }

        [NonAction]
        public SelectList CategoryList(string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

                list.Add(new SelectListItem()
                {
                    Text = "1",
                    Value = "Home"
                });
                list.Add(new SelectListItem()
                {
                    Text = "2",
                    Value = "Electronics"
                });
                list.Add(new SelectListItem()
                {
                    Text = "3",
                    Value = "Furniture"
                });
                list.Add(new SelectListItem()
                {
                    Text = "4",
                    Value = "Groceries"
                });
            return new SelectList(list, "Value", "Text");
        }
    }
}