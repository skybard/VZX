using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VZX.MvcCoreUI.Models
{
    public class ProductIndexViewModel
    {
        [Display(Name ="Ürün No")]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }

        [Display(Name = "Fiyat")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Stok")]
        public Int16 UnitsInStock { get; set; }

        [Display(Name = "Resim")]
        public string ImgURL { get; set; }

        [Display(Name = "Marka No")]
        public int BrandId { get; set; }

        [Display(Name = "Marka")]
        public string BrandName { get; set; }
    }
}
