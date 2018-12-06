using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VZX.MvcCoreUI.Business.Abstract;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.Business.Concrete
{
    public class BrandManager : IBrandServices
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add(Brand brand)
        {
            _brandDal.Add(brand);
        }

        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            return _brandDal.GetAll(filter);
        }
    }
}
