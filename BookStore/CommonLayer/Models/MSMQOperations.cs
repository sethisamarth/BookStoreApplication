using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class MSMQOperations
    {
        MessageQueue msmq = new MessageQueue();  //MessageQueue provides access to a queue on a message queue server.
        public void Sender(string token)
        {
            msmq.Path = @".\private$\Tokens";
            try
            {
                //for get or create queue
                if (!MessageQueue.Exists(msmq.Path))
                {
                    MessageQueue.Create(msmq.Path);
                }

                //process to send data to queue
                //Formatter is used as msg is stored in searializes form 
                msmq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                msmq.ReceiveCompleted += Msmq_ReceiveCompleted;
                msmq.Send(token);
                msmq.BeginReceive();
                msmq.Close();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }


        private void Msmq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = msmq.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            // mail sending code smtp 
            string mailReceiver = GetEmailFromToken(token).ToString();
            MailMessage message = new MailMessage("pratikshatest20@gmail.com", mailReceiver);
            string bodymessage = "for reset click here <a href='https://localhost:44389/'> click me</a>" +
                "copy the token Provided here : " + token;
            message.Subject = " Email For reset the password";
            message.Body = bodymessage;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);//smtp server name and port number
                                                                      //
            System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("sj7525316@gmail.com", "s789456123");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            msmq.BeginReceive();
        }
        public static string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decoded = handler.ReadJwtToken((token));
            var result = decoded.Claims.FirstOrDefault().Value;
            return result;
        }

    }
}

