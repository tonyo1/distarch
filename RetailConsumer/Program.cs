using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;
using DtoLib;
public class Program
{
    const string TOPIC = "retail_server_c299_s9999_cmd";
    public static void Main(string[] args)
    {
        Task.Factory.StartNew(() =>
        {
            Consume();

        }, TaskCreationOptions.LongRunning);

        //
        Console.WriteLine("Hello, World!");

        //Produce();
        Console.ReadKey();
    }

    private static void Consume()
    {

        try
        {
  
            using var c = new ConsumerBuilder<StoreCommandType, string>(GetConfig()).Build();
            c.Subscribe(TOPIC);
         
            while (true)
            { 
                Console.WriteLine("while");
                ConsumeResult<StoreCommandType, string> cr = c.Consume();

                switch (cr.Message.Key)
                {
                    case StoreCommandType.Associate:

                        break;
                }
                c.Commit(cr);
                Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}  .");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        static ConsumerConfig GetConfig()
        {
            return new ConsumerConfig
            {
                GroupId = "primary",
                BootstrapServers = "localhost:9092",
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest,

            };
        }
    }
}
