using MyFirstMvcApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvcProject.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        [CustomAuthorize]
        public ActionResult productList()
        {
            return View();
        }

        [HttpGet]
        public JsonResult dataTable()
        {
            using (CommerceDbEntities dataBase = new CommerceDbEntities())
            {
                var productList = dataBase.Products.Include("Categories").Select(p =>
                    new
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Brand = p.Brand,
                        CategoryName = p.Category.CategoryName
                    }
                    ).ToList();

                return Json(productList, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult productDelete(int proId)
        {
            using (CommerceDbEntities dataBase = new CommerceDbEntities())
            {
                Product willDelete = dataBase.Products.Where(p => p.Id == proId).SingleOrDefault();
                dataBase.Products.Remove(willDelete);
                dataBase.SaveChanges();
                return RedirectToAction("productList");
            }
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult productUpdate(int proId)
        {
            using (CommerceDbEntities dataBase = new CommerceDbEntities())
            {
                dynamic model = new ExpandoObject();
                model.selectedProduct = dataBase.Products.Find(proId);
                model.listCategory = dataBase.Categories.ToList();
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult productUpdate(Product updateProduct)
        {
            using (CommerceDbEntities dataBase = new CommerceDbEntities())
            {
                var selectedProduct = dataBase.Products.Find(updateProduct.Id);
                selectedProduct.Brand = updateProduct.Brand;
                selectedProduct.ProductName = updateProduct.ProductName;
                selectedProduct.CategoryId = updateProduct.CategoryId;
                dataBase.SaveChanges();
                return Json("Success");
            }
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult productInsert()
        {
            using (CommerceDbEntities dataBase = new CommerceDbEntities())
            {
                //Dropdown'in içine kategorileri gönderir
                var listCategory = dataBase.Categories.ToList();
                return View(listCategory);
            }
        }

        [HttpPost]
        public JsonResult productInsert(Product newProduct)
        {
            using (CommerceDbEntities dataBase = new CommerceDbEntities())
            {
                //Form post edildiğinde gelen product veritabanına eklendi
                dataBase.Products.Add(newProduct);
                dataBase.SaveChanges();
                return Json("Success");
            }
        }
    }
}