﻿using System;

namespace EasyNetQ.Subscribe1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
            {
                bus.PubSub.Subscribe<Model.Message>("test", HandleTextMessage);

                Console.WriteLine($"消费者 01 准备好接收消息了...");
                Console.ReadLine();
            }
        }

        static void HandleTextMessage(Model.Message message)
        {
            Console.WriteLine($"消费者 01收到消息：{message.Text}");
        }
    }
}
