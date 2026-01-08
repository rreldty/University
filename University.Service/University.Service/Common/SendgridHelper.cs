using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace University.Api.Common
{
    public class SendgridHelper
    {
        string strApiId;
        string strApiSecret;

        public SendgridHelper(string strSendgridApiId, string strSendgridApiSecret)
        {
            strApiId = strSendgridApiId;
            strApiSecret = strSendgridApiSecret;
        }

        public Attachment generateFileAttachment(string strFileName, byte[] bytFile)
        {
            Attachment attachment = new Attachment()
            {
                Content = Convert.ToBase64String(bytFile),
                ContentId = strFileName.Replace(".", "_"),
                Filename = strFileName
            };

            return attachment;
        }

        public string SendEmail(string strEmailFrom, string strEmailTo, string strEmailMessage, string strEmailSubject)
        {
            return SendEmail(strEmailFrom, strEmailTo, string.Empty, string.Empty, strEmailMessage, strEmailSubject, null);
        }

        public string SendEmail(string strEmailFrom, string strEmailTo, string strEmailCc, string strEmailMessage, string strEmailSubject)
        {
            return SendEmail(strEmailFrom, strEmailTo, strEmailCc, string.Empty, strEmailMessage, strEmailSubject, null);
        }

        public string SendEmail(string strEmailFrom, string strEmailTo, string strEmailCc, string strEmailBcc, string strEmailMessage, string strEmailSubject)
        {
            return SendEmail(strEmailFrom, strEmailTo, strEmailCc, strEmailBcc, strEmailMessage, strEmailSubject, null);
        }

        public string SendEmail(string strEmailFrom, string strEmailTo, string strEmailMessage, string strEmailSubject, List<Attachment> lstMailAttachment)
        {
            return SendEmail(strEmailFrom, strEmailTo, string.Empty, string.Empty, strEmailMessage, strEmailSubject, lstMailAttachment);
        }

        public string SendEmail(string strEmailFrom, string strEmailTo, string strEmailCc, string strEmailMessage, string strEmailSubject, List<Attachment> lstMailAttachment)
        {
            return SendEmail(strEmailFrom, strEmailTo, strEmailCc, string.Empty, strEmailMessage, strEmailSubject, lstMailAttachment);
        }

        public string SendEmail(string strEmailFrom, string strEmailTo, string strEmailCc, string strEmailBcc, string strEmailMessage, string strEmailSubject, List<Attachment> lstMailAttachment)
        {
            string strResult = string.Empty;

            //Validation
            if (string.IsNullOrEmpty(strEmailFrom))
                strResult += "Email From can't empty\n";

            if (string.IsNullOrEmpty(strEmailTo))
                strResult += "Email To can't empty\n";

            if (string.IsNullOrEmpty(strEmailTo))
                strResult += "Email To can't empty\n";

            if (string.IsNullOrEmpty(strEmailSubject))
                strResult += "Email Subject can't empty\n";

            if (!string.IsNullOrEmpty(strEmailFrom))
            {
                string[] lstEmailFrom = strEmailFrom.Split(';');
                if (lstEmailFrom.Length > 1)
                {
                    strResult += "Email From cannot more than 1 address\n";
                }
            }

            List<EmailAddress> lstMailTo = new List<EmailAddress>();
            List<EmailAddress> lstMailCc = new List<EmailAddress>();
            List<EmailAddress> lstMailBcc = new List<EmailAddress>();

            string[] lstEmailTo = strEmailTo.Split(';');
            for (int n = 0; n < lstEmailTo.Length; n++)
            {
                if (!string.IsNullOrEmpty(lstEmailTo[n]))
                {
                    lstMailTo.Add(new EmailAddress(lstEmailTo[n]));
                }
            }

            if (!string.IsNullOrEmpty(strEmailCc))
            {
                string[] lstEmailCc = strEmailCc.Split(';');
                for (int n = 0; n < lstEmailCc.Length; n++)
                {
                    if (!string.IsNullOrEmpty(lstEmailCc[n]))
                    {
                        lstMailCc.Add(new EmailAddress(lstEmailCc[n]));
                    }
                }
            }

            if (!string.IsNullOrEmpty(strEmailBcc))
            {
                string[] lstEmailBcc = strEmailBcc.Split(';');
                for (int n = 0; n < lstEmailBcc.Length; n++)
                {
                    if (!string.IsNullOrEmpty(lstEmailBcc[n]))
                    {
                        lstMailBcc.Add(new EmailAddress(lstEmailBcc[n]));
                    }
                }
            }

            Task<string> taskResult = Task.Run(() => SendEmailAsync(new EmailAddress(strEmailFrom), lstMailTo, lstMailCc, lstMailBcc, strEmailMessage, strEmailSubject, lstMailAttachment));
            taskResult.Wait();
            strResult = taskResult.Result;
            return strResult;
        }
        async Task<string> SendEmailAsync(EmailAddress mailFrom, List<EmailAddress> lstMailTo, List<EmailAddress> lstMailCc, List<EmailAddress> lstMailBcc, string strEmailMessage, string strEmailSubject, List<Attachment> lstMailAttachment)
        {
            string strResult = string.Empty;
            try
            {
                SendGridMessage sendGridMessage = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmailToMultipleRecipients(mailFrom, lstMailTo, strEmailSubject, string.Empty, strEmailMessage);
                if (lstMailCc != null && lstMailCc.Count > 0)
                {
                    sendGridMessage.AddCcs(lstMailCc);
                }
                if (lstMailBcc != null && lstMailBcc.Count > 0)
                {
                    sendGridMessage.AddBccs(lstMailBcc);
                }

                if (lstMailAttachment != null && lstMailAttachment.Count > 0)
                {
                    sendGridMessage.AddAttachments(lstMailAttachment);
                }

                SendGridClient sendGridClient = new SendGridClient(strApiSecret);
                Response response = await sendGridClient.SendEmailAsync(sendGridMessage).ConfigureAwait(false);

                if (response != null && !response.IsSuccessStatusCode)
                {
                    strResult = response.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return strResult;


        }
    }
}