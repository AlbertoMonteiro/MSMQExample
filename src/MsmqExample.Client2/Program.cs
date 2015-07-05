using System;
using System.Messaging;
using System.Threading;

namespace MsmqExample.Client2
{
    class Program
    {
        const string queue_name = @".\private$\sample_queue";

        static void Main(string[] args)
        {
            Console.Title = "Sender";

            var queue = MessageQueue.Exists(queue_name) ? new MessageQueue(queue_name) : MessageQueue.Create(queue_name);

            for (int i = 0; i < 100; i++)
            {
                queue.Send(i.ToString());
                Thread.Sleep(1000);
            }
        }

    }
}
