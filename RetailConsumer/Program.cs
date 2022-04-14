using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

public class Program
{
    const string TOPIC = "posretail_c2_s2";
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
            var conf = new ConsumerConfig
            {
                GroupId = "5",
                BootstrapServers = "localhost:9092",
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Latest,

            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            //c.Assign(new TopicPartitionOffset(TOPIC, 0, new Offset(321)));
            c.Subscribe(TOPIC);
            int i = 0;
            
      
            while (true)
            {  
             
                 Console.WriteLine("while");
                var cr = c.Consume();
                
                c.Commit(cr);
                Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}  .");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

public class Bannana
{
    public string Name { get; set; } = "";
    public int Price { get; set; } = 1;
}
public class CustomValueSerializer<Bannana> : Confluent.Kafka.ISerializer<Bannana>
{
    public byte[] Serialize(Bannana data, SerializationContext ctx)
    {
        return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
    }
}