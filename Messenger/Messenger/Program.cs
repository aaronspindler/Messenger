using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            string toNumber = "+1";
            int messagePerNumber = 3;
            
            StreamReader reader = new StreamReader("auth.txt");
            //Load Settings
            string accountSid = reader.ReadLine();
            string authToken = reader.ReadLine();

            TwilioClient.Init(accountSid, authToken);
            
            
            List<string> fromList = new List<string>();
            fromList.Add("+1");

            for (int i = 0; i < fromList.Count; i++)
            {
                for (int j = 0; j < messagePerNumber; j++)
                {
                    var message = MessageResource.Create(
                        body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
                        from: new Twilio.Types.PhoneNumber(fromList[i]),
                        to: new Twilio.Types.PhoneNumber(toNumber)
                        );
                    Console.WriteLine(message.Sid);
                }
            }
        }
    }
}