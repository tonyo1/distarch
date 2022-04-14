
using Confluent.Kafka;
using Newtonsoft.Json;
using DtoLib;
public class Program
{
    static int cnt = 0;
    public static void Main(string[] args)
    {


        Console.WriteLine("Hello, World!");
        while (true)
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


        var bn = new Bannana
        {
            Name = "Bannana" + cnt++,
            Price = cnt++ * 10
        };


        Console.WriteLine("Producing: " + JsonConvert.SerializeObject(bn));
        producer.ProduceAsync(bn);
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
        _port = 9092;
        _topic = "posretail_c2_s2";
    }

    ProducerConfig GetProducerConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = $"{_host}:{_port}",
            Debug = "msg",

            // retry settings:
            // Receive acknowledgement from all sync replicas
            Acks = Acks.Leader,
            // Number of times to retry before giving up
            MessageSendMaxRetries = 3,
            // Duration to retry before next attempt
            RetryBackoffMs = 1000,
            // Set to true if you don't want to reorder messages on retry
            EnableIdempotence = true
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