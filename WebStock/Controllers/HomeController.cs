using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebStock.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            StockDbEntities db = new StockDbEntities();

            var products = db.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            StockDbEntities db = new StockDbEntities();
            List<Unit> unit = db.Units.ToList();
            ViewBag.UnitList = new SelectList(unit, "Id", "UnitName");
            return PartialView("_AddProduct");
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            StockDbEntities db = new StockDbEntities();
            product.Id = Guid.NewGuid();
            if (string.IsNullOrEmpty(product.ProductName))
            {
                ModelState.AddModelError("ProductName", "Некорректное название");
            }
            if (product.Price < 0)
            {
                ModelState.AddModelError("Name", "Цена не может быть 0");
            }

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult EditProduct(Guid id)
        {
            using(StockDbEntities db = new StockDbEntities())
            {
                List<Unit> unit = db.Units.ToList();
                ViewBag.UnitList = new SelectList(unit, "Id", "UnitName");
                Product product = db.Products.Find(id);
                return PartialView("_EditProduct", product);
            }       
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            
                using (var db = new StockDbEntities())
                {
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
           

        }

        [HttpGet]
        public ActionResult DeleteProduct(Guid id)
        {
            StockDbEntities db = new StockDbEntities();
            Product product = db.Products.Find(id);
            return PartialView("_DeleteProduct", product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product product)
        {
            StockDbEntities db = new StockDbEntities();
            var stock = db.Stocks.Where(x => x.ProductId == product.Id);
            db.Stocks.RemoveRange(stock);
            db.Products.Attach(product);
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}