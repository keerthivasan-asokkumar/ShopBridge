using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridgeApplication.Controllers;
using ShopBridgeApplication.Data;
using ShopBridgeApplication.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeTests
{
    [TestClass]
    public class ProductControllerTest 
    {

        private Mock<ShopBridgeDbContext> _dbContext;
        private ProductController productController;

        IFormFile mockImage = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a test file")), 0, 0, "Data", "test.jpg");

        private Product _table1 = new Product()
        {
            ProductName = "test1",
            Description = "test product",
            Price = 1000,
            UnitsAvailable = 10,
            ProductImage = "",
            ProductId = 1001
        };

        [TestInitialize]
        public void Test()
        {
            _dbContext = new Mock<ShopBridgeDbContext>();
            _dbContext.Setup(p => p.Set<Product>().Add(_table1));
            productController = new ProductController(_dbContext.Object);
        }

        private ProductViewModel GivenProductDetails()
        {

            var product = new ProductViewModel()
            {
                ProductId = 1001,
                ProductName = "test1",
                Description = "test product",
                Price = 1000,
                UnitsAvailable = 10,
                ProductImage = mockImage
            };

            return product;
        }

        public ActionResult ProductCreate(ProductViewModel product)
        {
            var task = productController.Create(product);
            var result = task.GetAwaiter().GetResult();
            return result;
        }

        public ActionResult ProductEdit(ProductViewModel product)
        {
            var task = productController.Edit(product);
            var result = task.GetAwaiter().GetResult();
            return result;
        }

        public ActionResult ProductDetails(ProductViewModel product)
        {
            return productController.Details(product.ProductId);
        }

        public ActionResult ProductDelete(ProductViewModel product)
        {
            var task = productController.DeleteAsync(product.ProductId);
            var result = task.GetAwaiter().GetResult();
            return result;
        }

        [TestMethod]
        public void ProductCreation()
        {
            var product = GivenProductDetails();
            var result = ProductCreate(product);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductEdit()
        {
            var product = GivenProductDetails();
            var result = ProductEdit(product);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductDetails()
        {
            var product = GivenProductDetails();
            var result = ProductDetails(product);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductDelete()
        {
            var product = GivenProductDetails();
            var result = ProductDelete(product);
            Assert.IsNotNull(result);
        }
    }
}
