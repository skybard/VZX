using System;
using VZX.MvcCoreUI.Entities.Abstract;

namespace VZX.MvcCoreUI.Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16 UnitsInStock { get; set; }
        public string ImgURL { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
