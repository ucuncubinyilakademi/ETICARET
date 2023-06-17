using ETICARET.Context;
using ETICARET.Entity;
using ETICARET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETICARET.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().AddToProduct(product, 1);
            }

            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }


        private Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();

            if (cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(cart,entity);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(entity);
            }
        }

        private void SaveOrder(Cart cart,ShippingDetails entity)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = cart.Total();
            order.OrderState = EnumOrderState.Waiting;
            order.OrderDate = DateTime.Now;
            order.Username = User.Identity.Name;

            order.AdresBasligi = entity.AdresBasligi;
            order.Adres = entity.Adres;
            order.Sehir = entity.Sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.PostaKodu = entity.PostaKodu;

            order.OrderItems = new List<OrderItem>();

            foreach (var pr in cart.CartItems)
            {
                var orderItem = new OrderItem();
                orderItem.ProductId = pr.Product.Id;
                orderItem.Price = pr.Quantity * pr.Product.Price;
                orderItem.Quantity = pr.Quantity;

                order.OrderItems.Add(orderItem);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}