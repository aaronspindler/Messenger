using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            //Change to set how long between sending messages
            //Good to prevent account being locked by spam control
            int interval = 2000;
            
            //Phone number format must follow + (Country Code) (Area Code) Number
            // For instance +11234567890 where 1 is country code 123 is area code and then the number
            string toNumber = "+";
            
            //How many messages do you want to send per number in numbers.txt
            int messagePerNumber = 3;
            
            //There must be an auth.txt file that has the first line as your account SID
            //and the second line as your auth token
            StreamReader authReader = new StreamReader("auth.txt");
            string accountSid = authReader.ReadLine();
            string authToken = authReader.ReadLine();
            authReader.Close();
            
            //Messages can be multi line
            StreamReader messageReader = new StreamReader("message.txt");
            string messageBody = messageReader.ReadToEnd();
            messageReader.Close();
            
            //Numbers must be entered in the same format as for the toNumber and
            //one number per line
            StreamReader numbersReader = new StreamReader("numbers.txt");
            List<string> fromList = new List<string>();
            string line = numbersReader.ReadLine();
            do
            {
                fromList.Add(line);
                line = numbersReader.ReadLine();
            } while (line != null);
            numbersReader.Close();

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