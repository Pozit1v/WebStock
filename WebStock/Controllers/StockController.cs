using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStock.Controllers
{
    public class StockController : Controller
    {
        public ActionResult Index()
        {
            using (StockDbEntities db = new StockDbEntities())
            {
                var stocks = db.Stocks.Include("StocksInfo").Include("Product").Include("Product.Unit");
                
                return View(stocks.ToList());
            }
        }

        [HttpGet]
        public ActionResult ProductToStock()
        {
            using (StockDbEntities db = new StockDbEntities())
            {
                List<StocksInfo> stocks = db.StocksInfoes.ToList();
                ViewBag.StocksList = new SelectList(stocks, "Id", "StockName");
                List<Product> products = db.Products.ToList();
                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");


                return PartialView("_ProductToStock");
            }


        }

        [HttpPost]
        public ActionResult ProductToStock(Stock stock)
        {
            using (StockDbEntities db = new StockDbEntities())
            {
                stock.Id = Guid.NewGuid();
                stock.CreateDate = DateTime.Now;
                db.Stocks.Add(stock);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


        }

        [HttpGet]
        public ActionResult AddNewStock()
        {
            return PartialView("_AddNewStock");
        }

        [HttpPost]
        public ActionResult AddNewStock(StocksInfo stock)
        {
            using(StockDbEntities db = new StockDbEntities())
            {
                stock.Id = Guid.NewGuid();
                db.StocksInfoes.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult EditStock(Guid id)
        {
            using(StockDbEntities db = new StockDbEntities())
            {
                List<Product> product = db.Products.ToList();
                ViewBag.ProductList = new SelectList(product, "Id", "ProductName");
                List<StocksInfo> stockInfo = db.StocksInfoes.ToList();
                ViewBag.StockInfoList = new SelectList(stockInfo, "Id", "StockName");
                var stock = db.Stocks.Find(id);
                return PartialView("_EditStock", stock);
            }
            
        }

        [HttpPost]
        public ActionResult EditStock(Stock stock)
        {
            using (var db = new StockDbEntities())
            {
                stock.CreateDate = DateTime.Now;
                db.Entry(stock).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult DeleteProductInStock(Guid id)
        {
            using(StockDbEntities db = new StockDbEntities())
            {
                var stock = db.Stocks.Include("StocksInfo").Include("Product").Include("Product.Unit").First(x => x.Id == id);
                return PartialView("_DeleteProductInStock", stock);
            }
        }

        [HttpPost]
        public ActionResult DeleteProductInStock(Stock stock)
        {
            using (StockDbEntities db = new StockDbEntities())
            {
                db.Stocks.Attach(stock);
                db.Stocks.Remove(stock);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
       }

    }
}