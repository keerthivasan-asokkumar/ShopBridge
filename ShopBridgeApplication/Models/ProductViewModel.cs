using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeApplication.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required]
        public int Price { get; set; }

        [Display(Name = "Product Image")]
        [Required]
        public IFormFile ProductImage { get; set; }

        [Display(Name = "Units In Stock")]
        [Required]
        public int UnitsAvailable { get; set; }
    }
}
