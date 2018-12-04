using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.Models
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; }
        public List<Brand> Brands { get; set; }
        public List<SelectListItem> SelectBrandsItem { get; set; }
    }


}
