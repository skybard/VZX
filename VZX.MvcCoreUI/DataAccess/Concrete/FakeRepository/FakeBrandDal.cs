using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.DataAccess.Concrete.FakeRepository
{
    public class FakeBrandDal : IBrandDal
    {
        private static readonly List<Brand> _context = new List<Brand>
            {
                    new Brand{ BrandId=1, BrandName="Mercedes"},
                    new Brand{ BrandId=2, BrandName="BMW"},
                    new Brand{ BrandId=3, BrandName="Volkswagen"},
                    new Brand{ BrandId=4, BrandName="Honda"},
                    new Brand{ BrandId=5, BrandName="Volvo"}
            };

        public void Add(Brand entity)
        {
            entity.BrandId = _context.Count() + 1;
            _context.Add(entity);
        }

        public void Delete(Brand entity)
        {
            _context.Remove(entity);
        }

        public Brand Get(Expression<Func<Brand, bool>> filter)
        {
            return _context.SingleOrDefault(filter.Compile());
        }

        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            return filter == null ?
                    _context.ToList() :
                    _context.Where(filter.Compile()).ToList();
        }

        public void Update(Brand entity)
        {
            var index = _context.FindIndex(p => p.BrandId == entity.BrandId);
            _context[index] = entity;
        }
    }
}
