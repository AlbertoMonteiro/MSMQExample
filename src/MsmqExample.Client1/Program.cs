using System;
using System.Messaging;
using System.Threading;
using System.Threading.Tasks;
using ColoredConsole;

namespace MsmqExample.Client1
{
    class Program
    {
        const string QUEUE_NAME = @".\private$\sample_queue";
        static void Main(string[] args)
        {
            Console.Title = "Receiver";
            RunConsumer(s => s.Red(), "Worker 1");
            RunConsumer(s => s.White(), "Worker 2");
            Console.ReadLine();
        }

        private static void RunConsumer(Func<string, ColorToken> inColor, string workerName)
        {
            Task.Run(() =>
            {
                var queue = new MessageQueue(QUEUE_NAME) {Formatter = new XmlMessageFormatter(new[] {typeof (string)})};
                ColorConsole.WriteLine(inColor(string.Format("{0} - Iniciado", workerName)));
                while (true)
                {
                    ColorConsole.WriteLine(inColor(string.Format("{0} - Carregando mensagens", workerName)));
                    var message = queue.Receive();
                    if (message != null)
                        ColorConsole.WriteLine(inColor(string.Format("{0} - Received: {1}", workerName, message.Body)));
                    ColorConsole.WriteLine(inColor(string.Format("{0} - Tudo recebido", workerName)));
                    Thread.Sleep(800);
                }
            });
        }
    }
}
