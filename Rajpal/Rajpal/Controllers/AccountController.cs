using Rajpal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.IO;
namespace Rajpal.Controllers
{
    public class AccountController : Controller
    {
        IDbConnection db = CommonClass.OpenConnection();

        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       
        [HttpPost]
        public ActionResult Register(string Firstname, string LastName, string Email, string Password, string Street = "", string City = "", string State = "", string country = "", string UserType="")
        {
            try
            {
                int rowsAffected = 0;
                string sqlQuery = @"Insert Into Registration (FirstName,LastName,[EmailId],[Password],Address,City,State,country,UserType) Values (@FirstName,@LastName,@EmailId,@Password,@Address,@City,@State,@country,@UserType);SELECT CAST(SCOPE_IDENTITY() as int)";
                     rowsAffected = db.Query<int>(sqlQuery, new
                    {
                        FirstName=Firstname,
                        LastName,
                        EmailId=Email,
                        Password,
                        Address = Street,
                        City,
                        State,
                        country,
                        UserType
                    }).SingleOrDefault();

                
                //HttpPostedFileBase file = Request.Files["file"];
                 
                //CommonClass.SendMailToAdmin(EnumValue.GetEnumDescription(EnumValue.EmailType.Appointment), FirstName + " " + LastName, Email, PhoneNumber, AppointmentDate, AppointmentTime, Notes);
                //CommonClass.SendMailToUser(EnumValue.GetEnumDescription(EnumValue.EmailType.Appointment), FirstName + " " + LastName, Email, AppointmentDate, AppointmentTime);
                return Json(rowsAffected, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int UserId)
        {

            string selectQuery = "SELECT * FROM [dbo].[Registration]  WHERE ID = @ID";
            Registration result = db.Query<Registration>(selectQuery, new { ID = UserId }).SingleOrDefault();
            if(result!=null)
            {
                string fullPath = Server.MapPath("~/Photos/RegisteredUsers");

                if (Directory.Exists(fullPath))
                {

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        file.SaveAs(Path.Combine(fullPath, fileName));
                    }
                }
                string URL = CommonClass.GetURL() + "/Photos/RegisteredUsers/" + file.FileName;
                string sqlQuery = "UPDATE Registration SET ProfileImage = @ProfileImage WHERE ID = @ID";
                int rowsAffected = db.Execute(sqlQuery, new { ID = UserId, ProfileImage = URL });
            }
           
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(string Email, string Password)
        {
                try
                {
                   // var password = CommonClass.EncryptString(model.Password);
                    var query = "Select * from [Registration] where EmailId=@Email and Password=@Password";
                    var result = db.Query<Registration>(query, new { Email = Email, Password = Password }).SingleOrDefault();
                    if (result != null)
                    {
                        Session["UserId"] = result.ID;
                       
                            return Json("success", JsonRequestBehavior.AllowGet);
                       
                    }
                    else
                    {
                        return Json("error", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    string ErrorMsg = ex.Message.ToString();

                    return null;
                }
           

        }

        public ActionResult IsEmailExits(string Email)
        {
            try
            {
                // var password = CommonClass.EncryptString(model.Password);
                var query = "Select * from [Registration] where EmailId=@Email";
                var result = db.Query<Registration>(query, new { Email = Email}).SingleOrDefault();
                if (result != null)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message.ToString();

                return null;
            }


        }


        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction("Index");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult ForgotPassword([Bind(Include = "Email")]ForgotPasswordModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            using (IDbConnection _db = Common.OpenConnection())
        //            {
        //                var query = "Select * from [User] where Email=@Email";
        //                var result = _db.Query<User>(query, new { Email = model.Email }).SingleOrDefault();
        //                if (result != null)
        //                {
        //                    var password = Common.DecryptString(result.Password);
        //                    Common.SendMailToUser("ForgotPassword", result.Name, result.Email, password);
        //                    ViewBag.success = "success";
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("Email", "This email address does not exist in our records.");
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string ErrorMsg = ex.Message.ToString();//

        //    }
        //    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
        //    var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);

        //    return View(model);
        //}


        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult ChangePassword([Bind(Include = "OldPassword,NewPassword,ConfirmPassword")] ChangePasswordModel changepasswordmodel)
        //{
        //    try
        //    {

        //        var UserId = Session["UserId"];
        //        var password = Common.EncryptString(changepasswordmodel.OldPassword);
        //        using (IDbConnection _db = Common.OpenConnection())
        //        {
        //            var query = "Select * from [User] where UserId=@UserId and Password=@Password";
        //            var result = _db.Query<User>(query, new { UserId = UserId, Password = password }).SingleOrDefault();
        //            if (result != null)
        //            {
        //                var p = new DynamicParameters();
        //                p.Add("@UserId", UserId);
        //                p.Add("@Name", result.Name);
        //                p.Add("@Email", result.Email);
        //                p.Add("@Password", Common.EncryptString(changepasswordmodel.NewPassword));
        //                p.Add("@MobileNo", result.MobileNo);
        //                //p.Add("@LogoPath", result.LogoPath);
        //                _db.Query<User>("AddUser", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
        //                TempData["ShowMessage"] = "success";
        //                TempData["MessageBody"] = "Password changed successfully.";
        //                return RedirectToAction("Index");

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("OldPassword", "Old password is wrong.");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string ErrorMsg = ex.Message.ToString();//

        //    }
        //    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
        //    var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
        //    return View(changepasswordmodel);
        //}
    }
}