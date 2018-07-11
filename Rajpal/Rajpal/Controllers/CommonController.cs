using Rajpal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace Rajpal.Controllers
{
    public class CommonController : Controller
    {


        public ActionResult SaveAppointment(string FirstName, string LastName, string Email, string PhoneNumber, string AppointmentDate, string AppointmentTime, string Notes,string UserType)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    string sqlQuery = @"Insert Into tbl_ScheduleAppointment (FirstName,LastName,[Email],[PhoneNumber],AppointmentDate,AppointmentTime,Notes,UserType) Values (@FirstName,@LastName,@Email,@PhoneNumber,@AppointmentDate,@AppointmentTime,@Notes,@UserType);SELECT CAST(SCOPE_IDENTITY() as int)";
                    int rowsAffected = db.Query<int>(sqlQuery, new
                    {
                        FirstName,
                        LastName,
                        Email,
                        PhoneNumber,
                        AppointmentDate,
                        AppointmentTime,
                        Notes,
                        UserType
                    }).SingleOrDefault();

                }
                CommonClass.SendMailToAdmin(EnumValue.GetEnumDescription(EnumValue.EmailType.Appointment), FirstName + " " + LastName, Email, PhoneNumber, AppointmentDate, AppointmentTime, Notes, UserType);
                CommonClass.SendMailToUser(EnumValue.GetEnumDescription( EnumValue.EmailType.Appointment),FirstName + " " + LastName, Email,AppointmentDate, AppointmentTime);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Contact(string username, string lastname, string Email, string phn, string message,string UserType)
        {
            bool isSent = CommonClass.SendMailToAdmin(EnumValue.GetEnumDescription(EnumValue.EmailType.ContactUs), username + " " + lastname, Email, phn, "", "", message, UserType);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}