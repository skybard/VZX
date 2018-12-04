using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {

        private readonly VZXDbContext _context;

        public EfProductDal(VZXDbContext context)
        {
            _context = context;
        }

        public void Add(Product entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            return _context.Set<Product>().SingleOrDefault(filter);
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return filter == null ?
                _context.Set<Product>().ToList() :
                _context.Set<Product>().Where(filter).ToList();
        }

        public void Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
