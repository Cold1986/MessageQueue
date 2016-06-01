using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SendMessageTransztion
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!MessageQueue.Exists(".\\Private$\\TestPQueueTransation3"))
            {
                MessageQueue myNewPrivateQueue =
                MessageQueue.Create(".\\Private$\\TestPQueueTransation3", true);
            }
            MessageQueue queue = new MessageQueue(".\\Private$\\TestPQueueTransation3");
            MessageQueueTransaction transation = new MessageQueueTransaction();
            try
            {
                transation.Begin();
                Console.WriteLine("SendMessage..");
                for (int i = 0; i < 5000; i++)
                {
                    queue.Send("a", transation);
                    queue.Send("b", transation);
                    queue.Send("c", transation);
                    queue.Send("d", transation);
                    queue.Send("e", transation);
                    queue.Send("f", transation);
                    queue.Send("g", transation);
                    queue.Send("h", transation);
                    queue.Send("i", transation);
                    queue.Send("j", transation);
                }
                transation.Commit();
                Console.WriteLine("Sucess");
                Console.ReadLine();
            }
            catch
            {
                transation.Abort();
            }
        }
    }
}
