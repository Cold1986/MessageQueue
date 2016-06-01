using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using Cold.CommonLibrary;

namespace ReceiveQueue
{
    class Program
    {
        LogHelper _log = new LogHelper("MQLog");
        static void Main(string[] args)
        {
            Program myNewQueue = new Program();
            Console.WriteLine("Start ReceivMessage and Press Any key to Start!");
            //Console.ReadLine();
            //myNewQueue.ReceivMessage();
            //Console.WriteLine("Then start SendMessageTransation and Press Any key to Start!");
            //Console.ReadLine();
            myNewQueue.ReceivMessageTransation();
            Console.WriteLine("Sucess and press any key to exit!");
            Console.ReadLine();  
        }

        public void ReceivMessage()
        {
            if (!MessageQueue.Exists(".\\Private$\\TestPQueue"))
            {
                MessageQueue myNewPrivateQueue =
                MessageQueue.Create(".\\Private$\\TestPQueue");
            }
            MessageQueue queue = new MessageQueue(@".\Private$\TestPQueue");
            queue.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
            using (MessageEnumerator messages = queue.GetMessageEnumerator2())
            {
                while (messages.MoveNext(TimeSpan.FromSeconds(30)))
                {
                    Message message = messages.Current;
                    message = queue.Receive();
                    _log.WriteLog(message.Body.ToString());
                    Console.WriteLine(message.Body);
                }
            }
            return;
        }



        public void ReceivMessageTransation()
        {
            if (!MessageQueue.Exists(".\\Private$\\TestPQueueTransation3"))
            {
                MessageQueue myNewPrivateQueue =
                MessageQueue.Create(".\\Private$\\TestPQueueTransation3", true);
            }
            MessageQueue queue2 = new MessageQueue(@".\Private$\TestPQueueTransation3");
            queue2.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
            MessageQueueTransaction mytransaction = new MessageQueueTransaction();
            try
            {

                using (MessageEnumerator messages = queue2.GetMessageEnumerator2())
                {
                    while (messages.MoveNext(TimeSpan.FromSeconds(30)))
                    {
                        mytransaction.Begin();
                        Message message = messages.Current;
                        message = queue2.Receive(mytransaction);
                        Console.WriteLine(message.Body);
                        mytransaction.Commit();
                    }
                }
            }
            catch (MessageQueueException e)
            {
                if (e.MessageQueueErrorCode == MessageQueueErrorCode.TransactionUsage)
                {
                    Console.WriteLine("Queue is not transactional.");
                }
                mytransaction.Abort();

            }
            return;
        }  
    }
}
