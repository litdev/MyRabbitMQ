using RabbitMQ.Client;
using System;

namespace MQ.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 路由模式：Direct

            ConnectionFactory factory = new() { HostName = "localhost", UserName = "admin", Password = "admin" };
            using (var connection = factory.CreateConnection()) //连接
            {
                using (var channel = connection.CreateModel()) //通道
                {
                    string queueName = "mq1";//队列名称
                    string exChangeName = "mq1ExChange";//路由名称
                    //队列
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //路由
                    channel.ExchangeDeclare(exchange: exChangeName, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    //将队列绑定到路由上
                    channel.QueueBind(queueName, exchange: exChangeName, routingKey: string.Empty, arguments: null);

                    Console.WriteLine("生产者已准备就绪...");
                    int i = 1;
                    string input = Console.ReadLine();
                    while (input.ToLower() != "q")
                    {
                        string message = $"【{i}】--{input}";
                        byte[] body = System.Text.Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: exChangeName, routingKey: string.Empty, basicProperties: null, body: body);
                        Console.WriteLine($"消息：{message}   已发送");
                        i++;
                        input = Console.ReadLine();
                    }

                }
            }

            #endregion

            #region 路由模式：Fanout

            //ConnectionFactory factory = new() { HostName = "localhost", UserName = "admin", Password = "admin" };
            //using (var connection = factory.CreateConnection()) //连接
            //{
            //    using (var channel = connection.CreateModel()) //通道
            //    {
            //        string queueName1 = "mq1";//第一个队列
            //        string queueName2 = "mq2";//第二个队列
            //        string exChangeName = "mq1ExChange";//路由名称
            //        //队列
            //        channel.QueueDeclare(queue: queueName1, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        //队列2
            //        channel.QueueDeclare(queue: queueName2, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        //路由
            //        channel.ExchangeDeclare(exchange: exChangeName, type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);
            //        //将两个队列绑定到路由上
            //        channel.QueueBind(queueName1, exchange: exChangeName, routingKey: string.Empty, arguments: null);
            //        channel.QueueBind(queueName2, exchange: exChangeName, routingKey: string.Empty, arguments: null);

            //        Console.WriteLine("生产者已准备就绪...");
            //        int i = 1;
            //        string input = Console.ReadLine();
            //        while (input.ToLower() != "q")
            //        {
            //            string message = $"【{i}】--{input}";
            //            byte[] body = System.Text.Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: exChangeName, routingKey: string.Empty, basicProperties: null, body: body);
            //            Console.WriteLine($"消息：{message}   已发送");
            //            i++;
            //            input = Console.ReadLine();
            //        }

            //    }
            //}

            #endregion

            Console.WriteLine("Hello World!");
        }
    }
}
