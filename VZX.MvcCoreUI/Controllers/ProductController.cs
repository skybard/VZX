using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductServices _productManager;
        private readonly IBrandServices _brandManager;

        public ProductController(IHostingEnvironment hostingEnvironment, IProductServices productManager, IBrandServices brandManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _productManager = productManager;
            _brandManager = brandManager;
        }

        public IActionResult Create()
        {
            var brands = _brandManager.GetAll();

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

            //var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"images", productViewModel.ImageFile.GetFilename());
            var path = Path.Combine(_hostingEnvironment.WebRootPath, @"images", productViewModel.ImageFile.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await productViewModel.ImageFile.CopyToAsync(stream);
            }

            productViewModel.Product.ImgURL = Path.Combine(@"images", productViewModel.ImageFile.GetFilename());

            _productManager.Add(productViewModel.Product);

            return RedirectToAction("Index");
        }

        [Route("urun-listele")]
        public IActionResult Index(string SearchProductName)
        {
            HttpContext.Session.Set("BMW", new Brand() { BrandId = 2, BrandName = "BMW" });

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
                BrandName = p.Brand.BrandName
            }).ToList();

            return View(models);
        }

        public JsonResult SessionList()
        {
            return Json(HttpContext.Session.Get<Product>("BMW"));
        }
    }
}