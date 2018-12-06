using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal : IBrandDal
    {
        private readonly VZXDbContext _context;

        public EfBrandDal(VZXDbContext context)
        {
            _context = context;
        }

        public void Add(Brand entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Delete(Brand entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public Brand Get(Expression<Func<Brand, bool>> filter)
        {
            return _context.Set<Brand>().SingleOrDefault(filter);
        }

        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            return filter == null ?
                _context.Set<Brand>().ToList() :
                _context.Set<Brand>().Where(filter).ToList();
        }

        public void Update(Brand entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
