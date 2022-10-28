using System.Net;
using System.Net.Mail;

namespace RiodeBackEndFinal.Utlis.Constant
{
    public class EmailHelper
    {

        public bool SendEmail(string userMail, string emailcontent,string emailSubject)
        {

           
            string myMail = "riodehelp@gmail.com";
            string pass = "lakmvyvazlnazjab";

            MailMessage mailMessage = new ();
            mailMessage.From= new MailAddress(myMail,"Riode");
            mailMessage.To.Add(new MailAddress(userMail));
            mailMessage.Subject = emailSubject;
            mailMessage.Body = emailcontent;
            mailMessage.IsBodyHtml = true;

            SmtpClient client = new()
            {
                Credentials = new NetworkCredential(myMail, pass),
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
            };
            client.Send(mailMessage);

            return true;
        }
    }
}
