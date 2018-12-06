using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.Business.Abstract
{
    public interface IBrandServices
    {
        List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null);
        void Add(Brand product);
    }
}
