using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.DataAccess.Concrete.FakeRepository
{
    public class FakeProductDal : IProductDal
    {
        private static readonly List<Product> _context = new List<Product>();

        public void Add(Product entity)
        {
            entity.ProductId = _context.Count() + 1;
            _context.Add(entity);
        }

        public void Delete(Product entity)
        {
            _context.Remove(entity);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            return _context.SingleOrDefault(filter.Compile());
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return filter == null ?
                    _context.ToList() :
                    _context.Where(filter.Compile()).ToList();
        }

        public void Update(Product entity)
        {
            var index = _context.FindIndex(p => p.ProductId == entity.ProductId);
            _context[index] = entity;
        }
    }
}
