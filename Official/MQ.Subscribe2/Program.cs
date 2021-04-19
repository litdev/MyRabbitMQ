using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace MQ.Subscribe2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new() { HostName = "localhost", UserName = "admin", Password = "admin" };
            using (var connection = factory.CreateConnection()) //连接
            {
                using (var channel = connection.CreateModel())//通道
                {
                    Console.WriteLine($"消费者 02 准备好接收消息了...");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;

                    string queueName = "mq1";//队列名称
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    Console.WriteLine("输入[Enter]退出程序");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Hello World!");
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = System.Text.Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine($"消费者 02 接收消息：{message}");
        }
    }
}
