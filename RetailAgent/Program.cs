using System;
using System.Net;
 
using System.Threading.Tasks;
using System.Diagnostics;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

public class Program
{
    static int cnt = 0;
    public static void Main(string[] args)
    {
        

        Console.WriteLine("Hello, World!");
        while(true)
        {
            Produce();
            System.ConsoleKeyInfo message = Console.ReadKey();
            if (message.KeyChar == 'q')
            {
                break;
            }
        }
     
      
    }

    private static void Produce()
    {
        var producer = new Producer<Bannana>();

        for (int i = 0; i < 1; i++)
        {
             
            var bannana = new Bannana
            {

                Name = "Bannana " + cnt++,
                Price = i +1,
            };
            Console.WriteLine("Producing: " + JsonConvert.SerializeObject(bannana));
            producer.ProduceAsync(bannana);
        }
        Console.Read();
    }

    private static void Consume()
    {
        try
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:29092",
  //EnableAutoCommit = false,

            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe("test2");
            while (true)
            {
                 Console.WriteLine(   "while (true)");

                var cr = c.Consume();
                Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }
}

public class Producer<T>
{

    readonly string? _host;
    readonly int _port;
    readonly string? _topic;

    public Producer()
    {
        _host = "localhost";
        _port = 29092;
        _topic = "posretail_c2_s2";
    }

    ProducerConfig GetProducerConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = $"{_host}:{_port}",
           
        };
    }

    public async Task ProduceAsync(T data)
    {
        using (var producer = new ProducerBuilder<Null, T>(GetProducerConfig())
                                             .SetValueSerializer(new CustomValueSerializer<T>())
                                             .Build())
        {
            await producer.ProduceAsync(_topic, new Message<Null, T> { Value = data });
        }
    }
}

public class Bannana
{
    public string Name { get; set; } = "";
    public int Price { get; set; } = 1;
    public DateTime TS { get; set; } = DateTime.Now;

}
public class CustomValueSerializer<Bannana> : Confluent.Kafka.ISerializer<Bannana>
{
    public byte[] Serialize(Bannana data, SerializationContext ctx)
    {
        return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
    }
}