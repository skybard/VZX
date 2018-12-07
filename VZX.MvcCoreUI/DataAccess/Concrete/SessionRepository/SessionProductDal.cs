using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.DataAccess.Concrete.FakeRepository;
using VZX.MvcCoreUI.Entities.Concrete;
using VZX.MvcCoreUI.ExtensionMethods;

namespace VZX.MvcCoreUI.DataAccess.Concrete.SessionRepository
{
    public class SessionProductDal : IProductDal
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private List<Product> _context;

        public SessionProductDal(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            if (_session.Get<List<Product>>("SEPET") == default(List<Product>))
            {
                _session.Set("SEPET", new List<Product>());
            }
        }

        public void Add(Product entity)
        {
            _context = _session.Get<List<Product>>("SEPET");
            entity.ProductId = _context.Count() + 1;
            _context.Add(entity);
            _session.Set("SEPET", _context);
        }

        public void Delete(Product entity)
        {
            _context = _session.Get<List<Product>>("SEPET");
            _context.Remove(entity);
            _session.Set("SEPET", _context);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            _context = _session.Get<List<Product>>("SEPET");
            return _context.SingleOrDefault(filter.Compile());
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            _context = _session.Get<List<Product>>("SEPET");

            var brands = new FakeBrandDal().GetAll();
            var products = filter == null ?
                            _context :
                            _context.Where(filter.Compile()).ToList();

            var models = (from productItem in products
                          join brandItem in brands
                          on productItem.BrandId equals brandItem.BrandId
                          select new Product
                          {
                              ProductId = productItem.ProductId,
                              ProductName = productItem.ProductName,
                              ImgURL = productItem.ImgURL,
                              UnitPrice = productItem.UnitPrice,
                              UnitsInStock = productItem.UnitsInStock,
                              BrandId = productItem.BrandId,
                              Brand = brandItem
                          }).ToList();

            return models;
        }

        public void Update(Product entity)
        {
            _context = _session.Get<List<Product>>("SEPET");
            var index = _context.FindIndex(p => p.ProductId == entity.ProductId);
            _context[index] = entity;
            _session.Set("SEPET", _context);
        }
    }
}
