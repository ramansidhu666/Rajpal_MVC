using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace Rajpal
{
    public class CommonClass
    {
        public static Boolean SendMailToUser(string Type, string Name, string Email = "", string Date = "", string Time = "")
        {
            try
            {
                // Send mail.
                MailMessage mail = new MailMessage();

                string FromEmailID = WebConfigurationManager.AppSettings["FromEmailID"];
                string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                string ToEmailID = WebConfigurationManager.AppSettings["ToEmailID"];

                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
                int _Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString());
                Boolean _UseDefaultCredentials = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseDefaultCredentials"].ToString());
                Boolean _EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSsl"].ToString());


                mail.To.Add(new MailAddress(Email));
                mail.From = new MailAddress(FromEmailID);
                string msgbody = "";
                 if(Type==EnumValue.GetEnumDescription(EnumValue.EmailType.Appointment))
                {
                    mail.Subject = "Appointment";
                    //using (StreamReader reader = new StreamReader(@"C:\Sites\Paramsites\RplWebsite\RplWebsite\Template\index.html"))
                    using (StreamReader reader = new StreamReader(@"E:\Rajpal\Rajpal\Appointment.html"))
                    {
                        msgbody = reader.ReadToEnd();
                        msgbody = msgbody.Replace("{Name}", Name);
                        msgbody = msgbody.Replace("{Date}", Date);
                        msgbody = msgbody.Replace("{Time}", Time);
                       
                    }
                }
                else
                {
                    mail.Subject = "Event deleted";

                    msgbody = msgbody + "<br />";
                    msgbody = msgbody + "<table style='width:80%'>";
                    msgbody = msgbody + "<tr>";
                    msgbody = msgbody + "<td align='left' style=' font-family:Arial; font-size:15px;'>Hi " + Name + " , <br /></td></tr>";
                    msgbody = msgbody + "<br /><tr><td><font style=' font-family:Arial; font-size:13px;'>Event with " + Name + " has been deleted, please delete its folder.</font><br /><br /></td>";
                    msgbody = msgbody + "</tr>";


                    msgbody = msgbody + "<br /><br />";
                    msgbody = msgbody + "<br /><font style=' font-family:Arial; font-size:13px;'>Thanks,</font><br /><br />";
                    msgbody = msgbody + "<font style=' font-family:Arial; font-size:13px;'>Image OCR team.</font>";
                }


                mail.Body = msgbody;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = WebConfigurationManager.AppSettings["Host"].ToString();
                smtp.Port = _Port;
                smtp.Credentials = new System.Net.NetworkCredential(FromEmailID, FromEmailPassword);// Enter senders User name and password
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = _EnableSsl;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.ToString();
                return false;
            }
        }
        public static Boolean SendMailToAdmin(string Type, string Name, string Email = "", string PhoneNumber = "", string Date = "", string Time = "", string Comment = "")
        {
            try
            {
                // Send mail.
                MailMessage mail = new MailMessage();

                string FromEmailID = WebConfigurationManager.AppSettings["FromEmailID"];
                string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
                string ToEmailID = WebConfigurationManager.AppSettings["ToEmailID"];

                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
                int _Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString());
                Boolean _UseDefaultCredentials = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseDefaultCredentials"].ToString());
                Boolean _EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSsl"].ToString());


                mail.To.Add(new MailAddress(FromEmailID));
                mail.From = new MailAddress(FromEmailID);
                if(Type==EnumValue.GetEnumDescription(EnumValue.EmailType.Appointment))
                {
                    mail.Subject = "Appointment";
                }
              
                string msgbody = "";
                msgbody = msgbody + "<br />";
                msgbody = msgbody + "<table style='width:80%'>";
                msgbody = msgbody + "<tr>";
                msgbody = msgbody + "<td align='left' style=' font-family:Arial; font-size:15px;'>Hi Admin, <br /></td></tr>";
                msgbody = msgbody + "<br /><tr><td><font style=' font-family:Arial; font-size:13px;'>You have recently contacted for appointment. Below are details -</font><br /></td></tr>";
                //msgbody = msgbody + "<br /><tr><td><font style=' font-family:Arial; font-size:13px;'>Please find your password below:</font></td></tr>";

                msgbody = msgbody + "<tr><td align='left'>";
                msgbody = msgbody + "<br /><font style=' font-family:Arial; font-size:13px;'>Name: " + Name + "</font><br /><br />";
                msgbody = msgbody + "<font style=' font-family:Arial; font-size:13px;'>Email: " + Email + "</font><br /><br />";
                msgbody = msgbody + "<br /><font style=' font-family:Arial; font-size:13px;'>Phone Number: " + PhoneNumber + "</font><br /><br />";
                
                msgbody = msgbody + "<font style=' font-family:Arial; font-size:13px;'>Appointment Date: " + Date + "</font><br /><br />";
                msgbody = msgbody + "<br /><font style=' font-family:Arial; font-size:13px;'>Appointment Time: " +Time + "</font><br /><br />";
                msgbody = msgbody + "<font style=' font-family:Arial; font-size:13px;'>Comment: " + Comment + "</font>";
                msgbody = msgbody + "<br /><br />";
                //msgbody = msgbody + "<br /><font style=' font-family:Arial; font-size:13px;'>Thanks,</font><br /><br />";
                //msgbody = msgbody + "<font style=' font-family:Arial; font-size:13px;'>Sports Photo team.</font>";

                mail.Body = msgbody;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = WebConfigurationManager.AppSettings["Host"].ToString(); //_Host;
                smtp.Port = _Port;
                smtp.Credentials = new System.Net.NetworkCredential(FromEmailID, FromEmailPassword);// Enter senders User name and password
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = _EnableSsl;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.ToString();
                return false;
            }
        }
    }
}