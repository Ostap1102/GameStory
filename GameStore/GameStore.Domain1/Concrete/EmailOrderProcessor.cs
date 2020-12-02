using System.Net;
using System.Net.Mail;
using System.Text;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "lord11022000@ukr.net";
        public string MailFromAddress = "ostap.melnyk@oa.edu.ua";
        public bool UseSsl = true;
        public string Username = "ostap.melnyk@oa.edu.ua";
        public string Password = "ostap1102";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\game_store_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Нове запмовлення опрацьовано")
                    .AppendLine("---")
                    .AppendLine("Товари:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Game.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (Загальна сума: {2:c}",
                        line.Quantity, line.Game.Name, subtotal);
                }

                body.AppendFormat("Загальна вартість покупкиь: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine("Прізвище Імя:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine("Вулиця,будинок,квартира:")
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine("Номер телефону:")
                    .AppendLine(shippingInfo.Line2)
                    .AppendLine("Емайл:")
                    .AppendLine(shippingInfo.Line3)
                    .AppendLine("Місто:")
                    .AppendLine(shippingInfo.City)
                    .AppendLine("Доставка:")
                    .AppendLine(shippingInfo.vid)
                    .AppendLine("---")
                    .AppendFormat("Подарункова упаковка: {0}",
                        shippingInfo.GiftWrap ? "так" : "Ні");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// від кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Нове замовлення відправлено",		// Тема
                                       body.ToString()); 				// Сутність листа письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
