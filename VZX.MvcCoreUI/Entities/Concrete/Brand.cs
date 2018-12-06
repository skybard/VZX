using System.Collections.Generic;
using VZX.MvcCoreUI.Entities.Abstract;

namespace VZX.MvcCoreUI.Entities.Concrete
{
    public class Brand : IEntity
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public List<Product> Products { get; set; }
    }
}
