using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace SimpleMailer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check to see if the configuration file was supplied and does exist
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("Missing configuration file");
                Environment.Exit(1);
            }

            // Load the options
            Options options = JsonConvert.DeserializeObject<Options>(File.ReadAllText(args[0]));
            
            // Create a smpt client
            SmtpClient client = new SmtpClient(options.Host, options.Port);
            client.Credentials = new NetworkCredential(options.User, options.Password);
            client.EnableSsl = true;

            // Create a new message
            MailMessage message = new MailMessage();
            message.Subject = options.Subject;
            message.Body = options.Body;
            message.From = new MailAddress(options.From);

            // Add all the recipients
            foreach (string to in options.To)
            {
                message.To.Add(to);
            }

            // Add attachements if applicable
            foreach (string attachement in options.Attachments)
            {
                if (File.Exists(attachement))
                {
                    message.Attachments.Add(new Attachment(attachement));
                }
            }

            // Send the message
            client.Send(message);
        }
    }
}
