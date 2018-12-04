using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.Business.Abstract
{
    public interface IProductServices
    {
        List<Product> GetAll(Expression<Func<Product, bool>> filter = null);
        void Add(Product product);
    }
}
