using EncryptStringSample;
using MyFirstMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyFirstMvcApp.Controllers
{
    public class UserController : Controller
    {
        string baseUrl = "http://localhost:2332/User";
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(User newUser)
        {
            using (CommerceDbEntities db = new CommerceDbEntities())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Request.Cookies["remember"] != null)
            {
                HttpCookie registeredCookie = Request.Cookies["remember"];

                Dictionary<string, string> cookie = new Dictionary<string, string>();
                cookie.Add("username", registeredCookie.Values["username"]);
                cookie.Add("password", registeredCookie.Values["password"]);

                return View(cookie);
            }
            return View();
        }
        [HttpPost]
        public JsonResult Login(User searchedUser, string rememberMe)
        {
            using (CommerceDbEntities db = new CommerceDbEntities())
            {
                User registeredUser = db.Users.FirstOrDefault(u => u.Username == searchedUser.Username);
                if (registeredUser != null)
                {
                    if (registeredUser.Password == searchedUser.Password)
                    {
                        if (rememberMe == "on")
                        {
                            //Kullanıcı bilgilerini cookie'ye atar..
                            HttpCookie rememberCookie = new HttpCookie("remember");
                            rememberCookie.Values.Add("username", registeredUser.Username);
                            rememberCookie.Values.Add("password", registeredUser.Password);
                            rememberCookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(rememberCookie);
                        }
                        Session["UserInfo"] = registeredUser;
                        return Json("Login Successfuly", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Faulty Password", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Faulty Username", JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            //HttpCookie deletedCookie = new HttpCookie("remember");
            //deletedCookie.Expires = DateTime.Now.AddDays(-1d);
            //Response.Cookies.Add(deletedCookie);

            Session["UserInfo"] = null;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            using (CommerceDbEntities db = new CommerceDbEntities())
            {
                User remindUser = db.Users.Where(u => u.Email == email).SingleOrDefault();
                if (remindUser != null)
                {
                    string encryptedResetTime = StringCipher.Encrypt(DateTime.Now.ToShortDateString(), "EncrypteKey");
                    string encryptedEmail = StringCipher.Encrypt(email, "EncrypteKey");

                    string msg = baseUrl + "/ResetPassword?resetTime=" + encryptedResetTime + "&email=" + encryptedEmail;
                    if (SendMail(email, msg))
                    {
                        return Json("Mail Sent", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Mail Sending an Error Occourred", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Email Adress Not Registered", JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public ActionResult ResetPassword(string resetTime, string email)
        {
            if (resetTime != null && email != null)
            {
                string decryptedEmail = StringCipher.Decrypt(email, "EncrypteKey");

                string decryptedResetTime = StringCipher.Decrypt(resetTime, "EncrypteKey");

                DateTime passwordResetTime = Convert.ToDateTime(decryptedResetTime);

                if (passwordResetTime <= DateTime.Now.AddDays(-1d))
                {
                    return Json("Time Out", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    using (CommerceDbEntities db = new CommerceDbEntities())
                    {
                        User remindUser = db.Users.Where(u => u.Email == decryptedEmail).SingleOrDefault();
                        if (remindUser != null)
                        {
                            return View();
                        }
                        else
                        {
                            return Json("User Can't Registered", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            else
            {
                return Json("Not Authorization", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ResetPassword(User resetUser)
        {
            using (CommerceDbEntities db = new CommerceDbEntities())
            {
                User updateUser = db.Users.Where(u => u.Username == resetUser.Username).SingleOrDefault();
                updateUser.Password = resetUser.Password;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public bool SendMail(string mailAdress, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("gurkansozdemir@gmail.com", "gurkan_sozdemir_58");
                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.To.Add(mailAdress);
                mail.From = new MailAddress("gurkansozdemir@gmail.com");
                mail.Subject = "E-Posta Sıfırlama";
                mail.Body = message;

                client.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}