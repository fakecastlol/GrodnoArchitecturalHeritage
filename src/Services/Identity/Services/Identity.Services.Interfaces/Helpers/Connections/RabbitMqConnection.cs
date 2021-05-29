//using EasyNetQ;
//using System;

//namespace Identity.Services.Interfaces.Helpers.Connections
//{
//    public class RabbitMqConnection
//    {
//        public static void RabbitConnection()
//        {
//            using var bus = RabbitHutch.CreateBus("amqp://rabbitmq:rabbitmq@localhost:5672");
//            var input = "";
//            Console.WriteLine("Enter a message. 'q' to quit.");
//            while ((input = Console.ReadLine()) != "q")
//            {
//                bus.PubSub.Publish(new TextMessage
//                {
//                    Text = input
//                });
//            }
//        }
//    }
//}
