using System;
using EasyNetQ;

namespace EasyNetQ.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
            {
                Console.WriteLine("生产者已准备就绪...");
                int i = 1;
                string input = Console.ReadLine();
                while (input.ToLower() != "q")
                {
                    string message = $"【{i}】--{input}";
                    bus.PubSub.Publish(new Model.Message { Text = message });
                    Console.WriteLine($"消息：{message}   已发送");
                    i++;
                    input = Console.ReadLine();
                }
            }
        }
    }
}
