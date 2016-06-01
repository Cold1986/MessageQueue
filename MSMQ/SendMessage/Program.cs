using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SendMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!MessageQueue.Exists(".\\Private$\\TestPQueue"))
            {
                MessageQueue myNewPrivateQueue = MessageQueue.Create(".\\Private$\\TestPQueue");
            }

            MessageQueue queue = new MessageQueue(".\\Private$\\TestPQueue");
            queue.Purge();//清空指定队列的消息。
            Console.WriteLine("StartSendMessage");
            for (int i = 0; i < 5000; i++)
            {
                Message message = new Message
                {
                    ID = Guid.NewGuid().ToString(),
                    Time = System.DateTime.Now
                };
                queue.Send(JsonConvert.SerializeObject(message));
                //queue.Send("111");
            }
            Console.WriteLine("Sucess!");
            Console.ReadLine();  
        }
    }
}
