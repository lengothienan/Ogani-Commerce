using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library;
using ActionDB;
using System.Security.Principal;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;
using DoAn_TMDT.Models;

namespace DoAn_TMDT.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Chat(string id)
        {
            if (id == null)
            {
                id = "13";
            }
            Code code = new Code();
            User u = code.GetUserID(id);
            if(u != null)
            {
                ViewBag.User = u.UID;
            }
            else
            {
                ViewBag.User = "";
            }
            return View();
        }
        public JsonResult JSChat(FormCollection data)
        {
            JsonResult json = new JsonResult();
            if (data == null)
            {
                Response.Redirect("/Admin/Chat");
            }
            string thutu = data["thutu"];
            Code code = new Code();
            Chat chat = code.GetChats().Where(m => m.IDChat == thutu).FirstOrDefault();
            if (chat != null)
            {
                if (chat.ToWho == data["User"] || chat.FromWho == data["User"])
                {
                    if (chat.FromWho == data["User"])
                    {
                        json.Data = new
                        {
                            status = "OK",
                            Name = data["User"],
                            Mess = chat.Mess,
                            Time = chat.Time
                        };
                    }
                    if (chat.ToWho == data["User"])
                    {
                        json.Data = new
                        {
                            status = "OK",
                            Name = "Admin",
                            Mess = chat.Mess,
                            Time = chat.Time
                        };
                    }
                }
                else
                {
                    json.Data = new
                    {
                        status = "OKK"
                    };
                }
            }
            else
            {
                Chat chatlast = code.GetChats().LastOrDefault();
                if (int.Parse(thutu) < int.Parse(chatlast.IDChat))
                {
                    json.Data = new
                    {
                        status = "OKK"
                    };
                }
                else
                {
                    json.Data = new
                    {
                        status = "ER"
                    };
                }
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendMess(FormCollection data)
        {
            JsonResult json = new JsonResult();
            if (data == null)
            {
                Response.Redirect("/Admin/Chat");
            }
            string text = data["text"];
            Code code = new Code();
            Chat comment = code.GetChats().OrderBy(m => int.Parse(m.IDChat)).LastOrDefault();
                Chat cm = new Chat();
                if (comment != null)
                {
                    cm.IDChat = (int.Parse(comment.IDChat) + 1).ToString();
                }
                else
                {
                    cm.IDChat = "1";
                }
                string[] date = DateTime.Now.ToString().Split(' ');
                cm.FromWho = "admin";
                cm.Mess = text;
                cm.ToWho = data["User"];
                cm.Time = date[1] + date[2];
                code.AddObject(cm);
                code.Save();
                json.Data = new
                {
                    status = "OK"
                };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}