using System;
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
                var sender = "2000500666";
                var receptor = phoneNumber;
                var message = messageText;
                var api = new Kavenegar.KavenegarApi("575455525749495A3330304144706B355A6B7938714D364A6F686458523367344D7977457631316A3841773D");
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
