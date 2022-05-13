
using System;
using Serilog;
using Serilog.Debugging;
using Confluent.Kafka; 
using DtoLib;
using System.Text.Json;
using System.Text;

public class Program
{
    static int cnt = 0;
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Verbose()
                   .WriteTo.File("logs\\logfile_yyyyDDmm.txt")
                   .WriteTo.Console()
                   .CreateLogger();


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
        Producer<HLAgentMessage> producer = new Producer<HLAgentMessage>();
        HLAgentMessage cmd = new HLAgentMessage();
         
        producer.ProduceAsync(cmd);
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
        _topic = "retail_server_c299_s9999_cmd";
    }

    ProducerConfig GetProducerConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = $"{_host}:{_port}",
            Debug = "msg",

            // retry settings:
            // Receive acknowledgement from all sync replicas
            Acks = Acks.All,
            // Number of times to retry before giving up
            MessageSendMaxRetries = 3,
            // Duration to retry before next attempt
            RetryBackoffMs = 1000,
            // Set to true if you don't want to reorder messages on retry
            EnableIdempotence = false
        };
    }

    public async Task ProduceAsync(HLAgentMessage cmd, T data)
    {
        Log.Information("ProduceAsync: ");
        try
        {
            JsonSerializerOptions defaultOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            using (var producer = new ProducerBuilder<StoreCommandType, T>(GetProducerConfig())
                                .SetValueSerializer((ISerializer<T>)new CustomFormatter2(defaultOptions))
                                .SetLogHandler((_, message) =>
                                    Log.Information($"INFO Facility: {message.Facility}-{message.Level} Message: {message.Message}" ))
                                .SetErrorHandler((_, e) => Log.Error($"Error: {e.Reason}. Is Fatal: {e.IsFatal}"))
                                .Build())
            {
                 await producer.ProduceAsync(_topic, new Message<HLAgentMessage, T> {Key=cmd, Value = data });
         }
        }
        catch (ProduceException<Null, T> e)
        {
            Log.Information($"Delivery failed: {e.Error.Reason}");
        }
        catch (Exception ex)
        {
            Log.Information($"Delivery really failed: {ex}");
        }

        Log.Information("Produced: "  );

    }

     
}