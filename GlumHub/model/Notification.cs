using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.RightsManagement;
using MimeKit;
using MailKit.Net.Smtp;
using System.Windows;

namespace GlumHub
{
    internal class Notification
    {
        private long _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get { return _id; } set { _id = value; } }

        [ForeignKey("UserId")]
        private long _receiverId;
        public long Receiverid { get { return _receiverId; } set { _receiverId = value; } }
        public User Receiver { get; set; }

        private DateTime _created;
        public DateTime Created { get { return _created; } set{ _created = value; } }

        private string _message;
        public string Message { get { return _message; } set { _message = value; } }

        private bool _watched;
        public bool Watched { get { return _watched; } set { _watched = value; } }

        public Notification() { }

        public Notification(long receiverId,  string message)
        {
            _receiverId = receiverId;
            _message = message;
            _watched = false;
            _created = DateTime.Now;
        }


        public static void SendNotification(long receiverId, string message)
        {
            Notification notification = new Notification(receiverId, message);
            using(ApplicationContextDB db = new ApplicationContextDB())
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
            SendEmailNotification(receiverId, message, "plain");

        }

        public static void SendEmailNotification(long receiverId, string message, string bodyType)
        {
            try
            {
                User receiver;
                using (ApplicationContextDB db = new ApplicationContextDB())
                {
                    receiver = db.Users.FirstOrDefault(u => u.Id == receiverId);
                }

                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("", "glumhub@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", receiver.Email));
                emailMessage.Subject = "Notification";
                emailMessage.Body = new TextPart(bodyType)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("glumhub@gmail.com", "erhc mmig kqdq kzsj");
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }catch(Exception ex)
            {

            }
        }


        public static void SendEmailNotification(string address, string message, string bodyType)
        {

            try
            {
                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("", "glumhub@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", address));
                emailMessage.Subject = "Notification";
                emailMessage.Body = new TextPart(bodyType)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("glumhub@gmail.com", "erhc mmig kqdq kzsj");
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }catch (Exception ex)
            {

            }
        }


        public static string GenerateGoogleCalendarUrlForClient(Booking booking)
        {
            // Формируем параметры для ссылки Google Calendar
            string eventTitle = $"Booking with {booking.Master.Firstname}";
            string dateTimeStart = booking.Date_Time.ToString("yyyyMMddTHHmmssZ");
            string dateTimeEnd = booking.Date_Time.AddHours(1).ToString("yyyyMMddTHHmmssZ"); // Длительность 1 час
            string eventDescription = $"Service: {booking.Service} | Master: {booking.Master.Firstname}";
            string location = $"Location: {booking.Master.MasterInfo.BusinessAddress}"; // Место проведения мероприятия

            // Формируем URL для добавления события в Google Calendar
            string googleCalendarUrl = $"https://www.google.com/calendar/render?action=TEMPLATE" +
                                       $"&text={Uri.EscapeDataString(eventTitle)}" +
                                       $"&dates={dateTimeStart}/{dateTimeEnd}" +
                                       $"&details={Uri.EscapeDataString(eventDescription)}" +
                                       $"&location={Uri.EscapeDataString(location)}";

            return googleCalendarUrl;
        }

        public static string GenerateGoogleCalendarUrlForMaster(Booking booking)
        {
            // Формируем параметры для ссылки Google Calendar
            string eventTitle = $"Booking with {booking.Client.Firstname}";
            string dateTimeStart = booking.Date_Time.ToString("yyyyMMddTHHmmssZ");
            string dateTimeEnd = booking.Date_Time.AddHours(1).ToString("yyyyMMddTHHmmssZ"); // Длительность 1 час
            string eventDescription = $"Service: {booking.Service} | Client: {booking.Client.Firstname}";
            string location = $"Location: {booking.Master.MasterInfo.BusinessAddress}"; // Место проведения мероприятия

            // Формируем URL для добавления события в Google Calendar
            string googleCalendarUrl = $"https://www.google.com/calendar/render?action=TEMPLATE" +
                                       $"&text={Uri.EscapeDataString(eventTitle)}" +
                                       $"&dates={dateTimeStart}/{dateTimeEnd}" +
                                       $"&details={Uri.EscapeDataString(eventDescription)}" +
                                       $"&location={Uri.EscapeDataString(location)}";

            return googleCalendarUrl;
        }
    }
}
