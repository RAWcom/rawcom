using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Net.Mail;
using System.Collections.Specialized;
using Microsoft.SharePoint.Utilities;
using System.Net;
using System.Text.RegularExpressions;

namespace Tools
{
    public class SPEmail
    {

        public static bool SendMail(SPWeb web, string from, string to, string subject, string body, string cc, string bcc, string replyTo)
        {

            StringDictionary headers = new StringDictionary();
            headers.Add("from", from);
            headers.Add("to", to);
            headers.Add("subject", subject);
            if (!String.IsNullOrEmpty(cc)) headers.Add("cc", cc);
            if (!String.IsNullOrEmpty(bcc)) headers.Add("bcc", bcc);
            if (!String.IsNullOrEmpty(replyTo)) headers.Add("Return-Path", replyTo);
            headers.Add("content-type", "text/html");
            
            return SPUtility.SendEmail(web, headers, body);


        }

        public static bool SendMailWithAttachment(SPWeb web, string from, string fromName, string to, string toName, string subject, string body, bool isBodyHtml, string cc, string bcc, string replyTo, string replyToName, SPListItem item)
        {

            MailMessage message = new MailMessage();
            SPList list = item.ParentList;
            message.From = new MailAddress(from, fromName);
            message.To.Add(new MailAddress(to, toName));
            if (!string.IsNullOrEmpty(cc)) message.CC.Add(new MailAddress(cc));
            if (!string.IsNullOrEmpty(bcc)) message.Bcc.Add(new MailAddress(bcc));
            if (!string.IsNullOrEmpty(replyTo)) message.ReplyTo = new MailAddress(replyTo, replyToName);
            message.IsBodyHtml = isBodyHtml;
            message.Body = body;
            message.Subject = subject;

            return SendMailWithAttachment(web, message, item);
         
        }

        public static bool SendMailWithAttachment(SPWeb web, string from, string to, string subject, string body, bool isBodyHtml, string cc, string bcc, string replyTo, SPListItem item)
        {

            MailMessage message = new MailMessage();
            SPList list = item.ParentList;
            message.From = new MailAddress(from);
            message.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(cc)) message.CC.Add(new MailAddress(cc));
            if (!string.IsNullOrEmpty(bcc)) message.Bcc.Add(new MailAddress(bcc));
            if (!string.IsNullOrEmpty(replyTo)) message.ReplyTo = new MailAddress(replyTo);
            //message.IsBodyHtml = isBodyHtml;
            message.Body = body;
            message.Subject = subject;

            return SendMailWithAttachment(web, message, item);
        }

        public static bool SendMailWithAttachment(SPWeb web, MailMessage message,SPListItem item)
        {
            bool result = false;

            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = web.Site.WebApplication.OutboundMailServiceInstance.Server.Address;

                if (item!=null)
                {
                    for (int attachmentIndex = 0; attachmentIndex < item.Attachments.Count; attachmentIndex++)
                    {
                        string url = item.Attachments.UrlPrefix + item.Attachments[attachmentIndex];
                        SPFile file = item.ParentList.ParentWeb.GetFile(url);
                        message.Attachments.Add(new Attachment(file.OpenBinaryStream(), file.Name));
                    }
                }

                client.Send(message);

                result = true;
            }
            catch (Exception ex)
            {
                var r = Tools.ElasticEmail.ReportError(ex, item.ParentList.ParentWeb.Url);
                return false;
            }

            return result;
        }
    }
}

