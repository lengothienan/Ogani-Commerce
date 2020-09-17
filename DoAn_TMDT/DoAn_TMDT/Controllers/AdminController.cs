using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library;
using ActionDB;
using DoAn_TMDT.Models;

namespace DoAn_TMDT.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("/Admin/Login");
            }
            return View();
        }
        public ActionResult Login()
        {
            if (Session["Admin"] != null)
            {
                Response.Redirect("/Admin/Index");
            }
            return View();
        }
        public JsonResult ActionLogin(FormCollection data)
        {
            var uid = data["uid"];
            var pw = data["pw"];
            JsonResult js = new JsonResult();
            if (String.IsNullOrEmpty(uid) || String.IsNullOrEmpty(pw))
            {
                js.Data = new
                {
                    status = "ERR"
                };
                Response.Redirect("/Admin/Index");
            }
            else
            {
                Code code = new Code();
                Admin u = code.GetAdmin(uid);
                if (u == null)
                {
                    js.Data = new
                    {
                        status = "ERR"
                    };
                }
                else
                {
                    if (u.PW != Encryptor.MD5Hash(pw))
                    {
                        js.Data = new
                        {
                            status = "ERR"
                        };
                    }
                    else
                    {
                        
                            Session["Admin"] = u;
                            Session.Timeout = 60;
                            js.Data = new
                            {
                                status = "OK"
                            };
                        
                    }
                }
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public RedirectToRouteResult Logout()
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("/quan-li-admin");
            }
            else
            {
                Session.Remove("Admin");
            }
            return RedirectToAction("Login");
        }
    }
}