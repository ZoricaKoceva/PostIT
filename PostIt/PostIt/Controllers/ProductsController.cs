using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PostIt.Models;

namespace PostIt.Controllers
{
    public class ProductsController: Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(int? category)
        {
            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Category = db.Categories.FirstOrDefault(x => x.Id == category);
            return View(db.Products.Include(x => x.Category).Include(x => x.User).Where(x => x.Approved == true).Where(x => x.Category.Id == category).ToList());
        }

        // GET: Popular Products
        public ActionResult Popular()
        {
            return PartialView(db.Products.OrderByDescending(x => x.Id).Include(x => x.Category).Include(x => x.User).Where(x => x.Approved == true).Take(4).ToList());
        }

        // SEARCH
        public ActionResult Search(string query)
        {
            if (query != "")
            {
                var productsFound = db.Products.Include(x => x.Category).Include(x => x.User).Where(x => x.Approved == true).Where(x => x.Title.Contains(query)).ToList();
                ViewBag.Query = query;
                return View(productsFound);
            } 
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // ADMIN PAGE HOME
        [Authorize(Roles ="Admin")]
        public ActionResult Admin()
        {
            var products = db.Products.Include(x => x.Category).Include(x => x.User).ToList();
            ViewBag.Users = db.Users.ToList();
            var users = db.Users.ToList();
            UserStore<ApplicationUser> mystore = new UserStore<ApplicationUser>(db);
            ApplicationUserManager userMgr = new ApplicationUserManager(mystore);

            var array = new int[ViewBag.Users.Count];
            int counter = 0;
            foreach (var curuser in users)
            {
                bool iss = userMgr.IsInRole(curuser.Id, "Admin");
                if (iss == true)
                {
                    array[counter] = 1;
                } 
                else
                {
                    array[counter] = 0;
                }
                counter++;
            }

            ViewBag.IsAdmin = array;

            return View(products);
        }

        // CHANGE STATUS OF PRODUCT
        [Authorize(Roles ="Admin")]
        public ActionResult ChangeStatusOfProduct(int? productId, bool status)
        {
            if (productId != null)
            {
                var product = db.Products.Include(x => x.Category).Include(x => x.User).FirstOrDefault(x => x.Id == productId);
                if (product != null)
                {
                    product.Approved = status;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return Content("Success");
                } 
                else
                {
                    return Content("Error");
                }
            } 
            else
            {
                return Content("Error");
            }
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(x => x.Category).Include(x => x.User).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles ="User,Admin")]
        public ActionResult Create()
        {
            CreateEditProductModel prod = new CreateEditProductModel();
            prod.Categories = db.Categories.ToList();
            return View(prod);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Price,CategoryId,Image,DateTime")] CreateEditProductModel product)
        {
            Product newProduct = new Product();
            newProduct.User = db.Users.Find(User.Identity.GetUserId());
            newProduct.Category = db.Categories.Find(product.CategoryId);
            newProduct.Price = product.Price;
            newProduct.Title = product.Title;
            newProduct.Description = product.Description;
            newProduct.Image = product.Image;
            newProduct.Approved = false;
            if (ModelState.IsValid)
            {
                newProduct.DateTime = DateTime.Now;
                db.Products.Add(newProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "User,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(m=> m.User).Include(m=> m.Category).FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (product.User.Id != User.Identity.GetUserId()) {
                return RedirectToAction("Index", "Home");
            }
            CreateEditProductModel editProduct = new CreateEditProductModel();
            editProduct.Title = product.Title;
            editProduct.Description = product.Description;
            editProduct.Price = product.Price;
            editProduct.CategoryId = product.Category.Id;
            editProduct.Categories = db.Categories.ToList();
            editProduct.Image = product.Image;
            return View(editProduct);

        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Price,CategoryId,Image,DateTime")] CreateEditProductModel product)
        {
            Product newProduct = db.Products.Include(m => m.User).Include(m => m.Category).FirstOrDefault(m => m.Id == product.Id);
            newProduct.Category = db.Categories.Find(product.CategoryId);
            newProduct.Price = product.Price;
            newProduct.Title = product.Title;
            newProduct.Description = product.Description;
            newProduct.Image = product.Image;
            if (ModelState.IsValid)
            {
                db.Entry(newProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize]
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Include(x => x.User).FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Include(x => x.User).FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                if (product.User.Id == User.Identity.GetUserId())
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }

        // POST: Products/Delete/5

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
