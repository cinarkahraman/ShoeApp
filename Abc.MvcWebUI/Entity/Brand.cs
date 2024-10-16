using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
        public List<Gender> Genders { get; set; }
    }
}