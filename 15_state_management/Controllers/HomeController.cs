using _15_state_management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _15_state_management.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Session (Durum) - Cookies (Çerezler)
            // Session, kullanýcýya özgü verileri sunucu tarafýnda saklamak için kullanýlýr ve genellikle oturum süresi boyunca geçerlidir.
            //Oturum Sona erdiðinde , session verileri silinir.
            //Session da özel bilgilerin saklanmasý önerilmez.
            //Sessionlar sunucuda saklanýr.

            //Sadece deðer okumasý yapýyor
            ViewBag.UserName = HttpContext.Session.GetString("UserName");//Session dan deðer okuma
            ViewBag.CreatedAt = HttpContext.Session.GetString("CreatedAt");//Session dan deðer okuma
            ViewBag.CookieUserName = Request.Cookies["UserName"];//Çerez den deðer okuma
            ViewBag.CookieCreatedAt = Request.Cookies["CookieCreatedAt"];//Çerez den deðer okuma


            // Cookies, kullanýcýya özgü verileri istemci tarafýnda (tarayýcýda) saklamak için kullanýlýr ve genellikle belirli bir süre boyunca geçerlidir.

            return View();
        }

        public IActionResult CreateSession()
        {
            HttpContext.Session.SetString("UserName", "Furkan Uçan");//Session a deðer atama
            HttpContext.Session.SetString("CreatedAt", DateTime.Now.ToString("HH:mm:ss"));//Session a deðer atama
            TempData["Message"] = "Session oluþturuldu.";
            return RedirectToAction("Index");
        }
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();//Session temizleme
            TempData["Message"] = "Session temizlendi.";
            return RedirectToAction("Index");
        }

        public IActionResult CreateCookie()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1),// Çerezin 1 dakika sonra sona ermesi
                HttpOnly = true, // Çerezin sadece sunucu tarafýnda eriþilebilir olmasý
                IsEssential = true // Çerezin temel iþlevsellik için gerekli olduðunu belirtir
            };

            Response.Cookies.Append("UserName", "Erkan Turk", cookieOptions);//Çerez oluþturma
            Response.Cookies.Append("CookieCreatedAt", DateTime.Now.ToString("HH:mm:ss"), cookieOptions);//Çerez oluþturma

            TempData["Message"] = "Çerez oluþturuldu.";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
