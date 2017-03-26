using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    [Authorize]
    [HandleError(View = "error_DbEntityValidationException", ExceptionType = typeof(DbEntityValidationException))]
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        ProductRepository repo = RepositoryHelper.GetProductRepository(); 

        // GET: Products
        public ActionResult Index(string filterActive, string sortBy, string keyword, int pageNo = 1)
        {
            var filterAction = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct().ToList();
            //var filterAction = repoProduct.All().Select(p => p.Active).Distinct()
            ViewBag.filterActive = new SelectList(filterAction);

            doSearchOnIndex(sortBy, keyword, pageNo);

            return View();
        }

        [HttpPost]
        public ActionResult Index(string filterActive, Product[] data, string sortBy, string keyword, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var prod = repoProduct.find(item.ProductId);

                    prod.ProductName = item.ProductName;
                    prod.Price = item.Price;
                    prod.Stock = item.Stock;
                    prod.Active = item.Active;
                }

                repoProduct.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            var filterAction = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct();
            //var filterAction = repoProduct.All().Select(p => p.Active).Distinct()
            ViewBag.filterActive = new SelectList(filterAction);

            doSearchOnIndex(sortBy, keyword, pageNo);

            return View();
        }

        private void doSearchOnIndex(string sortBy, string keyword, int pageNo)
        {
            var data = repo.All().AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

            if (sortBy == "+Price")
            {
                data = data.OrderBy(p => p.Price);
            }
            else
            {
                data = data.OrderByDescending(p => p.Price);
            }

            ViewBag.keyword = keyword;
            ViewBag.sortBy = sortBy;
            ViewBag.pageNo = pageNo;

            ViewData.Model = data.ToPagedList(pageNo, 10);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ProductOrderLines(int id)
        {
            Product product = repoProduct.find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product.OrderLine);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();

                repo.Add(product);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock,isDeleted")] Product product)
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = repoProduct.find(id);
            
            //if (TryUpdateModel(product, new string[] { "ProductName", "Stock"}))
            //{
            //    //var db = repo.UnitOfWork.Context;
            //    //db.Entry(product).State = EntityState.Modified;
            //    //db.SaveChanges();
            //    repoProduct.UnitOfWork.Commit();

            //    return RedirectToAction("Index");
            //}
            //return View(product);


            if (TryUpdateModel(product, new string[] { "ProductName", "Stock", "Active" }))
            {
                //var db = repo.UnitOfWork.Context;
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                //repoProduct.UnitOfWork.Commit();

                //return RedirectToAction("Index");
            }
            repoProduct.UnitOfWork.Commit();

            return RedirectToAction("Index");

            //return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.find(id.Value);
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
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repo.find(id);
            repo.Delete(product);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //public ActionResult login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult login(loginVM login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return Content("login succeed!");
        //    }
        //    else
        //    {
        //        return Content("login failed!");
        //    }
        //}
    }
}
