using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PluginDevelopment.Helper.RabbitMQ
{
    public class RabbitMqServer
    {
        public static List<string> RabbitMq()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "192.168.181.234",
                UserName = "flyt",
                VirtualHost = "flyt",
                Port = 5672,
                Password = "flyt123"
            };
            List<string> list = new List<string>();
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("dotnet", false, false, false, null);
                    //这样RabbitMQ就会使得每个Consumer在同一个时间点最多处理一个Message。
                    //换句话说，在接收到该Consumer的ack前，他它不会将新的Message分发给它。
                    channel.BasicQos(0, 1, false);
                    //var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += (model, ea) =>
                    //{
                    //    var body = ea.Body;
                    //    var message = Encoding.UTF8.GetString(body);
                    //    list.Add(message);
                    //};
                    //channel.BasicConsume("dotnet", true, consumer);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("task_hello", false, null, consumer);//需要接受方发送ack回执,删除消息
                    Console.WriteLine(" [*] Waiting for messages." + "To exit press CTRL+C");
                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();//挂起的操作
                        channel.BasicAck(ea.DeliveryTag, false);//与channel.BasicConsume("task_queue", false, null, consumer);对应
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);
                        Console.WriteLine(" [x] Done");
                    }
                }
            }
            return list;
        }
    }
}
