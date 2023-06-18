using ETICARET.Context;
using ETICARET.Identity;
using ETICARET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ETICARET.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var urunler = db.Products
                .Where(i => i.IsApproved && i.IsHome)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 50) : i.Name,
                    Description = i.Description.Length>100 ? i.Description.Substring(0,100) : i.Description,
                    Price = i.Price,
                    Image = i.Image,
                    CategoryId=i.CategoryId
                }).ToList();

            return View(urunler);
        }
        public ActionResult Details(int id)
        {
            return View(db.Products.Where(i => i.Id==id).FirstOrDefault());
        }

        public ActionResult List(int? id)
        {
            //AsQueryable() : Tamamlanmamış sorgu yazmamızı sağlar.

            var urunler = db.Products
                .Where(i => i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 50) : i.Name,
                    Description = i.Description.Length > 100 ? i.Description.Substring(0, 100) : i.Description,
                    Price = i.Price,
                    Image = i.Image,
                    CategoryId=i.CategoryId
                }).AsQueryable();

            if (id != null)
            {
                urunler = urunler.Where(i => i.CategoryId == id);
            }


            return View(urunler.ToList());
        }

        public PartialViewResult GetCategories()
        {   
            return PartialView(db.Categories.ToList());
        }

        public ActionResult MailSend(string email)
        {
            var message = new MailMessage();
            message.From = new MailAddress("test_altan_emre_1989@hotmail.com");

           message.To.Add(new MailAddress(email));
          
            message.Subject = "E-Bülten";
            message.Body = "Firma e-bülten aboneliğiniz gerçekleştirilmiştir.<br><b> Her ay düzenli olarak email alacaksınız.</b>";
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient("smtp-mail.outlook.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("test_altan_emre_1989@hotmail.com", "UBY12345");

                smtp.Send(message);
                return RedirectToAction("Index");
            }
        }
    }
}