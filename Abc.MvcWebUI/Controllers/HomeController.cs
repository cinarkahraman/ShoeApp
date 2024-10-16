using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;

namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();

        // GET: Home
        public ActionResult Index()
        {
            var urunler = _context.Products
                .Where(i => i.IsHome && i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Subtitle = i.Subtitle,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image,
                    genderId = i.genderId,
                    markaId = i.markaId,
                    CategoryId = i.CategoryId
                }).ToList();

            return View(urunler);
        }

        public ActionResult Details(int id)
        {
            return View(_context.Products.Where(i => i.Id == id).FirstOrDefault());
        }

        public ActionResult List(int? id, string searchString)
        {
            var urunler = _context.Products
                .Where(i => i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Subtitle = i.Subtitle,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image ?? "2fb6ff59-aca1-4f1f-a836-0888d4f119a6.jpg",
                    genderId = i.genderId,
                    markaId = i.markaId,
                    CategoryId = i.CategoryId
                }).AsQueryable();

            if (id != null)
            {
                urunler = urunler.Where(i => i.CategoryId == id);
            }
            // Arama işlevi
            if (!string.IsNullOrEmpty(searchString))
            {
                urunler = urunler.Where(i => i.Name.Contains(searchString) || i.Description.Contains(searchString));
            }

            return View(urunler.ToList());
        }

        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }


        public ActionResult Women(int? categoryId, string sortBy)
        {
            var urunler = _context.Products
               .Where(i => i.IsApproved)
               .Select(i => new ProductModel()
               {
                   Id = i.Id,
                   Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                   Subtitle = i.Subtitle,
                   Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                   Price = i.Price,
                   Stock = i.Stock,
                   Image = i.Image ?? "2fb6ff59-aca1-4f1f-a836-0888d4f119a6.jpg",
                   genderId = i.genderId,
                   markaId = i.markaId,
                   CategoryId = i.CategoryId
               }).AsQueryable();

            string selectedCategoryName = "All ";

           
            if (categoryId != null)
            {
                var selectedCategory = _context.Categories.Find(categoryId);
                if (selectedCategory != null)
                {
                    selectedCategoryName = selectedCategory.Name;
                }
            }


            IQueryable<Product> womenProducts = _context.Products
                .Where(p => p.genderId == 1 && (categoryId == null || p.CategoryId == categoryId));

            switch (sortBy)
            {
                case "asc":
                    womenProducts = womenProducts.OrderBy(p => p.Price);
                    break;
                case "desc":
                    womenProducts = womenProducts.OrderByDescending(p => p.Price);
                    break;
                default:
                    womenProducts = womenProducts.OrderBy(p => p.Price);
                    break;
            }
            ViewBag.SelectedCategory = selectedCategoryName;
            ViewBag.CategoryId = categoryId;

            return View(womenProducts.ToList());
        }

        public ActionResult Kids(String sort)
        {
            IQueryable<Product> kids = _context.Products.Where(p => p.genderId == 3);
            switch (sort)
            {
                case "asc":
                    kids = kids.OrderBy(p => p.Price);
                    break;
                case "desc":
                    kids = kids.OrderByDescending(p => p.Price);
                    break;
                default:
                    kids = kids.OrderBy(p => p.Price);
                    break;
            }
            return View(kids.ToList());
        }

        public ActionResult Men(int? categoryId, string sortBy)

        {
            string selectedCategoryName = "All ";




            if (categoryId != null)
            {
                var selectedCategory = _context.Categories.Find(categoryId);
                if (selectedCategory != null)
                {
                    selectedCategoryName = selectedCategory.Name;
                }
            }

            IQueryable<Product> menProducts = _context.Products
                .Where(p => p.genderId == 2 && (categoryId == null || p.CategoryId == categoryId));

            switch (sortBy)
            {
                case "asc":
                    menProducts = menProducts.OrderBy(p => p.Price);
                    break;
                case "desc":
                    menProducts = menProducts.OrderByDescending(p => p.Price);
                    break;
                default:
                    menProducts = menProducts.OrderBy(p => p.Price);
                    break;
            }
            ViewBag.SelectedCategory = selectedCategoryName;
            ViewBag.CategoryId = categoryId;

            return View(menProducts.ToList());
        }

    }
    [AdminRedirect]
    public class AdminRedirectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (user.Identity.IsAuthenticated && user.IsInRole("admin"))
            {
                filterContext.Result = new RedirectResult("/Order");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
