using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text.Json;

namespace SimpleMailer
{
    class Program
    {
        private static void Main(string[] args)
        {
            // Check to see if the configuration file was supplied and does exist
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("Missing configuration file");
                Environment.Exit(1);
            }

            // Load the options
            var options = JsonSerializer.Deserialize<Options>(File.OpenRead(args[0]));
            
            // Create a smtp client
            var client = new SmtpClient(options.Host, options.Port);
            client.Credentials = new NetworkCredential(options.User, options.Password);
            client.EnableSsl = true;

            // Create a new message
            var message = new MailMessage();
            message.Subject = options.Subject;
            message.Body = options.Body;
            message.From = new MailAddress(options.From);

            // Add all the recipients
            foreach (var to in options.To)
            {
                message.To.Add(to);
            }

            // Add attachments if applicable
            foreach (var attachment in options.Attachments)
            {
                if (File.Exists(attachment))
                {
                    message.Attachments.Add(new Attachment(attachment));
                }
            }

            // Send the message
            client.Send(message);
        }
    }
}
