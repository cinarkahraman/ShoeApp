using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Abc.MvcWebUI.Models
{
    public class ShippingDetails
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Address Title is required")]
        public string AdresBasligi { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string Sehir { get; set; }

        [Required(ErrorMessage = "District is required")]
        public string Semt { get; set; }

        [Required(ErrorMessage = "Neighborhood is required")]
        public string Mahalle { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        public string PostaKodu { get; set; }

    

    }  
}