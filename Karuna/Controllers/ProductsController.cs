using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Karuna.Models;
using PagedList;

namespace Karuna.Controllers
{
    public class ProductsController : Controller
    {
        private KarunaEntities db = new KarunaEntities();

        // GET: Products
        public ActionResult Index(ProductViewModels model)
        {
            var results = from a in db.Products
                          orderby a.Price descending
                          select new ProductView()
                          {
                              Id = a.Id,
                              Productname = a.Productname,
                              Price = a.Price,
                              Details = a.Details,
                              Publish = a.Publish
                          };

            var pageIndex = model.Page ?? 1;
            model.SearchResults = results.ToPagedList(pageIndex, 25);

            return View(model);
        }

        public ActionResult Result(ProductViewModels model)
        {
            var results = from a in db.Products
                          orderby a.Price descending
                          select new ProductView()
                          {
                              Id = a.Id,
                              Productname = a.Productname,
                              Price = a.Price,
                              Details = a.Details,
                              Publish = a.Publish
                          };

            if (model.Productname != null && model.Productname != "")
            {
                results = results.Where(x => x.Productname.Contains(model.Productname));
            }

            var pageIndex = model.Page ?? 1;
            model.SearchResults = results.ToPagedList(pageIndex, 25);

            return PartialView("Result", model);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Productname,Price,Details,Publish")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult ProductInsert(ProductViewModels model)
        {
            try
            {
                Product product = model.ProductInformation;
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult ProductEdit(ProductViewModels model)
        {
            try
            {

                //var product = db.Products.Find(model.ProductInformation.Id);
                //model.ProductInformation = product;
                db.Entry(model.ProductInformation).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ProductViewModels model = new ProductViewModels();
            try
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    model.ProductInformation = product;
                }
                else
                {
                    return Json(new { success = false, error = "Not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return PartialView("Edit", model);
        }

        public ActionResult GetProduct(int id)
        {
            ProductViewModels model = new ProductViewModels();
            try
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    model.ProductInformation = product;
                }
                else
                {
                    return Json(new { success = false, error = "Not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return PartialView("Show", model);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
