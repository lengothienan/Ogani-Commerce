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
    public class HomeController : Controller
    {

        public ActionResult Product(string id, string textsearch)
        {
            ViewBag.TextSearch = textsearch;
            Code code = new Code();
            List<ProductOfType> pot = code.GetProductOfTypess(id);
            return View(pot);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Response.Redirect("/Home/Product");
            }
            Code code = new Code();
            Product sp = code.GetProduct(id);
            if (sp == null)
            {
                Response.Redirect("/Home/Product");
            }
            return View(sp);
        }
        public ActionResult Cart()
        {
            List<CartItem> giohang = Session["cart"] as List<CartItem>;
            return View(giohang);
        }
        public JsonResult AddCart(FormCollection data)
        {
            JsonResult js = new JsonResult();
            if (String.IsNullOrEmpty(data["soluong"]))
            {
                Response.Redirect("/Home/Index");
            }
            else
            {
                string IDSP = data["idsp"];
                Code code = new Code();
                Product sp = code.GetProduct(IDSP);
                if (sp == null)
                {
                    js.Data = new
                    {
                        status = "ERR"
                    };
                }
                else
                {
                    int soluong = int.Parse(sp.Amount);
                    if (soluong > 0)
                    {
                        if (Session["cart"] == null) // Nếu giỏ hàng chưa được khởi tạo
                        {
                            Session["cart"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
                        }
                        List<CartItem> giohang = Session["cart"] as List<CartItem>;  // Gán qua biến giohang dễ code
                        // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

                        if (giohang.FirstOrDefault(m => m.SanPhamID == IDSP) == null) // ko co sp nay trong gio hang
                        {
                            Sale sl = code.GetSale(sp.IDKM);
                            CartItem newItem = new CartItem();
                            double gia = int.Parse(sp.Cost) - int.Parse(sp.Cost) * int.Parse(sl.phantram) * 0.01;
                            newItem.SanPhamID = IDSP;
                            newItem.TenSanPham = sp.Name;
                            newItem.SoLuong = int.Parse(data["soluong"]);
                            newItem.Hinh = sp.Image;
                            newItem.DonGia = "" + gia;
                            giohang.Add(newItem);  // Thêm CartItem vào giỏ 
                            js.Data = new
                            {
                                status = "OK"
                            };
                        }
                        else
                        {
                            // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                            CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == IDSP);
                            Code db = new Code();
                            Product a = db.GetProduct(IDSP);
                            if (cardItem.SoLuong + int.Parse(data["soluong"]) <= int.Parse(a.Amount))
                            {
                                cardItem.SoLuong += int.Parse(data["soluong"]);

                                js.Data = new
                                {
                                    status = "OK"
                                };
                            }
                            else
                            {

                                js.Data = new
                                {
                                    status = "ERAm",
                                    soluongmua = int.Parse(a.Amount)
                                };
                            }
                        }
                    }
                    else
                    {
                        js.Data = new
                        {
                            status = "ERR"
                        };
                    }
                }
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCart(FormCollection data)
        {
            // tìm carditem muon sua
            string SanPhamID = data["idsp"];
            int soluongmoi = int.Parse(data["soluong"]);
            List<CartItem> giohang = Session["cart"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            JsonResult js = new JsonResult();
            if (String.IsNullOrEmpty(SanPhamID))
            {
                Response.Redirect("/Home/Index");
            }
            if (itemSua != null)
            {
                Code code = new Code();
                Product sp = code.GetProduct(SanPhamID);
                if (soluongmoi > int.Parse(sp.Amount))
                {
                    itemSua.SoLuong = int.Parse(sp.Amount);
                }
                else
                {
                    itemSua.SoLuong = soluongmoi;
                }
                js.Data = new
                {
                    status = "OK",
                };
            }
            else
            {
                js.Data = new
                {
                    status = "ER"
                };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemovesCart(FormCollection data)
        {
            List<CartItem> giohang = Session["cart"] as List<CartItem>;
            var SanPhamID = data["idsp"];
            if (String.IsNullOrEmpty(SanPhamID))
            {
                Response.Redirect("/Home/Index");
            }
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            JsonResult js = new JsonResult();
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
                js.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                js.Data = new
                {
                    status = "ER"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckCart(FormCollection Data)
        {
            JsonResult js = new JsonResult();
            if (String.IsNullOrEmpty(Data["check"]))
            {
                Response.Redirect("/Home/Index");
            }
            string money = Data["check"];
            Code code = new Code();
            List<CartItem> giohang = Session["Cart"] as List<CartItem>;
            int tongtien = 0;
            if (Session["Cart"] != null)
            {
                tongtien = giohang.Sum(m => m.ThanhTien); ;
            }
            if (tongtien == int.Parse(money))
            {
                js.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                js.Data = new
                {
                    status = "ER"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckLove(FormCollection Data)
        {
            JsonResult js = new JsonResult();
            if (String.IsNullOrEmpty(Data["check"]))
            {
                Response.Redirect("/Home/Index");
            }
            string love = Data["check"];
            Code code = new Code();
            User u = (User)Session["User"];
            int tongtien = 0;
            if (u != null)
            {
                tongtien = code.GetLoveProducts(u.ID).Count();
            }
            if (tongtien == int.Parse(love))
            {
                js.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                js.Data = new
                {
                    status = "ER"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductSearch(FormCollection data)
        {
            if (data == null)
            {
                Response.Redirect("Home/Index");
            }
            JsonResult js = new JsonResult();
            int min = int.Parse(data["min"]) * 1000;
            int max = int.Parse(data["max"]) * 1000;
            string text = data["text"];
            Code code = new Code();
            if (string.IsNullOrEmpty(text))
            {
                List<Product> products = code.GetProductSearch("LeNgoThienAn");
                js.Data = new
                {
                    status = "OK",
                    product = products,
                };
            }
            else
            {
                List<Product> products = code.GetProductSearch(text).Where(m => int.Parse(m.Cost) >= min && int.Parse(m.Cost) <= max).ToList();
                js.Data = new
                {
                    status = "OK",
                    product = products,
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult CheckOut()
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/Home/Login");
            }
            if (Session["Cart"] == null)
            {
                Response.Redirect("/Home/Product");
            }
            List<CartItem> giohang = Session["Cart"] as List<CartItem>;
            return View(giohang);
        }
        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                Response.Redirect("/Home/Index");
            }
            return View();
        }
        public JsonResult ActionLogin(FormCollection data)
        {
            if (data == null)
            {
                Response.Redirect("/Home/Index");
            }
            var user = data["user"];
            var pass = Encryptor.MD5Hash(data["password"]);
            JsonResult js = new JsonResult();
            Code code = new Code();
            User u = code.GetUser(user);
            if (u != null)
            {
                if (u.PW == pass)
                {
                    Session["User"] = u;
                    Session.Timeout = 60;
                    js.Data = new
                    {
                        status = "OK"
                    };
                }
                else
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
            }
            else
            {
                js.Data = new
                {
                    status = "ER"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public RedirectToRouteResult Logout()
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/Home/Index");
            }
            else
            {
                Session.Remove("User");
            }
            return RedirectToAction("Index");
        }
        public JsonResult CheckSession(FormCollection data)
        {
            if (data == null)
            {
                Response.Redirect("/Home/Index");
            }
            JsonResult js = new JsonResult();
            if (Session["User"] != null)
            {
                User u = (User)Session["User"];
                js.Data = new
                {
                    status = "OK",
                    User = u.UID
                };
            }
            else
            {
                js.Data = new
                {
                    status = "ER",
                    User = ""
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSLove(FormCollection data)
        {
            JsonResult js = new JsonResult();
            if (data == null)
            {
                Response.Redirect("/Home/Index");
            }
            Code code = new Code();
            if (Session["User"] == null)
            {
                Response.Redirect("/Home/Login");
            }
            User user = (User)Session["User"];
            LoveProduct lo = new LoveProduct();
            lo.IDSP = data["idsp"];
            lo.UID = user.ID;
            lo.Status = "Love";
            LoveProduct love = code.GetLoveProduct(user.ID,lo.IDSP);
            if (love == null)
            {
                code.AddObject(lo);
            }
            else
            {
                code.DeleteObject(love);
            }
            code.Save();
            js.Data = new
            {
                status = "OK"
            };
            return Json(js,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Love()
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/Home/Login");
            }
            Code code = new Code();
            User us = (User)Session["User"];
            List<LoveProduct> love = code.GetLoveProducts(us.ID);
            return View(love);
        }
        public JsonResult JSCM(FormCollection data)
        {
            JsonResult json = new JsonResult();
            if (data == null)
            {
                Response.Redirect("/Home/Index");
            }
            string thutu = data["thutu"];
            string IDSP = data["IDSP"];
            Code code = new Code();
            Comment cm = code.GetComment(thutu);
            if (cm == null)
            {
                Comment cmm = code.GetComments().OrderBy(m=>int.Parse(m.IDCM)).LastOrDefault();
                if (int.Parse(thutu) < int.Parse(cmm.IDCM))
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
            else
            {
                if(IDSP == cm.IDSP)
                {
                    User us = code.GetUserID(cm.UID);
                    json.Data = new
                    {
                        status = "OK",
                        Comment = cm.Comment1,
                        Name = us.UID
                    };
                }
                else
                {
                    json.Data = new
                    {
                        status = "OKK"
                    };
                }
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendComment(FormCollection data)
        {
            JsonResult json = new JsonResult();
            if (data == null)
            {
                Response.Redirect("/Home/Index");
            }
            string text = data["text"];
            string IDSP = data["IDSP"];
            Code code = new Code();
            Comment comment = code.GetComments().OrderBy(m=>int.Parse(m.IDCM)).LastOrDefault();
            if (Session["User"] != null)
            {
                Comment cm = new Comment();
                User user = (User)Session["User"];
                cm.IDCM = (int.Parse(comment.IDCM)+1).ToString();
                cm.IDSP = IDSP;
                cm.Comment1 = text;
                cm.UID = user.ID;
                code.AddObject(cm);
                code.Save();
                json.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                json.Data = new
                {
                    status = "ER"
                };
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveCM(FormCollection data)
        {
            JsonResult js = new JsonResult();
            var IDCM = data["idcm"];
            if (String.IsNullOrEmpty(IDCM))
            {
                Response.Redirect("/Home/Index");
            }
            Code code = new Code();
            Comment cm = code.GetComment(IDCM);
            if (Session["User"] != null)
            {
                User u = (User)Session["User"];
                if (u.ID==cm.UID)
                {
                    code.DeleteObject(cm);
                    code.Save();
                    js.Data = new
                    {
                        status = "OK"
                    };
                }
                else
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
            }
            else
            {
                js.Data = new
                {
                    status = "ER"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Res()
        {
            if(Session["User"] != null)
            {
                Response.Redirect("/Home/Index");
            }

            return View();
        }
        public JsonResult ResObj(FormCollection data)
        {
            var pw = data["pw"];
            var pw2 = data["pw2"];
            var uid = data["uid"];
            var address = data["address"];
            var email = data["email"];
            if (String.IsNullOrEmpty(uid) || String.IsNullOrEmpty(pw) || String.IsNullOrEmpty(pw2) || String.IsNullOrEmpty(address)  || String.IsNullOrEmpty(email))
            {
                Response.Redirect("/Home/Index");
            }
            JsonResult js = new JsonResult();
            var RexPW = new Regex(@"^(?=.*\d)(?=.*[A-Z])(?=.*\W).{8,32}$");
            var RexUID = new Regex(@"^[a-z_][a-z0-9_\.\s]{8,32}$");
            var RexEmail = new Regex(@"^[a-z][a-z0-9_\.]{4,31}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$");
            if (pw == pw2)
            {
                if (!RexPW.IsMatch(pw))
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else if (!RexUID.IsMatch(uid))
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else if (!RexEmail.IsMatch(email))
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else if (address.Length<10)
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else
                {
                    Code code = new Code();
                    User u = code.GetUser(uid);
                    if (u != null)
                    {
                        js.Data = new
                        {
                            status = "ERR"
                        };
                    }
                    else
                    {
                        Random maxacnhan = new Random();
                        string content = (maxacnhan.Next(1000, 9999)).ToString();
                        code.sendmail(email, "Mã xác nhận","Mã xác nhận Website Ogani: " +content);
                        js.Data = new
                        {
                            status = "OK",
                            code = content

                        };
                    }
                }
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Maxacnhan(FormCollection data)
        {
            var pw = data["pw"];
            var pw2 = data["pw2"];
            var uid = data["uid"];
            var address = data["address"];
            var email = data["email"];
            if (String.IsNullOrEmpty(uid) || String.IsNullOrEmpty(pw) || String.IsNullOrEmpty(pw2) || String.IsNullOrEmpty(address) || String.IsNullOrEmpty(email))
            {
                Response.Redirect("/Home/Index");
            }
            JsonResult js = new JsonResult();
            var RexPW = new Regex(@"^(?=.*\d)(?=.*[A-Z])(?=.*\W).{8,32}$");
            var RexUID = new Regex(@"^[a-z_][a-z0-9_\.\s]{8,32}$");
            var RexEmail = new Regex(@"^[a-z][a-z0-9_\.]{4,31}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$");
            if (pw == pw2)
            {
                if (!RexPW.IsMatch(pw))
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else if (!RexUID.IsMatch(uid))
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else if (!RexEmail.IsMatch(email))
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else if (address.Length < 10)
                {
                    js.Data = new
                    {
                        status = "ER"
                    };
                }
                else
                {
                    Code code = new Code();
                    User u = code.GetUser(uid);
                    if (u != null)
                    {
                        js.Data = new
                        {
                            status = "ERR"
                        };
                    }
                    else
                    {
                        User user = new User();
                        user.ID = Guid.NewGuid().ToString();
                        user.UID = uid;
                        user.PW = Encryptor.MD5Hash(pw);
                        user.Address = address;
                        user.Status = "On";
                        user.Email = email;
                        code.AddObject(user);
                        code.Save();
                        js.Data = new
                        {
                            status = "OK"
                        };
                    }
                }
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public ActionResult History()
        {
            return View();
        }
        public ActionResult InforOrder()
        {
            ViewBag.Title = "ABC";
            return View();
        }
    }
}