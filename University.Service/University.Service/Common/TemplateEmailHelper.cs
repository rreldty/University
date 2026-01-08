using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using University.Dao.Base;

namespace University.Api.Common
{
    public class TemplateEmailHelper
    {
        SendgridHelper sendGridHelper;
        public TemplateEmailHelper() 
        {
            sendGridHelper = new SendgridHelper(Config.SendGridApiId, Config.SendGridApiSecret);
        }

        public string SendEmailProjectNotif(string strEmailTo, string strEmailCc, string strEmailSubject, Dictionary<string, string> prmMessage)
        {
            string strEmailMessage = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Templace/Email/project-notif-01.html"));

            foreach (KeyValuePair<string, string> kvp in prmMessage) {
                strEmailMessage = strEmailMessage.Replace(kvp.Key, kvp.Value);
            }

            List <Attachment> lstAttachement = new List<Attachment>
            {
                sendGridHelper.generateFileAttachment("project-notif-01-header.png", File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Templace/Email/project-notif-01-header.png"))),
                sendGridHelper.generateFileAttachment("project-notif-01-footer.png", File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Templace/Email/project-notif-01-footer.png")))
            };

            string strResult = sendGridHelper.SendEmail(Config.EmailFrom, strEmailTo, strEmailCc, strEmailMessage, strEmailSubject, lstAttachement);

            return strResult;
        }
    }
}