﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VZX.MvcCoreUI.Business.Abstract;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.Entities.Concrete;
using VZX.MvcCoreUI.Enums;
using VZX.MvcCoreUI.ExtensionMethods;
using VZX.MvcCoreUI.Models;

namespace VZX.MvcCoreUI.Controllers
{
    public class ProductController : Controller
    {

        private readonly IFileProvider _fileProvider;
        private readonly IProductServices _productManager;

        public ProductController(IFileProvider fileProvider, IProductServices productManager)
        {
            _fileProvider = fileProvider;
            _productManager = productManager;
        }

        public IActionResult Create()
        {
            var brands = new List<Brand>
            {
                    new Brand{ BrandId=1, BrandName="Mercedes"},
                    new Brand{ BrandId=2, BrandName="BMW"},
                    new Brand{ BrandId=3, BrandName="Volkswagen"},
                    new Brand{ BrandId=4, BrandName="Honda"},
                    new Brand{ BrandId=5, BrandName="Volvo"}
            };

            var brandListItems = brands.Select(s => new SelectListItem
            {
                Value = s.BrandId.ToString(),
                Text = s.BrandName
            }).ToList();

            var createProductViewModel = new ProductCreateViewModel()
            {
                Brands = brands,
                SelectBrandsItem = brandListItems
            };

            return View(createProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel productViewModel)
        {

            if (productViewModel.ImageFile == null || productViewModel.ImageFile.Length == 0)
                return Content("Resim seçilmedi");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", productViewModel.ImageFile.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await productViewModel.ImageFile.CopyToAsync(stream);
            }

            productViewModel.Product.ImgURL = $"~/images/{productViewModel.ImageFile.GetFilename()}";
            _productManager.Add(productViewModel.Product);

            return RedirectToAction("Index");
        }

        [Route("urun-listele")]
        public IActionResult Index(string SearchProductName)
        {
            var dbProducts = string.IsNullOrEmpty(SearchProductName) ?
                                    _productManager.GetAll() :
                                    _productManager.GetAll(s => s.ProductName.ToUpper().Contains(SearchProductName.ToUpper()));

            List<ProductIndexViewModel> models = dbProducts.Select(p => new ProductIndexViewModel()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                ImgURL = p.ImgURL,
                BrandId = p.BrandId,
                BrandName = ((BrandEnum)p.BrandId).ToString()
            }).ToList();

            return View(models);
        }
    }
}