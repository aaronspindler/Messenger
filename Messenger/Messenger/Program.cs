using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            int interval = 2000;
            
            //Phone number format must follow + (Country Code) (Area Code) Number
            // For instance +11234567890 where 1 is country code 123 is area code and then the number
            string toNumber = "+";
            int messagePerNumber = 3;
            
            StreamReader authReader = new StreamReader("auth.txt");
            //Load Settings
            string accountSid = authReader.ReadLine();
            string authToken = authReader.ReadLine();
            authReader.Close();
            
            StreamReader messageReader = new StreamReader("message.txt");
            string messageBody = messageReader.ReadToEnd();
            messageReader.Close();
            
            StreamReader numbersReader = new StreamReader("numbers.txt");
            List<string> fromList = new List<string>();
            string line = numbersReader.ReadLine();
            do
            {
                fromList.Add(line);
                line = numbersReader.ReadLine();
            } while (line != null);

            TwilioClient.Init(accountSid, authToken);

            for (int i = 0; i < fromList.Count; i++)
            {
                for (int j = 0; j < messagePerNumber; j++)
                {
                    var message = MessageResource.Create(
                        body: messageBody,
                        from: new Twilio.Types.PhoneNumber(fromList[i]),
                        to: new Twilio.Types.PhoneNumber(toNumber)
                        );
                    Console.WriteLine(DateTime.Now + message.Sid);
                    Thread.Sleep(interval);
                }
            }
        }
    }
}