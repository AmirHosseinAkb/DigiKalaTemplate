﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Senders
{
    public class MessageSender
    {
        public static bool SendMessage(string phoneNumber,string messageText)
        {
            try
            {
                var sender = "10008663";
                var receptor = phoneNumber;
                var message = messageText;
                var api = new Kavenegar.KavenegarApi("2F6B4A4267447442556A4D4A523559554651672F4F414A314D3941414B434A326A744845546834793677343D");
                api.Send(sender, receptor, message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
